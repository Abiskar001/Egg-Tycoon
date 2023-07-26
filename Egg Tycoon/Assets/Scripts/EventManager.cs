using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action dayPassed;

    public static event Action eggCollected;
    
    public static void oneDayPassed()
    {
        dayPassed?.Invoke();
    }

    public static void eggPressed()
    {
        eggCollected?.Invoke();
    }
}
