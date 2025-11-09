using System.Collections.Generic;
using UnityEngine;


public class task_creation : MonoBehaviour
{
    public GameObject basic_task_prefab;
    public List<GameObject> area_tasks_go_in;
    public int current_area;

    public List<GameObject> task_list;

    public void create_task()
    {
        var new_task = Instantiate(basic_task_prefab, Vector3.zero, Quaternion.identity);
        new_task.transform.SetParent(area_tasks_go_in[current_area].transform);
        new_task.transform.localScale = Vector3.one;
        new_task.transform.localPosition = new Vector3(new_task.transform.localPosition.x, new_task.transform.localPosition.y, 0f);
        task_list.Add(new_task);
    }

}
