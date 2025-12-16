using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TMPro.EditorUtilities;

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
    public TMP_InputField date_input_text;
     
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
    public List<GameObject> position_obj_refList;
    public int position_current;
    [Header("animation")]
    //public Rigidbody2D rb;
    public float move_speed;
    public bool intermediate;
    public Vector2 track_1;
    public Vector2 track_2;
    public List<GameObject> position_button_refList;
    
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

    public void assign_Position_value(int list_list, int place)//,GameObject source_obj,string source_name)
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
    public void position_data_changer(string obj,int current_list,int new_pos_1,int new_pos_2,int new_pos_3,int new_pos_4,GameObject obj_reference)
    {
        string a = new_pos_1.ToString();
        string b = new_pos_2.ToString();
        string c = new_pos_3.ToString();
        string d = new_pos_4.ToString();
        //Debug.Log("c" + c);
        //Debug.Log("d" + d);
        if (position_obj_refList.Contains(obj_reference) == false)
        {
            position_obj_refList.Add(obj_reference);
        }
        int id = positions_obj_name.IndexOf(obj);
        position_current = id;
        
        if (positions_1[id].Count() >= current_list + 1)
        {
            positions_1[id].Insert(current_list,a);
            positions_2[id].Insert(current_list,b);
            positions_3[id].Insert(current_list,c);
            positions_4[id].Insert(current_list,d);
            //Debug.Log("was greater than or equal to current list. changing column values");
        }

        //adds another sorting method if that method isn't already in the list.
        if (positions_1[id].Count() < current_list + 1)
        {
            positions_3[id] = positions_1[id] + c;//I do not know why, but having it be 3,4,1,2 instead of 1,2,3,4 fixes the issue where there are too many numbers
            positions_4[id] = positions_1[id] + d;
            positions_1[id] = positions_1[id] + a;
            positions_2[id] = positions_2[id] + b;
            
            //Debug.Log("was less than current list. adding next column");
        }
        //if that sorting method is in the list, you can change the part you are trying to change.
        
        //Debug.Log("positions_1[id].Count(): " + positions_1[id].Count());
        if ( positions_1[id].Count() != current_list + 1)
        {
            //position_data_changer(obj,current_list,new_pos_1,new_pos_2,new_pos_3,new_pos_4); Debug.Log("redoing the data change");
            repeat_pos_data_change(obj,current_list,new_pos_1,new_pos_2,new_pos_3,new_pos_4,obj_reference);
        }
    }
    
    public void repeat_pos_data_change(string a, int b,int c,int d, int e,int f,GameObject g)
    {
        //yield return new WaitForSeconds(0.1f);
        position_data_changer(a,b,c,d,e,f,g);
        //Debug.Log("redoing the pos data change");
    }
    

    #endregion

    #region animation
    

    public void start_animaiton(bool square_or_natural,bool xfir_yfir,GameObject objective_1,GameObject objective_2)
    {
        //turn_on_for_animating_purposes();
        StartCoroutine(seek(square_or_natural, xfir_yfir, objective_1, objective_2));
    }

    public void turn_on_for_animating_purposes()
    {
        transform.SetParent(object_holder.current.transform);
        this.gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }

    public IEnumerator seek(bool square_or_natural,bool xfir_yfir,GameObject objective_1,GameObject objective_2)
    {
        intermediate = settings_controller.current.intermediate;
        GameObject current_objective;
        bool initial = true;
        float initial_dir_x = 420420;
        float initial_dir_y = -6969;
        move_speed = settings_controller.current.move_speed;
        StartCoroutine(count_time_1());
        StartCoroutine(count_time_2());
        while (time_controller.current.starter == true)
        {
            if (intermediate == true)
            {
                current_objective = objective_1;
            }
            else
            {
                current_objective = objective_2;
                initial = true;
            }

            if (square_or_natural == true)
            {
                
                if (track_1 == track_2)//detects when movement stops.
                {
                    if (current_objective == objective_1)
                    {
                        intermediate = false;
                        StopCoroutine(count_time_1());
                        StopCoroutine(count_time_2());
                        track_1.x = 999;
                        track_2.y = -888888;
                        StartCoroutine(count_time_1());
                        StartCoroutine(count_time_2());
                    }
                    else
                    {
                        Destroy(current_objective);
                        transform.parent.GetComponent<VerticalLayoutGroup>().enabled = true;
                    }
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position,current_objective.transform.position,move_speed * Time.deltaTime);
                    
                }
                
            }
            else
            {
                float Direction_x = Mathf.Sign(current_objective.transform.position.x - transform.position.x);
                float Direction_y = Mathf.Sign(current_objective.transform.position.y - transform.position.y);
                if (Mathf.Abs(current_objective.transform.position.x - transform.position.x) < 1)
                {
                    Direction_x = 0;
                }
                if (Mathf.Abs(current_objective.transform.position.y - transform.position.y) < 1)
                {
                    Direction_y = 0;
                }
                
                if (initial == true)
                {
                    initial_dir_x = Direction_x;
                    initial_dir_y = Direction_y;
                    initial = false;
                }
                //Debug.Log(initial_dir_y);
                Debug.Log("dir y: " + Direction_y);
                Debug.Log("dir x: " + Direction_x);
                if (xfir_yfir == true)
                {
                    
                    if (initial_dir_y > 0)
                    {
                        if (0 < Direction_y)
                        {
                            
                            Vector3 MovePos = new Vector3(
                                transform.position.x,
                                transform.position.y + Direction_y * move_speed * Time.deltaTime, //MoveTowards on 1 axis
                                transform.position.z
                            );
                            transform.position = MovePos;
                        }
                        
                    }
                    if (initial_dir_y < 0)
                    {
                        if (0 > Direction_y)
                        {
                            
                            Vector3 MovePos = new Vector3(
                                transform.position.x,
                                transform.position.y + Direction_y * move_speed * Time.deltaTime, //MoveTowards on 1 axis
                                transform.position.z
                            );
                            transform.position = MovePos;
                        }
                        
                    }
                    
                    if (0 == Direction_y)
                    {
                        //float Direction = Mathf.Sign(current_objective.transform.position.x - transform.position.x);
                        Vector3 MovePos = new Vector3(
                            transform.position.x + Direction_x * move_speed * Time.deltaTime, //MoveTowards on 1 axis
                            transform.position.y,
                            transform.position.z
                        );
                        if (0 != Direction_x)
                        {
                            transform.position = MovePos;
                        }
                        else
                        {
                            if (current_objective != objective_1)
                            {
                                Destroy(current_objective);
                                transform.parent.GetComponent<VerticalLayoutGroup>().enabled = true;
                            }
                            yield return new WaitForSeconds(1f);
                            intermediate = false;
                        }
                    }
                
                    
                }
                else
                {
                    if (initial_dir_x > 0)
                    {
                        if (0 < Direction_x)
                        {
                            //float Direction = Mathf.Sign(current_objective.transform.position.x - transform.position.x);
                            Vector3 MovePos = new Vector3(
                                transform.position.x + Direction_x * move_speed * Time.deltaTime, //MoveTowards on 1 axis
                                transform.position.y,
                                transform.position.z
                            );
                            transform.position = MovePos;
                        }
                        
                    }
                    if (initial_dir_x < 0)
                    {
                        if (0 > Direction_x)
                        {
                            //float Direction = Mathf.Sign(current_objective.transform.position.x - transform.position.x);
                            Vector3 MovePos = new Vector3(
                                transform.position.x + Direction_x * move_speed * Time.deltaTime, //MoveTowards on 1 axis
                                transform.position.y,
                                transform.position.z
                            );
                            transform.position = MovePos;
                        }
                        
                    }
                    if (0 == Direction_x)
                    {
                        //float Direction = Mathf.Sign(current_objective.transform.position.y - transform.position.y);
                        Vector3 MovePos = new Vector3(
                            transform.position.x,
                            transform.position.y + Direction_y * move_speed * Time.deltaTime, //MoveTowards on 1 axis
                            transform.position.z
                        );
                        if (0 != Direction_y)
                        {
                            transform.position = MovePos;
                        }
                        else
                        {
                            if (current_objective != objective_1)
                            {
                                Destroy(current_objective);
                                transform.parent.GetComponent<VerticalLayoutGroup>().enabled = true;
                            }
                            yield return new WaitForSeconds(1f);
                            intermediate = false;
                        }
                    }
                }
            }

            yield return new WaitForSeconds(.01f);
        }

    }
    private IEnumerator count_time_1()
    {
        while (time_controller.current.starter == true)
        {
            track_1 = transform.position;
            yield return new WaitForSecondsRealtime(2f);
        }
    }
    private IEnumerator count_time_2()
    {
        yield return new WaitForSecondsRealtime(1f);
        while (time_controller.current.starter == true)
        {
            track_2 = transform.position;
            yield return new WaitForSecondsRealtime(3f);
        }
    }

    #endregion

    #region date stuff
    public void Set_day_with_text()
    {
        my_date = date_input_text.text;
    }

    public void Set_today()
    {
        Set_day(DateTime.Today);        
    }

    public void Set_tomorrow()
    {
        Set_day(DateTime.Today.AddDays(1));
    }

    public void Set_noDate()
    {
        my_date = null;
    }

    public void Set_later_this_week()
    {
        //Set_day(DateTime.)
    } 

    public void Set_day(DateTime day)
    {
        string date_holder_1;
        if (settings_controller.current.date_type == false)
        {
            date_holder_1 = day.ToString("dd/MM/yyyy");
            my_date = date_holder_1.Substring(0,10);
        }
        if (settings_controller.current.date_type == true)
        {
            date_holder_1 = day.ToString("MM/dd/yyyy");
            my_date = date_holder_1.Substring(0,10);
        }
    }

    public void Set_day_direct(int day,int month, int year)
    {
        //string date_holder_1;
        if (settings_controller.current.date_type == false)
        {
            //date_holder_1 = day.ToString("dd/MM/yyyy");
            //my_date = date_holder_1.Substring(0,10);
            my_date = day.ToString("00") + "/" + month.ToString("00") + "/" + year.ToString();
        }
        if (settings_controller.current.date_type == true)
        {
            //date_holder_1 = day.ToString("MM/dd/yyyy");
            //my_date = date_holder_1.Substring(0,10);
            my_date = month.ToString("00") + "/" + day.ToString("00") + "/" + year.ToString();
        }
    }

    public void Set_dateTime(DateTime dateTime)
    {
        //string date_holder_1;
        if (settings_controller.current.date_type == false)
        {
            my_date = dateTime.ToString("dd/MM/yyyy");
            //my_date = date_holder_1.Substring(0,10);
        }
        if (settings_controller.current.date_type == true)
        {
            my_date = dateTime.ToString("MM/dd/yyyy");
            //my_date = date_holder_1.Substring(0,10);
        }
    }
    #endregion
}

