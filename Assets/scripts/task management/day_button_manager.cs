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


    public TextMeshProUGUI menu_text;
    
    public TMP_InputField day_repeat_1_inp;
    public TMP_InputField day_repeat_2_inp;
    public TMP_InputField day_repeat_3_inp;
    public TMP_InputField day_repeat_4_inp;
    public TMP_InputField day_repeat_5_inp;

    public void OnEnable()
    {
        year_ = DateTime.Now.Year;
        month_ = DateTime.Now.Month;
        Update_month_and_year();
        glow_today();
    }

    #region month_year_texts

    

    
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
        glow_today();
    }

    public void Update_month_and_year()
    {
        update_MonthYear_text();
        toggle_optional_days(year_,month_);
    }

    #endregion

    #region calendar_changes

    

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

    public void glow_today()
    {
        foreach (GameObject day in day_buttons)
        {
            if (day != null)
            {
                day.GetComponent<Image>().color = Color.white;
            }
        }

        
        
        string today = DateTime.Today.ToString("MM/dd/yyyy");
        //Debug.Log("today" + today);
        //Debug.Log("year " + int.Parse(today.Substring(6, 4)) + "year_ " + year_);
        if (int.Parse(today.Substring(6, 4)) == year_)
        {
            //Debug.Log("year == year");
            //Debug.Log("month " + int.Parse(today.Substring(0,2)) + "month_ " + month_);
            if (int.Parse(today.Substring(0, 2)) == month_)
            {
                day_buttons[int.Parse(today.Substring(3, 2))].GetComponent<Image>().color = Color.red;
            }
        }

        day_ = task_.GetComponent<task_data>().my_date_date_format;
        //Debug.Log(day_);
        if (day_.Year == year_)
        {
            //Debug.Log("year == year");
            //Debug.Log("month " + int.Parse(today.Substring(0,2)) + "month_ " + month_);
            if (day_.Month == month_)
            {
                day_buttons[day_.Day].GetComponent<Image>().color = Color.green;
            }
        }

    }
    
    #endregion

    #region day_setting

    

    
    public void Set_day_with_text()
    {
        if (task_.TryGetComponent<task_data>(out task_data task))
        {
            string my_date = task.date_input_text.text;

        
            string temp_d = my_date.Substring(0,2);
            string temp_m = my_date.Substring(3,2);
            string temp_y = my_date.Substring(6,4);
            string temp_h = "01";
            string temp_mm = "23";
            string temp_s = "45";
            //01/34/6789:12:45:78 
            

            string temp_full;

            if (settings_controller.current.date_type == false)
            {//m d y => d m y
                temp_full = temp_m +"/"+ temp_d +"/"+ temp_y;
            }
            else
            {
                temp_full = temp_d +"/"+ temp_m +"/"+ temp_y;
            }
            
            if (my_date.Length >= 11)
            {
                temp_h = my_date.Substring(11,2);
                temp_mm = my_date.Substring(14,2);
                temp_s = my_date.Substring(17,2);

                task.my_time = (temp_h + ":" + temp_mm + ":" + temp_s);
                temp_full += (" "+ temp_h + ":" + temp_mm + ":" + temp_s);
            }
            Debug.Log(temp_full);
            task.Set_day(DateTime.Parse(temp_full));
            
            
        
        }
        
        Set_day_menu_date();
    }
    public void Set_today()
    {
        task_.GetComponent<task_data>().Set_day(DateTime.Today);
        Set_day_menu_date();
    }

    public void Set_tomorrow()
    {
        task_.GetComponent<task_data>().Set_day(DateTime.Today.AddDays(1));
        Set_day_menu_date();
    }

    public void Set_noDate()
    {
        task_.GetComponent<task_data>().my_date = null;
        menu_text.text = "[no date]";
        project_manager.current.update_current_area();
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
        Set_day_menu_date();
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
        Set_day_menu_date();
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
        Set_day_menu_date();
    }
    
    public void button_func(GameObject button)
    {
        day_output = day_buttons.IndexOf(button);
        var cultureInfo = new CultureInfo("de-DE");
        string DateString = (day_output + " " + month_ + " " + year_).ToString();
        //string DateString = "12 June 2008";
        task_.GetComponent<task_data>().Set_day(DateTime.Parse(DateString,cultureInfo));
        Set_day_menu_date();
    }

    public void Set_day_menu_date()
    {
        menu_text.text = task_.GetComponent<task_data>().my_date_date_format.ToString("u");
        project_manager.current.update_current_area();
        glow_today();
    }
    #endregion
    #region repetition

    

    
    public void repeat_x_days()
    {
        Debug.Log(day_repeat_1_inp.text.Length);
        if (day_repeat_1_inp.text.Length != 0)
        {
            task_.GetComponent<task_data>().day_to_repeat_to = int.Parse(day_repeat_1_inp.text);
        }
        else
        {
            task_.GetComponent<task_data>().day_to_repeat_to = 0;
        }
        task_.GetComponent<task_data>().day_of_the_week_to_repeat_to = 0; 
        task_.GetComponent<task_data>().day_of_the_month_to_repeat_to = 0;day_repeat_2_inp.text = null;
        task_.GetComponent<task_data>().day_of_the_year_to_repeat_to = 0.ToString();day_repeat_3_inp.text = null;
        if (day_repeat_5_inp.text.Length != 0)
        {
            if (day_repeat_5_inp.text.Substring(0, 1) != "@")
            {
                task_.GetComponent<task_data>().hours_to_repeat_to = 0;
                day_repeat_5_inp.text = null;
                task_.GetComponent<task_data>().minutes_to_repeat_to = 0;
                day_repeat_5_inp.text = null;
                task_.GetComponent<task_data>().seconds_to_repeat_to = 0;
                day_repeat_5_inp.text = null;
            }
        }
        else
        {
            task_.GetComponent<task_data>().hours_to_repeat_to = 0;
            day_repeat_5_inp.text = null;
            task_.GetComponent<task_data>().minutes_to_repeat_to = 0;
            day_repeat_5_inp.text = null;
            task_.GetComponent<task_data>().seconds_to_repeat_to = 0;
            day_repeat_5_inp.text = null;
        }
    }
    public void repeat_week_days(int num)
    {
        task_.GetComponent<task_data>().day_of_the_week_to_repeat_to = num;
        task_.GetComponent<task_data>().day_to_repeat_to = 0;
        task_.GetComponent<task_data>().day_of_the_month_to_repeat_to = 0;day_repeat_2_inp.text = null;
        task_.GetComponent<task_data>().day_of_the_year_to_repeat_to = 0.ToString();day_repeat_3_inp.text = null;
        if (day_repeat_5_inp.text.Length != 0)
        {
            if (day_repeat_5_inp.text.Substring(0, 1) != "@")
            {
                task_.GetComponent<task_data>().hours_to_repeat_to = 0;
                day_repeat_5_inp.text = null;
                task_.GetComponent<task_data>().minutes_to_repeat_to = 0;
                day_repeat_5_inp.text = null;
                task_.GetComponent<task_data>().seconds_to_repeat_to = 0;
                day_repeat_5_inp.text = null;
            }
        }
        else
        {
            task_.GetComponent<task_data>().hours_to_repeat_to = 0;
            day_repeat_5_inp.text = null;
            task_.GetComponent<task_data>().minutes_to_repeat_to = 0;
            day_repeat_5_inp.text = null;
            task_.GetComponent<task_data>().seconds_to_repeat_to = 0;
            day_repeat_5_inp.text = null;
        }
    }
    public void repeat_month_days()
    {
        if (day_repeat_2_inp.text.Length != 0)
        {
            task_.GetComponent<task_data>().day_of_the_month_to_repeat_to = int.Parse(day_repeat_2_inp.text);
        }
        else
        {
            task_.GetComponent<task_data>().day_of_the_month_to_repeat_to = 0;
        }
        task_.GetComponent<task_data>().day_of_the_week_to_repeat_to = 0;
        task_.GetComponent<task_data>().day_to_repeat_to = 0;day_repeat_1_inp.text = null;
        task_.GetComponent<task_data>().day_of_the_year_to_repeat_to = 0.ToString();day_repeat_3_inp.text = null;
        if (day_repeat_5_inp.text.Length != 0)
        {
            if (day_repeat_5_inp.text.Substring(0, 1) != "@")
            {
                task_.GetComponent<task_data>().hours_to_repeat_to = 0;
                day_repeat_5_inp.text = null;
                task_.GetComponent<task_data>().minutes_to_repeat_to = 0;
                day_repeat_5_inp.text = null;
                task_.GetComponent<task_data>().seconds_to_repeat_to = 0;
                day_repeat_5_inp.text = null;
            }
        }
        else
        {
            task_.GetComponent<task_data>().hours_to_repeat_to = 0;
            day_repeat_5_inp.text = null;
            task_.GetComponent<task_data>().minutes_to_repeat_to = 0;
            day_repeat_5_inp.text = null;
            task_.GetComponent<task_data>().seconds_to_repeat_to = 0;
            day_repeat_5_inp.text = null;
        }
    }
    public void repeat_year_days()
    {
        if (day_repeat_3_inp.text.Length != 0)
        {
            task_.GetComponent<task_data>().day_of_the_year_to_repeat_to = day_repeat_3_inp.text;
        }
        else
        {
            task_.GetComponent<task_data>().day_of_the_year_to_repeat_to = "0";
        }

        task_.GetComponent<task_data>().day_of_the_week_to_repeat_to = 0;
        task_.GetComponent<task_data>().day_of_the_month_to_repeat_to = 0;day_repeat_2_inp.text = null;
        task_.GetComponent<task_data>().day_to_repeat_to= 0;day_repeat_1_inp.text = null;
        if (day_repeat_5_inp.text.Length != 0)
        {
            if (day_repeat_5_inp.text.Substring(0, 1) != "@")
            {
                task_.GetComponent<task_data>().hours_to_repeat_to = 0;
                day_repeat_5_inp.text = null;
                task_.GetComponent<task_data>().minutes_to_repeat_to = 0;
                day_repeat_5_inp.text = null;
                task_.GetComponent<task_data>().seconds_to_repeat_to = 0;
                day_repeat_5_inp.text = null;
            }
        }
        else
        {
            task_.GetComponent<task_data>().hours_to_repeat_to = 0;
            day_repeat_5_inp.text = null;
            task_.GetComponent<task_data>().minutes_to_repeat_to = 0;
            day_repeat_5_inp.text = null;
            task_.GetComponent<task_data>().seconds_to_repeat_to = 0;
            day_repeat_5_inp.text = null;
        }
    }

    public void repeat_time()
    {
        if (task_.TryGetComponent<task_data>(out task_data taskData))
        {
            if (day_repeat_5_inp.text.Length != 0)
            {
                if (day_repeat_5_inp.text.Substring(0, 1) == "h" ||
                    day_repeat_5_inp.text.Substring(0, 1) == "H")
                {
                    if (day_repeat_5_inp.text.Length != 0)
                        taskData.hours_to_repeat_to = int.Parse(day_repeat_5_inp.text.Substring(1));
                    taskData.minutes_to_repeat_to = 0;
                    taskData.seconds_to_repeat_to = 0;
                    taskData.time_to_repeat_to_h = 0;
                    taskData.time_to_repeat_to_m = 0;
                }

                if (day_repeat_5_inp.text.Substring(0, 1) == "m" ||
                    day_repeat_5_inp.text.Substring(0, 1) == "M")
                {
                    if (day_repeat_5_inp.text.Length != 0)
                        taskData.minutes_to_repeat_to = int.Parse(day_repeat_5_inp.text.Substring(1));
                    taskData.hours_to_repeat_to = 0;
                    taskData.seconds_to_repeat_to = 0;
                    taskData.time_to_repeat_to_h = 0;
                    taskData.time_to_repeat_to_m = 0;
                }

                if (day_repeat_5_inp.text.Substring(0, 1) == "s" ||
                    day_repeat_5_inp.text.Substring(0, 1) == "S")
                {
                    if (day_repeat_5_inp.text.Length != 0)
                        taskData.seconds_to_repeat_to = int.Parse(day_repeat_5_inp.text.Substring(1));
                    taskData.minutes_to_repeat_to = 0;
                    taskData.hours_to_repeat_to = 0;
                    taskData.time_to_repeat_to_h = 0;
                    taskData.time_to_repeat_to_m = 0;
                }

                if (day_repeat_5_inp.text.Substring(0, 1) == "@")
                {
                    taskData.hours_to_repeat_to = 0;
                    taskData.minutes_to_repeat_to = 0;
                    taskData.seconds_to_repeat_to = 0;

                    if (day_repeat_5_inp.text.Length != 0)
                        taskData.time_to_repeat_to_h = int.Parse(day_repeat_5_inp.text.Substring(1, 2));
                    if (day_repeat_5_inp.text.Length != 0)
                        taskData.time_to_repeat_to_m = int.Parse(day_repeat_5_inp.text.Substring(4, 2));
                    if (day_repeat_5_inp.text.Length != 0)
                    {
                        if (settings_controller.current.time_type == false)
                        {

                            if (day_repeat_5_inp.text.Substring(6, 2) == "am" ||
                                day_repeat_5_inp.text.Substring(6, 2) == "Am" ||
                                day_repeat_5_inp.text.Substring(6, 2) == "AM")
                            {
                                taskData.time_to_repeat_to_am_pm = false;
                            }

                            if (day_repeat_5_inp.text.Substring(6, 2) == "pm" ||
                                day_repeat_5_inp.text.Substring(6, 2) == "Pm" ||
                                day_repeat_5_inp.text.Substring(6, 2) == "PM")
                            {
                                taskData.time_to_repeat_to_am_pm = true;
                            }

                        }
                    }
                }
                else
                {
                    task_.GetComponent<task_data>().day_of_the_year_to_repeat_to = "0";
                    task_.GetComponent<task_data>().day_of_the_week_to_repeat_to = 0;
                    task_.GetComponent<task_data>().day_of_the_month_to_repeat_to = 0;
                    day_repeat_2_inp.text = null;
                    task_.GetComponent<task_data>().day_to_repeat_to = 0;
                    day_repeat_1_inp.text = null;
                }

            }
            else
            {
                taskData.hours_to_repeat_to = 0;
                taskData.minutes_to_repeat_to = 0;
                taskData.seconds_to_repeat_to = 0;
                taskData.time_to_repeat_to_h = 0;
                taskData.time_to_repeat_to_m = 0;
            }
        }
        
        
    }
    
    public void repeat_custom_days()
    {
        if (day_repeat_4_inp.text.Length != 0)
        {
            task_.GetComponent<task_data>().day_of_custom_repeat_to = day_repeat_4_inp.text;
            
            if (day_repeat_4_inp.text.Substring(0, 1) == "a"
                || day_repeat_4_inp.text.Substring(0, 1) == "A")
            {
                //multiply all 'repeat to's by the number to the right, using a modifier tacked on to the end of all of them.
                task_.GetComponent<task_data>().day_repeat_modifier = int.Parse(day_repeat_4_inp.text.Substring(1));
            }

            if (day_repeat_4_inp.text.Substring(0, 1) == "b"
                || day_repeat_4_inp.text.Substring(0, 1) == "B")
            {
                //this will be "last day of the week  of the month"
            }
        }
        else
        {
            task_.GetComponent<task_data>().day_of_custom_repeat_to = "0";
        }
    }

    public void toggle_repeat()
    {
        task_.GetComponent<task_data>().repeat_date = !task_.GetComponent<task_data>().repeat_date;
    }
    
    #endregion
}
