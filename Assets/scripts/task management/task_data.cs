using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class task_data : MonoBehaviour
{
    [Header("text")]
    public TextMeshProUGUI main_text;
    public TextMeshProUGUI description;


    public string main_textString;
    public string descriptionString;

    [Header("edit window")]
    public TextMeshProUGUI header_editor;
    public TextMeshProUGUI body_editor;

    public GameObject editor_window;
    public GameObject editor_window_backup;
    public GameObject window_holder;
    public Button Unminimizer;
    public Button task_edit_button;
    public bool minimize;
    public bool isOpen;

    [Header("state stuff")]
    public int state; // 0 is neutral, 1 is complete, 2 is fail, 3 is delete
    public bool completion_toggle;
    public bool failure_toggle;
    public bool deletion_toggle;

    public GameObject[] crossouts;

    [Header("subtask stuff")]
    public GameObject parent_task;
    public List<int> Place_in_task_list; //use the number in the [] to retrieve the type of list. use the number it spits out as where that task goes in that list.
    //list type(priority sort, etc) reference table:
    /// <summary>
    /// 
    /// </summary>
    public int typeOf_Subtask; //0 is regular task as a subtask, 1 is comment, 2 is a project(sub project i guess), 3 is a section
    public List<GameObject> sub_tasks;

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
        toggle_State_appearance();
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
        toggle_State_appearance();
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
        toggle_State_appearance();
    }

    public void toggle_State_appearance()
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
        if (editor_window != parent_task.GetComponent<task_data>().editor_window)
        {
            Debug.Log("force open worked");
            editor_window.GetComponent<task_editing_window_behavior>().toggle_open();
        }
        else
        {
            Debug.Log("force open failed, trying again");
            editor_window = Instantiate(editor_window_backup, Vector3.zero, quaternion.identity);
            editor_window.transform.SetParent(window_holder.transform);
            editor_window.GetComponent<task_editing_window_behavior>().Unminimizer = Unminimizer;
            editor_window.GetComponent<task_editing_window_behavior>().Unminimizer.onClick.RemoveAllListeners();
            editor_window.GetComponent<task_editing_window_behavior>().Unminimizer.onClick.AddListener(editor_window.GetComponent<task_editing_window_behavior>().toggle_minimize);
            task_edit_button.onClick.RemoveAllListeners();
            task_edit_button.onClick.AddListener(editor_window.GetComponent<task_editing_window_behavior>().toggle_minimize);
            editor_window.GetComponent<task_editing_window_behavior>().parent = window_holder;
            editor_window.transform.localPosition = new Vector3(parent_task.GetComponent<task_data>().editor_window.transform.localPosition.x-25f, parent_task.GetComponent<task_data>().editor_window.transform.localPosition.y, 0f);
            editor_window.transform.localScale = Vector3.one;
            Debug.Log("replacement window fully created");
            force_open();
        }
    }

    #region subtasks

    public void toggle_Subtask_appearance()
    {
        //make it change the object and set that object's data to match instead of toggling an object in the task. otherwise it would cause lots of lag
    }

    public void make_Into_Subtask()
    {
        //this is going to be for the button that lets you make things into subtasks of other things as if they were sections
        //it's also going to be for making it into the subtask of something when you drag it into it.
    }
    
    public void assign_Position_value(int list, int place)
    {
        Place_in_task_list[list] = place;
    }

    #endregion
}
