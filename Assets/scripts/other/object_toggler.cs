using UnityEngine;

public class object_toggler : MonoBehaviour
{
    public bool toggle;
    public GameObject Object;
    public void toggling()
    {
        toggle = !toggle;
        Object.SetActive(toggle);
    }
}
