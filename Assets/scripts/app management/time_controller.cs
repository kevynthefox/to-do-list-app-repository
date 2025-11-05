using UnityEngine;

public class time_controller : MonoBehaviour
{
    public static time_controller current;
    public bool starter;

    public void Awake()
    {
        current = this;
    }
}
