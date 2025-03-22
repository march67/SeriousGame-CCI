using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeManager : MonoBehaviour
{
    // 1f = 1 seconde
    private float dayDuration = 10f; // x seconds = one day
    private float timeScale = 1f; // game speed multiplicator, = 0 if on stop, = 1 if normal speed / timer resumption
    private float statGenerationInterval = 5f;
    private float currentDayTime = 0;
    private float lastStatGenerationTime = 0;
    private const float epsilon = 0.01f; // error margin, used before condition doesn't work before timer reset
    private bool isDayStarted = false;
    public bool isPaused = false;
    public bool isPausedByDialogue = false;

    private static TimeManager instance;

    public static TimeManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("TimeManager instance already exists");
        }
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (isPaused || isPausedByDialogue) return;

        currentDayTime += Time.deltaTime * timeScale;

        // Stat generation interval
        if (currentDayTime - lastStatGenerationTime >= statGenerationInterval - epsilon)
        {
            EventManager.TriggerEvent(EventManager.EventType.StatGeneration);
            lastStatGenerationTime = currentDayTime;
        }
        // Beginning of the day
        if (currentDayTime >= 0 && !isDayStarted)
        {
            isDayStarted = true;
            EventManager.TriggerEvent(EventManager.EventType.DayStart);

            // test ink story
            EventManager.TriggerEvent(EventManager.EventType.EnterDialogue);
        }
        // End of the day
        if (currentDayTime >= dayDuration && isDayStarted == true) 
        {
            isDayStarted = false;
            EventManager.TriggerEvent(EventManager.EventType.DayEnd);
            ResetAllTimersAndPause();
        }
    }

    private void ResetAllTimersAndPause()
    {
        isPaused = true;
        StartCoroutine(PauseBetweenDays(5f));
        currentDayTime = 0f;
        lastStatGenerationTime = 0f;
    }

    public void PauseTimerByDialogue()
    {
        isPaused = true;
        isPausedByDialogue = true;
    }

    public void StartTimerByDialogue()
    {
        isPaused = false;
        isPausedByDialogue = false;
    }

    private IEnumerator PauseBetweenDays(float pauseDuration)
    {
        isPaused = true;
        float elapsedTime = 0f;

        
        while (elapsedTime < pauseDuration)
        {
            if ( isPausedByDialogue )
            {
                Debug.Log($"BetweenDays" + TimeManager.instance.isPausedByDialogue);
                // Wait until dialogue ended
                yield return new WaitUntil(() => !isPausedByDialogue);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isPaused = false;
    }
}
