using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEditor.UIElements;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    // Structure handling call order
    private static List<KeyValuePair<int, Action>> statGenerationCallbacks = new List<KeyValuePair<int, Action>>();

    // Dictionary to stock all events with their prioritized callbacks
    private static Dictionary<EventType, List<KeyValuePair<int, Action>>> eventCallbacks = new Dictionary<EventType, List<KeyValuePair<int, Action>>>();
    
    public enum EventType
    {
        DayEnd
    }

    public static event Action OnDayStart;
    public static event Action OnDayEnd;
    public static event Action OnDialogueEventTrigger;

    public static void DayStart()
    {
        OnDayStart?.Invoke();
        StatGeneration();
    }

    public static void DayEnd()
    {
        OnDayEnd?.Invoke();
    }

    public static void DialogueEventTrigger()
    {
        OnDialogueEventTrigger?.Invoke();
    }


    // Subscription with priority (lowest value is highest priority)
    public static void AddStatGenerationListener(Action callback, int priority = 0)
    {
        statGenerationCallbacks.Add(new KeyValuePair<int, Action>(priority, callback));
        statGenerationCallbacks.Sort((a, b) => a.Key.CompareTo(b.Key)); // sort
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

    public static void AddListener(EventType eventType, Action callback, int priority = 0)
    {
        // add callback with its priority to the dictionary
        eventCallbacks[eventType].Add(new KeyValuePair<int,Action>(priority, callback));
        // sort
        eventCallbacks[eventType].Sort((a, b) => a.Key.CompareTo(b.Key));
    }

    public static void RemoveListener(EventType eventType, Action callback)
    {
        if (eventCallbacks.ContainsKey(eventType))
        {
            eventCallbacks[eventType].RemoveAll(pair => pair.Value == callback);
        }
    }

    public static void TriggerEvent(EventType eventType)
    {
        if (eventCallbacks.ContainsKey(eventType))
        {
            // Appeler tous les callbacks dans l'ordre de priorité
            foreach (var callback in eventCallbacks[eventType])
            {
                callback.Value.Invoke();
            }

            Debug.Log($"Event {eventType} triggered");
        }
    }
}
