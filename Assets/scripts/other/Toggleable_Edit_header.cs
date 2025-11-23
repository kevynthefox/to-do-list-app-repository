using TMPro;
using UnityEngine;

public class Toggleable_Edit_header : MonoBehaviour
{
    public TMP_InputField text_editable;
    //public TextMeshProUGUI text_uneditable;
    public bool text_editable_;
    

    public void toggle_editing()
    {
        text_editable_ = !text_editable_;

        text_editable.interactable = text_editable_;

        //text_editable.gameObject.SetActive(text_editable_);
        //text_uneditable.gameObject.SetActive(!text_editable_);

        //assign_name();
    }

    /*public void assign_name()
    {
        //text_uneditable.text = text_editable.text;
        text_editable.readOnly = true;
    }*/
}
