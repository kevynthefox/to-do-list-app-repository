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
    public DateTime later_this_week;
    public DateTime this_weekend;
    public DateTime next_week;

    void Awake()
    {
        current = this;
    }
}
