using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
        //glow_today();
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

    /*public void glow_today()
    {
        foreach (GameObject day in day_buttons)
        {
            if (day != null)
            {
                day.GetComponent<Image>().color = Color.white;
            }
        }

        string day_format = "0";
        if (settings_controller.current.date_type == false)
        {
            day_format =("dd/MM/yyyy");
        }
        if (settings_controller.current.date_type == true)
        {
            day_format =("MM/dd/yyyy");
        }
        
        string today = DateTime.Today.ToString(day_format);

        if (int.Parse(today.Substring(6, 4)) == year_)
        {
            if (settings_controller.current.date_type == false)
            {
                if (int.Parse(today.Substring(4, 2)) == month_)
                {
                    Debug.Log(int.Parse(today.Substring(0, 2)));
                    day_buttons[int.Parse(today.Substring(0, 2))].GetComponent<Image>().color = Color.yellow;
                }
            }
            if (settings_controller.current.date_type == true)
            {
                if (int.Parse(today.Substring(2, 2)) == month_)
                {
                    Debug.Log(int.Parse(today.Substring(4, 2)));
                    day_buttons[int.Parse(today.Substring(4, 2))].GetComponent<Image>().color = Color.yellow;
                }
            }
        }

    }*/
    
    public void Set_day_with_text()
    {
        task_.GetComponent<task_data>().my_date = task_.GetComponent<task_data>().date_input_text.text;
    }
    public void Set_today()
    {
        task_.GetComponent<task_data>().Set_day(DateTime.Today);        
    }

    public void Set_tomorrow()
    {
        task_.GetComponent<task_data>().Set_day(DateTime.Today.AddDays(1));
    }

    public void Set_noDate()
    {
        task_.GetComponent<task_data>().my_date = null;
    }

    public void Set_next_week()
    {
        if (task_.TryGetComponent<task_data>(out task_data taskData))
        {
            taskData.Set_day(DateTime.Today);
            float day_to_add = day_to_add = settings_controller.current.next_week +7;
            if (taskData.my_date == null) day_to_add = settings_controller.current.next_week + 7;
            if (taskData.my_day_of_the_week == DayOfWeek.Sunday) day_to_add = settings_controller.current.next_week -0+7;
            if (taskData.my_day_of_the_week == DayOfWeek.Monday)day_to_add = settings_controller.current.next_week -1+7;
            if (taskData.my_day_of_the_week == DayOfWeek.Tuesday) day_to_add = settings_controller.current.next_week -2+7;
            if (taskData.my_day_of_the_week == DayOfWeek.Wednesday) day_to_add = settings_controller.current.next_week -3+7;
            if (taskData.my_day_of_the_week == DayOfWeek.Thursday) day_to_add = settings_controller.current.next_week -4+7;
            if (taskData.my_day_of_the_week == DayOfWeek.Friday) day_to_add = settings_controller.current.next_week -5+7;
            if (taskData.my_day_of_the_week == DayOfWeek.Saturday) day_to_add = settings_controller.current.next_week -6+7;
            taskData.Set_day(DateTime.Today.AddDays(day_to_add));
        }
    }
    
    public void Set_this_weekend()
    {
        if (task_.TryGetComponent<task_data>(out task_data taskData))
        {
            float day_add_to = settings_controller.current.this_weekend;
            taskData.Set_day(DateTime.Today);
            if (taskData.my_day_of_the_week == DayOfWeek.Sunday) day_add_to = settings_controller.current.this_weekend -1;
            if (taskData.my_day_of_the_week == DayOfWeek.Monday) day_add_to = settings_controller.current.this_weekend -2;
            if (taskData.my_day_of_the_week == DayOfWeek.Tuesday) day_add_to = settings_controller.current.this_weekend -3;
            if (taskData.my_day_of_the_week == DayOfWeek.Wednesday) day_add_to = settings_controller.current.this_weekend -4;
            if (taskData.my_day_of_the_week == DayOfWeek.Thursday) day_add_to = settings_controller.current.this_weekend -5;
            if (taskData.my_day_of_the_week == DayOfWeek.Friday) day_add_to = settings_controller.current.this_weekend -6;
            if (taskData.my_day_of_the_week == DayOfWeek.Saturday) day_add_to = settings_controller.current.this_weekend -7;
            taskData.Set_day(DateTime.Today.AddDays(day_add_to));
        }
    }
    
    public void Set_later_this_week()
    {
        if (task_.TryGetComponent<task_data>(out task_data taskData))
        {
            float day_add_to = settings_controller.current.this_weekend;
            taskData.Set_day(DateTime.Today);
            if (taskData.my_day_of_the_week == DayOfWeek.Sunday) day_add_to = settings_controller.current.later_this_week -1;
            if (taskData.my_day_of_the_week == DayOfWeek.Monday) day_add_to = settings_controller.current.later_this_week -2;
            if (taskData.my_day_of_the_week == DayOfWeek.Tuesday) day_add_to = settings_controller.current.later_this_week -3;
            if (taskData.my_day_of_the_week == DayOfWeek.Wednesday) day_add_to = settings_controller.current.later_this_week -4;
            if (taskData.my_day_of_the_week == DayOfWeek.Thursday) day_add_to = settings_controller.current.later_this_week -5;
            if (taskData.my_day_of_the_week == DayOfWeek.Friday) day_add_to = settings_controller.current.later_this_week -6;
            if (taskData.my_day_of_the_week == DayOfWeek.Saturday) day_add_to = settings_controller.current.later_this_week -7;
            taskData.Set_day(DateTime.Today.AddDays(day_add_to));
        }
    }
    
    public void button_func(GameObject button)
    {
        day_output = day_buttons.IndexOf(button);
        var cultureInfo = new CultureInfo("de-DE");
        string DateString = (day_output + " " + month_ + " " + year_).ToString();
        //string DateString = "12 June 2008";
        task_.GetComponent<task_data>().Set_day(DateTime.Parse(DateString,cultureInfo));
    }
}
