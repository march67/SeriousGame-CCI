using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeManager : MonoBehaviour
{
    // 1f = 1 seconde
    private float dayDuration = 10f; // 60 secondes = une journée
    private float timeScale = 1f; // multiplicateur de vitesse de jeu, = 0 si arrêt, = 1 si écoulement normal
    private float statGenerationInterval = 5f;
    private float currentDayTime = 0;
    private float lastStatGenerationTime = 0;
    private const float epsilon = 0.01f; // error margin, used before condition doesn't work before timer reset
    private bool isDayStarted = false;

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
            ResetAllTimers();
        }
    }

    private void ResetTimer(float timer)
    {
        timer = 0f;
    }

    private void ResetAllTimers()
    {
        currentDayTime = 0f;
        lastStatGenerationTime = 0f;
    }
}
