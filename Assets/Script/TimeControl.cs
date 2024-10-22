using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public float TimeShiftTime;
    public float SlowdownFactor = 0.05f;
    public float SlowdownLenght = 2f;

    float Timer;

    [HideInInspector]public bool IsTimeShift = false;
    private void Start()
    {
        Timer = TimeShiftTime;
    }
    void Update()
    {
        if(Timer <= 0)
        {
            IsTimeShift = false;
        }
        if (IsTimeShift)
        {
            Timer -= Time.unscaledDeltaTime;
        }
        else
        {
            Time.timeScale += (1 / SlowdownLenght) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0, 1);
            Timer += Time.unscaledDeltaTime;
            FindObjectOfType<SoundManager>().slowPitch(false);
        }
    }
    public void SlowMo()
    {
        if (!IsTimeShift)
        {
            Time.timeScale = SlowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.2f;
        }
    }
    public void TimeShift()
    {
        if (!IsTimeShift)
        {
            IsTimeShift = true;
            Time.timeScale = SlowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.2f;
            FindObjectOfType<SoundManager>().slowPitch(true);
        }
        else if (IsTimeShift)
        {
            IsTimeShift = false;
        }
    }
}
