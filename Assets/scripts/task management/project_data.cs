using System;
using System.Collections.Generic;
using UnityEngine;

public class project_data : MonoBehaviour
{
    public GameObject area_tasks_go_in;
    public GameObject area_selection_button;
    public List<GameObject> list_of_tasks;
    public void set_as_active_project()
    {
        project_manager.current.switch_active_project(this.gameObject);
    }
}

