using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class task_creation : MonoBehaviour
{
    public static task_creation current;

    //public GameObject basic_task_prefab;
    public List<GameObject> area_tasks_go_in;
    public int current_area;

    public List<GameObject> task_list;

    public int typeOf_Subtask; //0 is regular task as a subtask, 1 is comment, 2 is a project(sub project i guess), 3 is a section.
    public GameObject parent_task;

    void Awake()
    {
        current = this;  
    }

    public void create_task()
    {
        var new_task = Instantiate(object_holder.current.task_prefab, Vector3.zero, Quaternion.identity);
        new_task.transform.SetParent(area_tasks_go_in[current_area].transform); 
        if (parent_task != null)
        {
            parent_task.GetComponent<task_data>().sub_tasks.Add(new_task);
            parent_task.GetComponent<task_data>().subtask_tracker();
            new_task.GetComponent<task_data>().assign_Position_value(0,parent_task.GetComponent<task_data>().sub_tasks.Count);
            new_task.GetComponent<task_data>().parent_task = parent_task;
        }
        new_task.transform.localScale = Vector3.one;
        new_task.transform.localPosition = new Vector3(new_task.transform.localPosition.x, new_task.transform.localPosition.y, 0f);
        new_task.GetComponent<task_data>().force_open();
        task_list.Add(new_task);
    }

}
