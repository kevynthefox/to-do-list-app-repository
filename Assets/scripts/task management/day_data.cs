using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class day_data : MonoBehaviour
{
    public string my_date;

    public GameObject day_content;
    public List<GameObject> tasks_for_today;
    public List<string> lists;
    
    public List<GameObject> display_list;
    public int current_list;
    public int method_exception_count;
    /*public bool trigger_bc_imtoolasyforbutton;

    void Update()
    {
        if (trigger_bc_imtoolasyforbutton == true)
        {
            trigger_bc_imtoolasyforbutton = false;
            activate_day();
        }
    }*/

    public void collect_tasks_for_today()
    {
        //foreach (toDo_task task in [insert grand global task list])
        foreach (GameObject task in object_holder.current.global_task_list) //GameObject.FindGameObjectsWithTag("toDo_task"))
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
        display_list.Clear();
        foreach (GameObject task in tasks_for_today)
        {
            //inserts the task into the current list based on what the task says its position in that list is.
            if (task.TryGetComponent<task_data>(out task_data task_))
            {
                if (task_.positions_obj_name.Contains(my_date) == false)
                {
                    
                    
                    string p = tasks_for_today.IndexOf(task).ToString("D4");


                    task_.positions_obj_name.Add(my_date);
                    task_.positions_1.Add(p.Substring(0,1));
                    task_.positions_2.Add(p.Substring(1,1));
                    task_.positions_3.Add(p.Substring(2,1));
                    task_.positions_4.Add(p.Substring(3,1));
                }
                int index_of_this_list_in_task = task_.positions_obj_name.IndexOf(my_date/* + "_" +lists[current_list]*/);//example: october25th smart sort //the list part is already handeled by the other digits in the string i thnk.
                //this part makes sure it grabs only the correct character. task 1 is stored as the first digit, 2 as the second. for task 1 grab the first digit of each.
                
                /*int startup_val = 0;
                if (current_list == 0)
                {
                    startup_val = 1;
                }
                else
                {
                    startup_val = 0;
                }*/

                if (task_.positions_1[task_.positions_obj_name.IndexOf(my_date)].Count() < current_list+1)
                {
                    string Mec = method_exception_count.ToString("D4");
                    Debug.Log("mec" + Mec);
                    int Mec_a = int.Parse(Mec.Substring(0, 1));
                    int Mec_b = int.Parse(Mec.Substring(1, 1));
                    int Mec_c = int.Parse(Mec.Substring(2, 1));
                    int Mec_d = int.Parse(Mec.Substring(3, 1));
                    
                    task_.position_data_changer(my_date,current_list,Mec_a,Mec_b,Mec_c,Mec_d);
                    
                    method_exception_count++;
                }
                Debug.Log(task_.positions_1[task_.positions_obj_name.IndexOf(my_date)].Count());


                string a = task_.positions_1[index_of_this_list_in_task].Substring(0 +current_list,1);//startup_val +current_list);
                string b = task_.positions_2[index_of_this_list_in_task].Substring(0 +current_list,1);//startup_val +current_list);
                string c = task_.positions_3[index_of_this_list_in_task].Substring(0 +current_list,1);//startup_val +current_list);
                string d = task_.positions_4[index_of_this_list_in_task].Substring(0 +current_list,1);//startup_val +current_list);
                //

                string index_p1 = a+b+c+d;
                

                Debug.Log(task_.header_textString + " " + task_.positions_obj_name[index_of_this_list_in_task] + " pos1: " + a + " pos2: " + b
                + " pos3: " + c + " pos4: " + d + " full pos: " + index_p1);

                int index_p2 = int.Parse(index_p1);

                if (display_list.Count < index_p2)
                {
                    int amount_needed = index_p2 - display_list.Count;
                    for (int i = 0; i < amount_needed; i++)
                    {
                        display_list.Add(null);

                        //if for example you are trying to add to 59 and the list isn't 59 long, then this makes it 59 long.
                    }
                }
                display_list.Insert(index_p2, task);
            }
            //display_list.Insert(task.GetComponent<task_data>().obj[task.GetComponent<task_data>().position[current_list]],task);

        }

        foreach (GameObject task in display_list)
        {
            if (task != null)
            {
                task.transform.SetParent(day_content.transform);
                task.transform.localPosition = Vector3.zero;
            }
        }
    }
    

    public void activate_day()
    {
        tasks_for_today.Clear();
        collect_tasks_for_today();
        arrange_tasks();
    }
}
