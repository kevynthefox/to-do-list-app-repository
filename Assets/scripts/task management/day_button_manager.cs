using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class day_button_manager : MonoBehaviour
{
    public List<GameObject> day_buttons;
    public int day_output;
    public GameObject task_;
    public int year_;
    public int month_;
    DateTime day_;
    public TextMeshProUGUI MonthYear_text;

    public void OnEnable()
    {
        year_ = DateTime.Now.Year;
        month_ = DateTime.Now.Month;
        toggle_optional_days(year_,month_);
    }

    public void year_up()
    {
        year_+=1;
    }
    public void year_down()
    {
        year_-=1;
    }

    public void month_right()
    {
        month_+=1;
    }
    public void month_left()
    {
        month_-=1;
    }

    public void update_MonthYear_text()
    {
        string month_name = "debug";
        if (month_ == 1)
        {
            month_name = "January";
        }
        if (month_ == 2)
        {
            month_name = "Febuary";
        }
        if (month_ == 3)
        {
            month_name = "March";
        }
        if (month_ == 4)
        {
            month_name = "April";
        }
        if (month_ == 5)
        {
            month_name = "May";
        }
        if (month_ == 6)
        {
            month_name = "June";
        }
        if (month_ == 7)
        {
            month_name = "July";
        }
        if (month_ == 8)
        {
            month_name = "August";
        }
        if (month_ == 9)
        {
            month_name = "September";
        }
        if (month_ == 10)
        {
            month_name = "October";
        }
        if (month_ == 11)
        {
            month_name = "November";
        }
        if (month_ == 12)
        {
            month_name = "December";
        }
        if (month_ <= 0 || month_ >= 13)
        {
            month_name = "OH GOD WHAT HAVE YOU DONE!?";
        }
        MonthYear_text.text = year_ + "/n" + month_ + "|" + month_name;
    }

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
        task_.GetComponent<task_data>().Set_day_direct(day_output,month_,year_);
    }
}
