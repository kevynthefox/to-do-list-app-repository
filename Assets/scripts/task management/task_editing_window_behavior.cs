using System.Collections;
using System.Linq;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;


public class task_editing_window_behavior : MonoBehaviour
{
    [Header("window features")]//ie opening, closing, minimizing, maximizing.
    public bool isOpen;
    public bool minimize;
    public bool maximize;
    public int window_speed;//time it takes in fixed updates(?) for the window to change size fully.
    public Button Unminimizer;


    [Header("other")]
    public GameObject parent;
    public GameObject task_I_am_part_of;
    public GameObject task_I_am_part_of_parent;
    public TextMeshProUGUI limit_displayer;
    public TextMeshProUGUI parent_displayer;
    public TextMeshProUGUI header;
    public TextMeshProUGUI body;

    void Start()
    {
        if (task_I_am_part_of.GetComponent<task_data>().parent_task != null)
        {
            task_I_am_part_of_parent = task_I_am_part_of.GetComponent<task_data>().parent_task;
        }  
    }

    public void toggle_open()
    {
        isOpen = !isOpen;
        this.gameObject.SetActive(isOpen);

        if (isOpen == true)
        {
            transform.SetParent(time_controller.current.transform);
            transform.SetAsLastSibling();
        }
        else
        {
            transform.SetParent(parent.transform);
            transform.SetAsFirstSibling();
        }

        transform.SetPositionAndRotation(new Vector3(0, 0, transform.position.z), transform.rotation);

        task_I_am_part_of.GetComponent<task_data>().subtask_tracker();
        if (task_I_am_part_of.GetComponent<task_data>().parent_task != null)
        {
            parent_displayer.text = "> " + task_I_am_part_of.GetComponent<task_data>().parent_task.GetComponent<task_data>().header_textString;
        }
        else
        {
            parent_displayer.text = null;
        }


        if (task_I_am_part_of_parent != null)
        {
            sort_upon_edit_finish();
        }
    }

    public void toggle_minimize()
    {
        minimize = !minimize;
        StartCoroutine(animate_minimize());
        Unminimizer.gameObject.SetActive(minimize);

        task_I_am_part_of.GetComponent<task_data>().subtask_tracker();
    }
    public void toggle_maximize()
    {
        maximize = !maximize;
        StartCoroutine(animate_maximize());
    }



    public IEnumerator animate_minimize()
    {
        for (int i = 0; i < window_speed; i++)
        {
            if (minimize == true)
            {
                Debug.Log("minimize true");
                if (transform.localScale.x > 0)
                {
                    this.gameObject.transform.localScale -= (Vector3.one / window_speed);
                }
                else
                {
                    transform.localScale = Vector3.zero;
                    StopCoroutine(animate_minimize());
                }
            }
            if (minimize == false)
            {
                Debug.Log("minimize false");
                if (transform.localScale.x < 1)
                {
                    this.gameObject.transform.localScale += (Vector3.one / window_speed);
                }
                else
                {
                    transform.localScale = Vector3.one;
                    StopCoroutine(animate_minimize());
                }
            }
            yield return new WaitForFixedUpdate();

        }
    }
    public IEnumerator animate_maximize()
    {
        for (int i = 0; i < window_speed; i++)
        {
            if (maximize == true)
            {
                Debug.Log("maximize true");
                if (transform.localScale.x < 1.28f)
                {
                    this.gameObject.transform.localScale += (new Vector3(1.28f, 1.28f, 1.28f) / window_speed);
                }
                else
                {
                    transform.localScale = new Vector3(1.28f, 1.28f, 1.28f);
                    StopCoroutine(animate_maximize());
                }
            }
            if (maximize == false)
            {
                Debug.Log("maximize false");
                if (transform.localScale.x > 1)
                {
                    this.gameObject.transform.localScale -= (Vector3.one / window_speed);
                }
                else
                {
                    transform.localScale = Vector3.one;
                    StopCoroutine(animate_maximize());
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public void limit_display()
    {
        limit_displayer.text = "header: " + header.text.Length + "/540" + ", body: " + body.text.Length + "/5005";
    }

    public void sort_upon_edit_finish()
    {
        if (task_I_am_part_of.GetComponent<task_data>().priority >= 1 && task_I_am_part_of.GetComponent<task_data>().priority <= 8)
        {
            //task_I_am_part_of.GetComponent<task_data>().priority_change
            task_I_am_part_of.transform.parent = task_I_am_part_of_parent.GetComponent<task_data>().sort_areas[task_I_am_part_of.GetComponent<task_data>().priority].transform;
        }
    }
}
