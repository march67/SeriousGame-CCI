using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeManager : MonoBehaviour
{
    // 1f = 1 seconde
    private float dayDuration = 10f; // 60 seconds = one day
    private float timeScale = 1f; // game speed multiplicator, = 0 if on stop, = 1 if normal speed / timer resumption
    private float statGenerationInterval = 5f;
    private float currentDayTime = 0;
    private float lastStatGenerationTime = 0;
    private const float epsilon = 0.01f; // error margin, used before condition doesn't work before timer reset
    private bool isDayStarted = false;
    private bool isPaused = false;

    private static TimeManager instance;
    public static TimeManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = new GameObject("TimeManager");
                instance =gameObject.AddComponent<TimeManager>();
                DontDestroyOnLoad(gameObject);
            }
            return instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused) return;

        currentDayTime += Time.deltaTime * timeScale;

        // Stat generation
        if (currentDayTime - lastStatGenerationTime >= statGenerationInterval - epsilon)
        {
            EventManager.StatGeneration();
            lastStatGenerationTime = currentDayTime;
        }
        // Beginning of the day
        if (currentDayTime >= 0 && !isDayStarted)
        {
            isDayStarted = true;
            EventManager.DayStart();
        }
        // End of the day
        if (currentDayTime >= dayDuration && isDayStarted == true) 
        {
            isDayStarted = false;
            EventManager.DayEnd();
            ResetAllTimersAndPause();
        }
    }

    private void ResetTimer(float timer)
    {
        timer = 0f;
    }

    private void ResetAllTimersAndPause()
    {
        isPaused = true;
        StartCoroutine(PauseBetweenDays(3f));
        currentDayTime = 0f;
        lastStatGenerationTime = 0f;
    }

    private IEnumerator PauseBetweenDays(float pauseDuration)
    {
        float previousTimeScale = timeScale;
        timeScale = 0f;
        yield return new WaitForSeconds(pauseDuration);
        timeScale = previousTimeScale;
        isPaused = false;
    }
}
