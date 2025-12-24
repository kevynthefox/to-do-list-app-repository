using System;
using UnityEngine;

public class settings_controller : MonoBehaviour
{
    public static settings_controller current;

    [Header("animation")]
    public bool animate;
    public bool square_or_natural;
    public bool xfir_yfir;
    public bool intermediate;
    public float move_speed;
    [Header("date")]
    public bool date_type; //0 is dd,mm,yyyy, 1 is mm,dd,yyyy 
    public int later_this_week; //if it would be friday you would add 5 or whatever.
    public int this_weekend;
    public int next_week;

    void Awake()
    {
        current = this;
    }
}
