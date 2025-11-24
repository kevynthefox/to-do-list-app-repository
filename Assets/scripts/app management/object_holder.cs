using System.Collections.Generic;
using UnityEngine;

public class object_holder : MonoBehaviour
{
    public static object_holder current;


    public GameObject task_prefab;
    public GameObject section_prefab;
    public GameObject Reference_project_object;
    public GameObject Reference_project_button;
    public GameObject comment_prefab;

    //public toDo_task toDo_Task_reference;
    //public List<toDo_task> global_task_list;
    public List<GameObject> global_task_list;


    public void Awake()
    {
        current = this;
    }
}
