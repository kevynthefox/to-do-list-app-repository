using UnityEngine;

public class day_button_presser : MonoBehaviour
{
    public void press_()
    {
        transform.parent.GetComponent<day_button_manager>().button_func(this.gameObject);
    }
}
