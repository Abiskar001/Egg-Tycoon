using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action dayPassed;

    public static event Action eggCollected;

    public static event Action moneyUpdate;
    
    public static void oneDayPassed()
    {
        dayPassed?.Invoke();
    }

    public static void eggPressed()
    {
        eggCollected?.Invoke();
    }

    public static void moneyyUpdate()
    {
        moneyUpdate?.Invoke();
    }
}
