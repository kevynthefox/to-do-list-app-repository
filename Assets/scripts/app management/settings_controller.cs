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

    void Awake()
    {
        current = this;
    }
}
