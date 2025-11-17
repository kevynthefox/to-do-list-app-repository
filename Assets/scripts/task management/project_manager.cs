using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class project_manager : MonoBehaviour
{
    public static project_manager current;

    public List<GameObject> projects;
    public List<GameObject> project_buttons;

    public int current_area;
    public GameObject add_task_button;

    public void Awake()
    {
        current = this;
    }



    public void switch_active_project(GameObject new_active_project)
    {
        current_area = projects.IndexOf(new_active_project);
        add_task_button.GetComponent<task_creation>().current_area = current_area;

        foreach (GameObject project in projects)
        {
            if (project == new_active_project)
            {
                //I am setting things active or inactive because the projects are likely to have tons of children and it would lag the app a lot
                //if all of the children were always active.
                project.SetActive(true);
                project_buttons[projects.IndexOf(project)].transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                project.SetActive(false);
                project_buttons[projects.IndexOf(project)].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
