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
    public List<GameObject> global_task_list;
    [Header("animation related things")]
    public GameObject blank_target_object_reference;
    public GameObject redirector;
    


    public void Awake()
    {
        current = this;
    }
}
