using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeManager : MonoBehaviour
{
    public float gameTimeScale = 1;
    public Text yearUI;
    public Text dayUI;
    public Text TimeUI;
    public float timeTick; //realworld time for 1 minute increment

    private float timer;
    [SerializeField] private int minute;
    [SerializeField] private int hour;
    [SerializeField] private int day;
    [SerializeField] private int year;
    public int unaffectedDay;
    

    private void Awake()
    {
        Time.timeScale = gameTimeScale;
    }

    void Update()
    {
        Time.timeScale = gameTimeScale;
        if (day > 365)
        {
            year += 1;
            day = 1;
        }
        if (hour >= 24)
        {
            day += 1;
            unaffectedDay += 1;
            hour = 0;
            EventManager.oneDayPassed();
        }
        if (minute >= 60)
        {
            hour += 1;
            minute = 0;
        }
        if (timer > timeTick)
        {
            minute += 1;
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
        yearUI.text = "Year " + year;
        dayUI.text = "Day " + day ;
        TimeUI.text = hour.ToString("00") + " : " + minute.ToString("00");
    }
}
