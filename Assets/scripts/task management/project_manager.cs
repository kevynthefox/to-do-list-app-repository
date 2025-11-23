using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class project_manager : MonoBehaviour
{
    public static project_manager current;

    public List<GameObject> projects;
    public List<GameObject> project_buttons;

    public int current_area;
    public GameObject add_task_button;

    [Header("date lists")] //things like "today", "upcoming"
    public List<string> days; // search for the day you are looking for and then 
    public List<toDo_date> day_lists; //?


    
    public void Awake()
    {
        current = this;
    }



    public void switch_active_project(GameObject new_active_project)
    {
        current_area = projects.IndexOf(new_active_project);
        add_task_button.GetComponent<task_creation>().current_area = current_area;

        foreach (GameObject project in projects)
        {
            if (project == new_active_project)
            {
                //I am setting things active or inactive because the projects are likely to have tons of children and it would lag the app a lot
                //if all of the children were always active.
                project.SetActive(true);
                project_buttons[projects.IndexOf(project)].transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                project.SetActive(false);
                project_buttons[projects.IndexOf(project)].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    
}
///with all of this you should be able to do things like "get this task in [project]/[section]/[task]"?
/// the toDo_ prefix is to distinguish from potential conflicts with the engine itself. like, if unity renders things in 'tasks' or whatever.
/// 
[Serializable]
public class toDo_project
{
    public string header;
    public List<toDo_section> sections;
    public List<toDo_task> tasks;
}

[Serializable]
public class toDo_section
{
    public string header;
    public List<toDo_task> tasks;
}

[Serializable]
public class toDo_task
{
    public GameObject self;

    [Header("text")]
    public string header;
    public string body;
    
    [Header("data")]
    public int priority;
    public int state; // 0 is neutral, 1 is complete, 2 is fail, 3 is delete
    public bool completion_toggle;
    public bool failure_toggle;
    public bool deletion_toggle;
    public int position_in_global_task_list; //this is not in subtasks because this is the reference for this object in the global list of all tasks.

    [Header("subtask data")]
    public List<toDo_task> subtasks;
    public toDo_task parent_task;
    public int position_in_parents_tasks_list;
    public List<string> my_locations; //a list that contains locations. you don't input anything, instead you search for it. example: "today" and you get "today [smartview] 1" or something like that/

    public void task_motion()
    {
        //and then you apply one of 2 animations based on the settings.
    }
    

    public void assign_info()
    {
        ////parent_task = self.GetComponent<task_data>().parent_task;
        /// finish this. you will need to do something with retrieving the parent task based on the gameobject,
        ///  or just assigning it somehow, potentially by getting notified from the parent task that a new task is being created.
        object_holder.current.global_task_list.Add(this);
        position_in_global_task_list = object_holder.current.global_task_list.IndexOf(this);

        parent_task.subtasks.Add(this);
        position_in_parents_tasks_list = parent_task.subtasks.IndexOf(this);
    }

    public void change_info()
    {   
        /*
        //object_holder.current.global_task_list.Add(this);
        //position_in_global_task_list = object_holder.current.global_task_list.IndexOf(this);
        //we do not need this /\ at this part, because the global list of tasks is not going to have the tasks moved around inside it.

        parent_task.subtasks.Add(this);
        position_in_parents_tasks_list = parent_task.subtasks.IndexOf(this);

        // wait actually we shouldn't need to update any info on the list end, the info should just update right? like, if you have a gameobject referenced by 2 lists,
        //when the object updates, both lists get updated right?
        */

        header = self.GetComponent<task_data>().main_textString;
        body = self.GetComponent<task_data>().descriptionString;
        priority = self.GetComponent<task_data>().priority;

        state = self.GetComponent<task_data>().state;
        completion_toggle = self.GetComponent<task_data>().completion_toggle;
        failure_toggle = self.GetComponent<task_data>().failure_toggle;
        deletion_toggle = self.GetComponent<task_data>().deletion_toggle;
    }

}

[Serializable]

public class toDo_date
{
    public GameObject self;
    public GameObject self_content;
    public DateTime date;
    public string day;
    public List<toDo_task> tasks_for_today;
    public List<int> lists;
    public List<string> positions_1; 
    public List<string> positions_2; 
    public List<string> positions_3;
    public List<string> positions_4;
    /// <summary>
    /// this stores the positions of each task in each list. it does this like this "[task 1: pos 3], [task 2: pos 5]"
    /// revision: the first digit is stored in pos 1, second is stored in pos 2. it would work as follows:
    /// 01
    /// 03
    /// 02
    /// 13
    /// output at position 1 is 01, output at position 2 is 1323.
    /// it is HIGHLY unlikely that they will even have more than 100 tasks in a day. but if they are just using them as notes throughout the day or are doing stuff
    /// for a business, then they might have a few hundred tasks. anything over 1000 is extremely unlikely, if you have over 9999 tasks then i am sorry, but you
    /// probably also are one of those people who buys 50 watermelons and must fit them within the volume of their trunk. in otherwords, you probably do not exist. :p
    /// </summary>
    public List<toDo_task> display_list;
    public int current_list;

    public void collect_tasks_for_today()
    {
        //foreach (toDo_task task in [insert grand global task list])
    }

    public void arrange_tasks()
    {
        foreach (toDo_task task in tasks_for_today)
        {
            string index_p1 = positions_1[lists[current_list]]+positions_2[lists[current_list]]+positions_3[lists[current_list]]+positions_4[lists[current_list]];
            int index_p2 = int.Parse(index_p1);

            Debug.Log(task.header + " pos1: " + positions_1[lists[current_list]] + " pos2: " + positions_2[lists[current_list]]
             + " pos3: " + positions_3[lists[current_list]] + " pos4: " + positions_1[lists[current_list]] + " full pos: " + index_p2);


            display_list.Insert(index_p2, task);
        }
        foreach (toDo_task task in display_list)
        {
            task.self.transform.parent = self_content.transform;
        }
    }
}