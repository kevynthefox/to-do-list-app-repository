using System;
using System.Collections.Generic;
using UnityEngine;

public class day_data : MonoBehaviour
{
    public string my_date;

    public GameObject day_content;
    public List<GameObject> tasks_for_today;
    public List<string> lists;
    
    public List<GameObject> display_list;
    public int current_list;
    public bool trigger_bc_imtoolasyforbutton;

    void Update()
    {
        if (trigger_bc_imtoolasyforbutton == true)
        {
            trigger_bc_imtoolasyforbutton = false;
            activate_day();
        }
    }

    public void collect_tasks_for_today()
    {
        //foreach (toDo_task task in [insert grand global task list])
        foreach (GameObject task in GameObject.FindGameObjectsWithTag("toDo_task"))
        {
            if (task.GetComponent<task_data>().my_date == my_date) //while this is going to have to check like, everything, it hopefully shouldn't be too slow?
            {
                if (tasks_for_today.Contains(task) == false)
                {
                    tasks_for_today.Add(task);
                }
            }
        }
    }

    public void arrange_tasks()
    {
        foreach (GameObject task in tasks_for_today)
        {
            //inserts the task into the current list based on what the task says its position in that list is.
            if (task.TryGetComponent<task_data>(out task_data task_))
            {
                int index_of_this_list_in_task = task_.positions_obj_name.IndexOf(my_date + "_" +lists[current_list]);//example: october25th smart sort
                //this part makes sure it grabs only the correct character. task 1 is stored as the first digit, 2 as the second. for task 1 grab the first digit of each.
                string a = task_.positions_1[index_of_this_list_in_task].Substring(0 +current_list,1 +current_list);
                string b = task_.positions_2[index_of_this_list_in_task].Substring(0 +current_list,1 +current_list);
                string c = task_.positions_3[index_of_this_list_in_task].Substring(0 +current_list,1 +current_list);
                string d = task_.positions_4[index_of_this_list_in_task].Substring(0 +current_list,1 +current_list);
                //

                string index_p1 = a+b+c+d;
                int index_p2 = int.Parse(index_p1);

                Debug.Log(task_.header_textString + " pos1: " + a + " pos2: " + b
                + " pos3: " + c + " pos4: " + d + " full pos: " + index_p2);


                display_list.Insert(index_p2, task);
            }
            //display_list.Insert(task.GetComponent<task_data>().obj[task.GetComponent<task_data>().position[current_list]],task);

        }

        foreach (GameObject task in display_list)
        {
            task.transform.SetParent(day_content.transform);
            task.transform.localPosition = Vector3.zero;
        }
    }
    

    public void activate_day()
    {
        collect_tasks_for_today();
        arrange_tasks();
    }
}
