using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class day_button_manager : MonoBehaviour
{
    public List<GameObject> day_buttons;
    public List<GameObject> blanks;
    public List<int> blank_states;
    public int day_output;
    public GameObject task_;
    public int year_;
    public int month_;
    DateTime day_;
    public TextMeshProUGUI MonthYear_text;

    
    public List<GameObject> blanks_to_turn_on;

    public void OnEnable()
    {
        year_ = DateTime.Now.Year;
        month_ = DateTime.Now.Month;
        Update_month_and_year();
    }

    public void year_up()
    {
        year_+=1;
        Update_month_and_year();
    }
    public void year_down()
    {
        year_-=1;
        Update_month_and_year();
    }

    public void month_right()
    {
        month_+=1;
        Update_month_and_year();
    }
    public void month_left()
    {
        month_-=1;
        Update_month_and_year();
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
            
            
            if (month_ <= 0)
            {
                year_ -=1;
                month_ = 12;
                month_name = "December";
            }
            if (month_ >= 13)
            {
                year_ +=1;
                month_ = 1;
                month_name = "January";
            }
            
        }
        MonthYear_text.text = year_ + "<br>" + month_ + "|" + month_name;
    }

    public void Update_month_and_year()
    {
        update_MonthYear_text();
        toggle_optional_days(year_,month_);
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

        
        DateTime day_1 = new DateTime(year_,month_,1);
        if (day_1.DayOfWeek == DayOfWeek.Sunday)
        {}
        if (day_1.DayOfWeek == DayOfWeek.Monday)
        {
            blanks_to_turn_on.Add(blanks[1]);
        }
        if (day_1.DayOfWeek == DayOfWeek.Tuesday)
        {
            blanks_to_turn_on.Add(blanks[1]);
            blanks_to_turn_on.Add(blanks[2]);
        }
        if (day_1.DayOfWeek == DayOfWeek.Wednesday)
        {
            blanks_to_turn_on.Add(blanks[1]);
            blanks_to_turn_on.Add(blanks[2]);
            blanks_to_turn_on.Add(blanks[3]);
        }
        if (day_1.DayOfWeek == DayOfWeek.Thursday)
        {
            blanks_to_turn_on.Add(blanks[1]);
            blanks_to_turn_on.Add(blanks[2]);
            blanks_to_turn_on.Add(blanks[3]);
            blanks_to_turn_on.Add(blanks[4]);
        }
        if (day_1.DayOfWeek == DayOfWeek.Friday)
        {
            blanks_to_turn_on.Add(blanks[1]);
            blanks_to_turn_on.Add(blanks[2]);
            blanks_to_turn_on.Add(blanks[3]);
            blanks_to_turn_on.Add(blanks[4]);
            blanks_to_turn_on.Add(blanks[5]);
        }
        if (day_1.DayOfWeek == DayOfWeek.Saturday)
        {
            blanks_to_turn_on.Add(blanks[1]);
            blanks_to_turn_on.Add(blanks[2]);
            blanks_to_turn_on.Add(blanks[3]);
            blanks_to_turn_on.Add(blanks[4]);
            blanks_to_turn_on.Add(blanks[5]);
            blanks_to_turn_on.Add(blanks[6]);
        }
        foreach (GameObject blank in blanks_to_turn_on)
        {
            blank_states[blanks.IndexOf(blank)] = 2;
        }
        foreach (GameObject blank in blanks)
        {
            if (blank != null)
            {
                if (blank_states[blanks.IndexOf(blank)] == 0)
                {
                    blank.SetActive(false);
                }
                if (blank_states[blanks.IndexOf(blank)] == 1)
                {
                    blank.SetActive(false);
                }
                if (blank_states[blanks.IndexOf(blank)] == 2)
                {
                    blank.SetActive(true);
                    blank_states[blanks.IndexOf(blank)] = 1;
                }
            }
        }

        blanks_to_turn_on.Clear();
    }

    public void button_func(GameObject button)
    {
        day_output = day_buttons.IndexOf(button);
        task_.GetComponent<task_data>().Set_day_direct(day_output,month_,year_);
    }
}
