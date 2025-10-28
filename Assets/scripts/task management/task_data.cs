using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class task_data : MonoBehaviour
{
    public TextMeshProUGUI main_text;
    public TextMeshProUGUI description;


    public string main_textString;
    public string descriptionString;

    
    void Start()
    {
        main_text.text = main_textString;
        description.text = descriptionString;
    }

    
    void Update()
    {
        main_text.text = main_textString;
        description.text = descriptionString;
    }
}
