using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEditor.UIElements;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    public static event Action OnDayStart;
    public static event Action OnDayEnd;

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

    // Structure handling call order
    private static List<KeyValuePair<int, Action>> statGenerationCallbacks = new List<KeyValuePair<int, Action>>();

    // Subscription with priority (lowest value is highest priority)
    public static void AddStatGenerationListener(Action callback, int priority = 0)
    {
        statGenerationCallbacks.Add(new KeyValuePair<int, Action>(priority, callback));
        statGenerationCallbacks.Sort((a, b) => a.Key.CompareTo(b.Key));
    }

    // Unsubscription method for stat generation
    public static void RemoveStatGenerationListener(Action callback)
    {
        statGenerationCallbacks.RemoveAll(pair => pair.Value == callback);
    }

    public static void StatGeneration()
    {
        // Call callbacks in priority order
        foreach (var callback in statGenerationCallbacks)
        {
            callback.Value.Invoke();
        }

        Debug.Log("Stat generated");
    }
}
