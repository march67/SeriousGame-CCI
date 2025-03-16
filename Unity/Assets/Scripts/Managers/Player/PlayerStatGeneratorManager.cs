using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatGeneratorManager : MonoBehaviour
{
    // assign real source image through inspector on this GameObject
    public Sprite creativity;
    public Sprite fun;
    public Sprite art;
    public Sprite music;


    private List<GameObject> players = new List<GameObject>();
    private List<String> stats = new List<String>() { "creativity", "fun", "art", "music" };
    public Sprite[] images; // declare array of 4 images to assign in inspector
    private SpriteRenderer childSpriteRenderer; // reference to the component responsible of displaying the image

    private void Start()
    {
        Debug.Log("PlayerStatGeneratorManager started");

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            players.Add(playerObject);
        }
        
        foreach (GameObject player in players)
        {
            randomStat(player);
            Debug.Log("randomStat called");
        }


    }

    private void randomStat(GameObject player)
    {
        Transform imageChild = player.transform.Find("CNV_PlayerProjectProgression/PNL_PlayerProjectProgression/IMG_PlayerProjectProgression");
        Transform textChild = player.transform.Find("CNV_PlayerProjectProgression/PNL_PlayerProjectProgression/TXT_PlayerProjectProgression");
        if (imageChild != null && textChild != null)
        {
            // retrieve image and text component of the child
            Image imageComponent = imageChild.GetComponent<Image>();
            TextMeshProUGUI textMeshProGUIComponent = textChild.GetComponent<TextMeshProUGUI>();


            // choose a random stat + random value  to progress + corresponding image to display
            int randomStatIndex = UnityEngine.Random.Range(0, stats.Count);
            int randomStatValue = UnityEngine.Random.Range(0, 6);
            textMeshProGUIComponent.text = "+ " + randomStatValue.ToString();
            string stat = stats[randomStatIndex];
            switch (randomStatIndex)
            {
                case 0:
                    imageComponent.sprite = creativity;
                    break;
                case 1:
                    imageComponent.sprite = fun;
                    break;
                case 2:
                    imageComponent.sprite = art;
                    break;
                case 3:
                    imageComponent.sprite = music;
                    break;  
            }
        }






    }
}
