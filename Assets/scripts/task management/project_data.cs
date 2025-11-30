using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class project_data : MonoBehaviour
{
    public string project_name;
    public TMP_InputField name_changer;

    public GameObject area_tasks_go_in;
    public GameObject area_selection_button;
    public List<GameObject> list_of_tasks;
    public List<GameObject> tasks_that_are_in_here;

    public List<GameObject> display_list;
    public int current_list;
    public int method_exception_count; //this is the number used for when you switch your current list but the task doesn't have a place in that list.
    public void set_as_active_project()
    {
        project_manager.current.switch_active_project(this.gameObject);
    }

    /*public void return_tasks()
    {
        tasks_that_are_in_here.Clear();
        for (int i = 0; i < area_tasks_go_in.transform.childCount; i++)
        {
            tasks_that_are_in_here.Add(area_tasks_go_in.transform.GetChild(i).gameObject);
        }
        foreach (GameObject task in list_of_tasks)
        {
            if (tasks_that_are_in_here.Contains(task) == false)
            {
                task.transform.parent = area_tasks_go_in.transform;
            }
        }
    }*/

    public void arrange_tasks()
    {
        display_list.Clear();
        foreach (GameObject task in list_of_tasks)
        {
            //inserts the task into the current list based on what the task says its position in that list is.
            if (task.TryGetComponent<task_data>(out task_data task_))
            {
                int index_of_this_list_in_task = task_.positions_obj_name.IndexOf(project_name/* + "_" +lists[current_list]*/);//example: october25th smart sort //the list part is already handeled by the other digits in the string i thnk.
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

                if (task_.positions_1[task_.positions_obj_name.IndexOf(project_name)].Count() < current_list+1)
                {
                    string Mec = method_exception_count.ToString("D4");
                    Debug.Log("Mec" + Mec);
                    int Mec_a = int.Parse(Mec.Substring(0, 1));
                    int Mec_b = int.Parse(Mec.Substring(1, 1));
                    int Mec_c = int.Parse(Mec.Substring(2, 1));
                    int Mec_d = int.Parse(Mec.Substring(3, 1));
                    
                    task_.position_data_changer(project_name,current_list,Mec_a,Mec_b,Mec_c,Mec_d);
                    
                    method_exception_count++; 
                }
                
                string a = task_.positions_1[index_of_this_list_in_task].Substring(0 +current_list,1);//startup_val +current_list);
                string b = task_.positions_2[index_of_this_list_in_task].Substring(0 +current_list,1);//startup_val +current_list);
                string c = task_.positions_3[index_of_this_list_in_task].Substring(0 +current_list,1);//startup_val +current_list);
                string d = task_.positions_4[index_of_this_list_in_task].Substring(0 +current_list,1);//startup_val +current_list);
                //

                string index_p1 = a+b+c+d;
                

                /*Debug.Log(task_.header_textString + " " + task_.positions_obj_name[index_of_this_list_in_task] + " pos1: " + a + " pos2: " + b
                + " pos3: " + c + " pos4: " + d + " full pos: " + index_p1);*/

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
                task.transform.SetParent(area_tasks_go_in.transform);
                task.transform.localPosition = Vector3.zero;
            }
        }
    }

    public void change_project_name()
    {
        string previous_name = project_name;
        project_name = name_changer.text;

        if (list_of_tasks.Count > 0)
        {
            foreach (GameObject task in list_of_tasks)
            {
                if (task.TryGetComponent<task_data>(out task_data data))
                {
                    data.positions_obj_name[data.positions_obj_name.IndexOf(previous_name)] = project_name; 
                }
            }
        }
    }
}

