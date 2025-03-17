using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEditor.UIElements;

public class EventManager : MonoBehaviour
{
    public static event Action OnDayStart;
    public static event Action OnDayEnd;
    public static event Action OnStatGeneration;

    public static void DayStart()
    {
        OnDayStart?.Invoke();
        StatGeneration();

        Debug.Log("Day begins");
    }

    public static void DayEnd()
    {
        OnDayEnd?.Invoke();

        Debug.Log("Day ends");
    }

    public static void StatGeneration()
    {
        OnStatGeneration?.Invoke();


        Debug.Log("Stat generated");
    }
}
