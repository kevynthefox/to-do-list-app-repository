using UnityEngine;

public class object_holder : MonoBehaviour
{
    public static object_holder current;


    public GameObject task_prefab;
    public GameObject section_prefab;
    public GameObject Reference_project_object;
    public GameObject Reference_project_button;

    public void Awake()
    {
        current = this;
    }
}
