using UnityEngine;

public class object_holder : MonoBehaviour
{
    public static object_holder current;


    public GameObject task_prefab;

    public void Awake()
    {
        current = this;
    }
}
