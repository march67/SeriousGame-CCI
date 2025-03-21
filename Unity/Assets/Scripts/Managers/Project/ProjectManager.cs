using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProjectManager : MonoBehaviour
{
    private static ProjectManager instance;
    private int progressionValue;
    public StatProgression statProgression;

    public int deadlineTimeInDays { private get; set; }
    public int deadlineTimeInDaysLeft { get; private set; }

    public static ProjectManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            return;
        }

        instance = this;
        deadlineTimeInDaysLeft = 0;
    }

    private void OnEnable()
    {
        EventManager.AddStatGenerationListener(UpdateProjectUI, 1);
        EventManager.OnDayEnd += UpdateProjectDeadLine;
    }

    private void OnDisable()
    {
        EventManager.RemoveStatGenerationListener(UpdateProjectUI);
        EventManager.OnDayEnd -= UpdateProjectDeadLine;
    }


    private void UpdateProjectUI()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject pnlProjectProgression = GameObject.FindFirstObjectByType<GameObject>();
        foreach (GameObject player in players)
        {
            // Retrieving which component to manipulate
            Transform textChild = player.transform.Find("CNV_PlayerProjectProgression/PNL_PlayerProjectProgression/TXT_PlayerProjectProgression");
            Transform imageChild = player.transform.Find("CNV_PlayerProjectProgression/PNL_PlayerProjectProgression/IMG_PlayerProjectProgression");
            TextMeshProUGUI textMeshProGUIComponent = textChild.GetComponent<TextMeshProUGUI>();
            Image imageComponent = imageChild.GetComponent<Image>();
            string imageName = imageComponent.sprite.name;
            string text = textMeshProGUIComponent.text;
            string cleanedText = text.Replace(" ", "").Replace("+", ""); // delete space and '+' symbol to be parsed
            bool successIntParse = int.TryParse(cleanedText, out progressionValue); // string value to int value

            // increment value depending on which random sprite got pulled
            if (successIntParse)
            {
                if ( imageName.Contains("art"))
                {
                    statProgression.artProgressionValue += progressionValue;
                }
                else if ( imageName.Contains("fun"))
                {
                    statProgression.funProgressionValue += progressionValue;
                }
                else if ( imageName.Contains("creativity"))
                {
                    statProgression.creativityProgressionValue += progressionValue;
                }
                else if (imageName.Contains("music"))
                {
                    statProgression.musicProgressionValue += progressionValue;
                }
            }
        }
    }

    private void UpdateProjectDeadLine()
    {
        // at the beginning deadlineTimeLeft is equal to deadlineTime
        if (deadlineTimeInDaysLeft == 0 )
        {
            deadlineTimeInDaysLeft = deadlineTimeInDays;
        }

        // each day passed, decrease the deadlineTimeLeft by one
        if ( deadlineTimeInDaysLeft > 0 )
        {
            deadlineTimeInDaysLeft -= 1;
        }
    }
}
