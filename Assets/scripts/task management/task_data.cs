using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class task_data : MonoBehaviour
{
    public TextMeshProUGUI main_text;
    public TextMeshProUGUI description;


    public string main_textString;
    public string descriptionString;

    public TextMeshProUGUI header_editor;
    public TextMeshProUGUI body_editor;

    public GameObject editor_window;
    public bool minimize;
    public bool isOpen;

    [Header("state stuff")]
    public int state; // 0 is neutral, 1 is complete, 2 is fail, 3 is delete
    public bool completion_toggle;
    public bool failure_toggle;
    public bool deletion_toggle;

    public GameObject[] crossouts;

    void Start()
    {
        main_text.text = main_textString;
        description.text = descriptionString;
    }


    void Update()
    {
        main_text.text = main_textString;
        description.text = descriptionString;

        minimize = editor_window.GetComponent<task_editing_window_behavior>().minimize;
        isOpen = editor_window.GetComponent<task_editing_window_behavior>().isOpen;

        if ((minimize == false) && (isOpen == true))
        {
            main_text.gameObject.SetActive(false);
            description.gameObject.SetActive(false);
        }
        else
        {
            main_text.gameObject.SetActive(true);
            description.gameObject.SetActive(true);
        }
    }

    public void updateHeader()
    {
        main_textString = header_editor.text;
    }
    public void updateBody()
    {
        descriptionString = body_editor.text;
    }
    #region state toggling
    public void toggle_completion()
    {
        if (completion_toggle == false)
        {
            state = 1;
            failure_toggle = false;
            deletion_toggle = false;
        }
        else
        {
            state = 0;
        }
        completion_toggle = !completion_toggle;
        toggle_appearance();
    }
    public void toggle_failure()
    {
        if (failure_toggle == false)
        {
            state = 2;
            completion_toggle = false;
            deletion_toggle = false;
        }
        else
        {
            state = 0;
        }
        failure_toggle = !failure_toggle;
        toggle_appearance();
    }
    public void toggle_deletion()
    {
        if (deletion_toggle == false)
        {
            state = 3;
            failure_toggle = false;
            completion_toggle = false;
        }
        else
        {
            state = 0;
        }
        deletion_toggle = !deletion_toggle;
        toggle_appearance();
    }

    public void toggle_appearance()
    {
        if (state == 0)
        {
            crossouts[1].SetActive(false);
            crossouts[2].SetActive(false);
            crossouts[3].SetActive(false);

        }
        else
        {
            crossouts[state].SetActive(true);
        }
    }
    #endregion

    public void force_open()
    {
        editor_window.GetComponent<task_editing_window_behavior>().toggle_open();
    }
}
