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
        DayEnd,
        DayStart,
        StatGeneration,
        EnterDialogue,
    }

    public static event Action OnDialogueEventTrigger;

    public static void DialogueEventTrigger()
    {
        OnDialogueEventTrigger?.Invoke();
    }

    public static void AddListener(EventType eventType, Action callback, int priority = 0)
    {
        // initialize the first time the key is set
        if (!eventCallbacks.ContainsKey(eventType))
        {
            eventCallbacks.Add(eventType, new List<KeyValuePair<int, Action>>());
        }

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
            // Call all callbacks in priority order
            // The order is already sorted when the callback is added in AddListener
            foreach (var callback in eventCallbacks[eventType])
            {
                callback.Value.Invoke();
            }

            Debug.Log($"Event {eventType} triggered");
        }
    }
}
