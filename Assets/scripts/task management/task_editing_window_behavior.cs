using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class task_editing_window_behavior : MonoBehaviour
{
    [Header("window features")]//ie opening, closing, minimizing, maximizing.
    public bool isOpen;
    public bool minimize;
    public bool maximize;
    public int window_speed;//time it takes in fixed updates(?) for the window to change size fully.
    public GameObject Unminimizer;


    public void toggle_open()
    {
        isOpen = !isOpen;
        this.gameObject.SetActive(isOpen);
    }

    public void toggle_minimize()
    {
        minimize = !minimize;
        StartCoroutine(animate_minimize());
        Unminimizer.SetActive(minimize);
    }
    public void toggle_maximize()
    {
        maximize = !maximize;
        StartCoroutine(animate_maximize());
    }

    public void FixedUpdate()
    {
        transform.SetPositionAndRotation(new Vector3(0,0, transform.position.z), transform.rotation);

        
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
}
