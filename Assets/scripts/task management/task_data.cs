using System;
using System.Collections.Generic;
using TMPro;
//using TMPro.EditorUtilities;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class task_data : MonoBehaviour
{
    [Header("text")]
    public TextMeshProUGUI header_text;
    public TextMeshProUGUI description;


    public string header_textString;
    public string descriptionString;

    [Header("edit window")]
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

    [Header("subtask stuff")]
    public GameObject parent_task;
    //public toDo_task self_data_representation; // the data representation of this task.
    public List<int> Place_in_task_list; //use the number in the [] to retrieve the type of list. use the number it spits out as where that task goes in that list.
    //list type(priority sort, etc) reference table:
    /// <summary>
    /// list 0 = default, unsorted list.
    /// list 1 is p1
    /// list 2 is p2
    /// list 3 is p3
    /// list 4 is p4
    /// list 5 is p5
    /// list 6 is p6
    /// list 7 is p7
    /// list 8 is p8
    /// </summary>
    public int typeOf_Subtask; //0 is regular task as a subtask, 1 is comment, 2 is a project(possibly even a sub project i guess), 3 is a section
    public List<GameObject> sub_tasks;
    public List<GameObject> completed_subTasks;
    public List<GameObject> failed_subTasks;


    public GameObject subtask_displayer;
    public GameObject subtask_completion_displayer;
    public GameObject subtask_failure_displayer;
    public TextMeshProUGUI subtask_totalText_displayer;
    public TextMeshProUGUI bracket;
    public TextMeshProUGUI subtask_completionText_displayer;
    public TextMeshProUGUI subtask_failedText_displayer;

    [Header("priority stuff")]

    public int priority; //1-8. 1 is red, 2 is orange, etc. 8 is white and also the default.
    public TMP_Dropdown priority_changer;
    public List<GameObject> sort_areas;

    [Header("date stuff")]
    //public bool wee;
    public string my_date;
     
    public List<string> positions_obj_name; //get the index of the object you are trying to retrieve and that index will be the position in the list(vertically not horizontally) that you need to get the thing.
    public List<string> positions_1; 
    public List<string> positions_2; 
    public List<string> positions_3;
    public List<string> positions_4;
    /// <summary>
    /// this stores the positions of each task in each list. it does this like this "[task 1: pos 3], [task 2: pos 5]"
    /// revision: the first digit is stored in pos 1, second is stored in pos 2. it would work as follows:
    /// spot in the list 1:(today for example)
    /// 01
    /// 03
    /// 02
    /// 13
    /// left column would be smart sort as an example, and right would be priority or whatever.
    /// spot in the list 2: ()
    /// 22
    /// 36
    /// 54
    /// 19
    /// output at position 1 is 01, output at position 2 is 1323.
    /// it is HIGHLY unlikely that they will even have more than 100 tasks in a day. but if they are just using them as notes throughout the day or are doing stuff
    /// for a business, then they might have a few hundred tasks. anything over 1000 is extremely unlikely, if you have over 9999 tasks then i am sorry, but you
    /// probably also are one of those people who buys 50 watermelons and must fit them within the volume of their trunk. in otherwords, you probably do not exist. :p
    /// </summary>
    void Start()
    {
        header_text.text = header_textString;
        description.text = descriptionString;
        if (priority == 0) { priority = 8; } //sets the priority to 8 if it is at 0 since 8 is supposed to be the default.

        ////self_data_representation = Instantiate(object_holder.current.toDo_Task_reference, Vector3.zero, Quaternion.identity);
        //sets the self representation in the data to be the last one in the list(which is where the one we want is)
        //self_data_representation = object_holder.current.global_task_list[object_holder.current.global_task_list.Count -1];
        ///finish after you make task into
    }


    void Update()
    {
        header_text.text = header_textString;
        description.text = descriptionString;

        minimize = editor_window.GetComponent<task_editing_window_behavior>().minimize;
        isOpen = editor_window.GetComponent<task_editing_window_behavior>().isOpen;

        if ((minimize == false) && (isOpen == true))
        {
            header_text.gameObject.SetActive(false);
            description.gameObject.SetActive(false);
        }
        else
        {
            header_text.gameObject.SetActive(true);
            description.gameObject.SetActive(true);
        }
    }

    public void updateHeader()
    {
        header_textString = header_editor.text;
        subtask_tracker();
    }
    public void updateBody()
    {
        descriptionString = body_editor.text;
        subtask_tracker();
    }
    #region state toggling
    public void toggle_completion()
    {
        if (completion_toggle == false)
        {
            state = 1;
            failure_toggle = false;
            deletion_toggle = false;
            //parent_task.GetComponent<task_data>().completed_subTasks.Add(this.gameObject);
        }
        else
        {
            state = 0;
            //parent_task.GetComponent<task_data>().completed_subTasks.Remove(this.gameObject);
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
            //parent_task.GetComponent<task_data>().failed_subTasks.Add(this.gameObject);
        }
        else
        {
            state = 0;
            //parent_task.GetComponent<task_data>().failed_subTasks.Remove(this.gameObject);
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
            //parent_task.GetComponent<task_data>().sub_tasks.Remove(this.gameObject);
        }
        else
        {
            state = 0;
            //parent_task.GetComponent<task_data>().sub_tasks.Add(this.gameObject);

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
        /*if (editor_window != parent_task.GetComponent<task_data>().editor_window)
        {*/
        Debug.Log("force open worked");
        editor_window.GetComponent<task_editing_window_behavior>().toggle_open();
        /*}
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
        }*/
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

    public void assign_Position_value(int list_list, int place)
    {
        Place_in_task_list[list_list] = place;
        subtask_tracker();
    }

    //this will be for when you remove and put back in things like deleted tasks. for now though, you aren't doing that.
    /*public void move_position(int list_list, int place)
    {
        if (parent_task.GetComponent<task_data>().Place_in_task_list[list_list] == 0) //this will be expanded for all lists
        { 
            parent_task.GetComponent<task_data>().sub_tasks
        }
    }*/

    public void subtask_tracker()
    {
        if (sub_tasks.Count > 0)
        {
            completed_subTasks.Clear();
            foreach (GameObject task in sub_tasks)
            {
                if (task.GetComponent<task_data>().state == 1)
                {
                    completed_subTasks.Add(task);
                }
            }
            failed_subTasks.Clear();
            foreach (GameObject task in sub_tasks)
            {
                if (task.GetComponent<task_data>().state == 2)
                {
                    failed_subTasks.Add(task);
                }
            }

            subtask_displayer.SetActive(true);
            if (completed_subTasks.Count > 0)
            {
                subtask_completion_displayer.SetActive(true);
                subtask_completionText_displayer.gameObject.SetActive(true);
                subtask_completionText_displayer.text = completed_subTasks.Count.ToString();
            }
            else
            {
                subtask_completion_displayer.SetActive(false);
                subtask_completionText_displayer.gameObject.SetActive(false);
            }
            if (failed_subTasks.Count > 0)
            {
                subtask_failure_displayer.SetActive(true);
                subtask_failedText_displayer.gameObject.SetActive(true);
                subtask_failedText_displayer.text = failed_subTasks.Count.ToString();
            }
            else
            {
                subtask_failure_displayer.SetActive(false);
                subtask_failedText_displayer.gameObject.SetActive(false);
            }

            if (completed_subTasks.Count > 0 || failed_subTasks.Count > 0)
            {
                subtask_totalText_displayer.text = "]/" + sub_tasks.Count.ToString();
                bracket.gameObject.SetActive(true);
            }
            else
            {
                subtask_totalText_displayer.text = sub_tasks.Count.ToString();
                bracket.gameObject.SetActive(false);
            }
        }
        else
        {
            subtask_displayer.SetActive(false);
        }
    }

    #endregion

    #region  priority
    public void priority_change()
    {
        priority =  priority_changer.value + 1;

        if (priority == 1)
        {
            this.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0, 0);
        }
        if (priority == 2)
        {
            this.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, .5f, 0);
        }
        if (priority == 3)
        {
            this.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 0);
        }
        if (priority == 4)
        {
            this.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 1f, 0);
        }
        if (priority == 5)
        {
            this.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 1f);
        }
        if (priority == 6)
        {
            this.GetComponent<UnityEngine.UI.Image>().color = new Color(.5f, 0, 1f);
        }
        if (priority == 7)
        {
            this.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0, 1f);
        }
        if (priority == 8)
        {
            this.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f);
        }
    }
    #endregion

    #region data

    

    #endregion

}

