using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class task_data : MonoBehaviour
{
    public TextMeshProUGUI main_text;
    public TextMeshProUGUI description;


    public string main_textString;
    public string descriptionString;

    public TextMeshProUGUI header_editor;
    public TextMeshProUGUI body_editor;

    public GameObject editor_window;
    public bool minimize;
    public bool isOpen;

    
    void Start()
    {
        main_text.text = main_textString;
        description.text = descriptionString;
    }

    
    void Update()
    {
        main_text.text = main_textString;
        description.text = descriptionString;

        minimize = editor_window.GetComponent<task_editing_window_behavior>().minimize;
        isOpen = editor_window.GetComponent<task_editing_window_behavior>().isOpen;

        if ((minimize == false) && (isOpen == true))
        {
            main_text.gameObject.SetActive(false);
            description.gameObject.SetActive(false);
        }
        else
        {
            main_text.gameObject.SetActive(true);
            description.gameObject.SetActive(true);
        }
    }

    public void updateHeader()
    {
        main_textString = header_editor.text;
    }
    public void updateBody()
    {
        descriptionString = body_editor.text;
    }
}
