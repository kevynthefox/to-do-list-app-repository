using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;


public class task_creation : MonoBehaviour
{
    public static task_creation current;

    //public GameObject basic_task_prefab;
    public List<GameObject> area_tasks_go_in;
    public int current_area;

    [Header("tasks")]
    public List<GameObject> task_list;

    public int typeOf_Subtask; //0 is regular task or a subtask, 1 is comment, 2 is a project(sub project i guess), 3 is a section.
    public GameObject parent_task;

    [Header("projects")]

    public GameObject task_add_button;

    void Awake()
    {
        current = this;  
    }

    /*public void perform_creation()
    {
        if (typeOf_Subtask == 0)
        {
            create_task();
        }
        if (typeOf_Subtask == 1)
        {
            
        }
        if (typeOf_Subtask == 2)
        {
            create_new_project();
        }
        if (typeOf_Subtask == 3)
        {
            
        }
    }*/

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

    public void create_new_project()
    {
        var new_project = Instantiate(object_holder.current.Reference_project_object,Vector3.zero, quaternion.identity);
        new_project.transform.SetParent(time_controller.current.transform);
        new_project.transform.localScale = Vector3.one;
        new_project.transform.localPosition = new Vector3(130f,new_project.transform.localPosition.y,0f);
        
        var new_button = new_project.GetComponent<project_data>().area_selection_button;
        new_button.transform.SetParent(area_tasks_go_in[current_area].transform);
        new_button.transform.localScale = Vector3.one;
        new_button.transform.localPosition = new Vector3(new_button.transform.localPosition.x,new_button.transform.localPosition.y,0f);

        task_add_button.GetComponent<task_creation>().area_tasks_go_in.Add(new_project.GetComponent<project_data>().area_tasks_go_in);
        project_manager.current.projects.Add(new_project);
        project_manager.current.project_buttons.Add(new_button);
        //new_button.GetComponent<Button>().onClick.AddListener(new_project.GetComponent<project_data>().set_as_active_project);
        //new_button.GetComponent<Button>().onClick.AddListener(new_project.GetComponent<project_data>().set_as_active_project);
        //i am instead going to simply 'strip' the button from the project because it is too confusing to set its action
    }

    public void create_new_section()
    {
        var new_button = Instantiate(object_holder.current.section_prefab, Vector3.zero, Quaternion.identity);
        new_button.transform.SetParent(area_tasks_go_in[current_area].transform);
        new_button.transform.localScale = Vector3.one;
        new_button.transform.localPosition = new Vector3(new_button.transform.localPosition.x,new_button.transform.localPosition.y,0f);

    }

}
