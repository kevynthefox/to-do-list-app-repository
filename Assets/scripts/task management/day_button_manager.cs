using System;
using System.Collections.Generic;
using UnityEngine;

public class day_button_manager : MonoBehaviour
{
    public List<GameObject> day_buttons;
    public int day_output;

    public void toggle_optional_days(int year_, int month_)
    {
        if (DateTime.DaysInMonth(year_,month_) >= 29)
        {
            day_buttons[29].SetActive(true);
            if (DateTime.DaysInMonth(year_,month_) >= 30)
            {
                day_buttons[30].SetActive(true);
                if (DateTime.DaysInMonth(year_,month_) >= 31)
                {
                    day_buttons[31].SetActive(true);
                }
                else
                {
                    day_buttons[31].SetActive(false);
                }
            }
            else
            {
                day_buttons[30].SetActive(false);
                day_buttons[31].SetActive(false);
            }
        }
        else
        {
            day_buttons[29].SetActive(false);
            day_buttons[30].SetActive(false);
            day_buttons[31].SetActive(false);
        }
    }

    public void button_func(GameObject button)
    {
        day_output = day_buttons.IndexOf(button);
    }
}
