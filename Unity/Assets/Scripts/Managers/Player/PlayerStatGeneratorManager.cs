using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    int statValueRetrieved;
    int statGenerated;

    private void Start()
    {
    }

    private void OnEnable()
    {
        EventManager.AddStatGenerationListener(PlayerGenerateProgression, 0);
        EventManager.OnDialogueEventTrigger += SendRandomStory;
    }

    private void OnDisable()
    {
        EventManager.RemoveStatGenerationListener(PlayerGenerateProgression);
        EventManager.OnDialogueEventTrigger -= SendRandomStory;
    }

    private void PlayerGenerateProgression()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            players.Add(playerObject);
        }
        
        foreach (GameObject player in players)
        {
            randomStat(player);
        }
    }

    private void randomStat(GameObject player)
    {
        // retrieve the components
        Transform imageChild = player.transform.Find("CNV_PlayerProjectProgression/PNL_PlayerProjectProgression/IMG_PlayerProjectProgression");
        Transform textChild = player.transform.Find("CNV_PlayerProjectProgression/PNL_PlayerProjectProgression/TXT_PlayerProjectProgression");

        // retrieve player's stat
        PlayerStat playerStat = player.GetComponent<PlayerStat>();

        if (imageChild != null && textChild != null)
        {
            // retrieve image and text component of the child
            Image imageComponent = imageChild.GetComponent<Image>();
            TextMeshProUGUI textMeshProGUIComponent = textChild.GetComponent<TextMeshProUGUI>();

            // choose a random stat + random value  to progress + corresponding image to display
            int randomStatIndex = UnityEngine.Random.Range(0, stats.Count);
            double randomStatValueMultiplicator = UnityEngine.Random.Range(1.01f, 2.01f); // 1.01 to 2.00 multiplicator

            switch (randomStatIndex)
            {
                case 0:
                    statValueRetrieved = playerStat.creativityStat;
                    imageComponent.sprite = creativity;
                    break;
                case 1:
                    statValueRetrieved = playerStat.funStat;
                    imageComponent.sprite = fun;
                    break;
                case 2:
                    statValueRetrieved = playerStat.artStat;
                    imageComponent.sprite = art;
                    break;
                case 3:
                    statValueRetrieved = playerStat.musicStat;
                    imageComponent.sprite = music;
                    break;  
            }

            // calculate stat to be generated then display to the UI
            statGenerated = (int)(Math.Ceiling(randomStatValueMultiplicator * statValueRetrieved));
            textMeshProGUIComponent.text = "+ " + statGenerated.ToString();

            // test to increase stat
            string stat = stats[randomStatIndex].ToString();
            playerStat.increasePlayerTargetedStatByFive(player, stat);
        }
    }

    private void SendRandomStory()
    {
        int randomIndexCharacter = Random.Range(0, 4);
        TextAsset[] textAssets = TextAssetPoolManager.GetInstance().GetTextAssetsFromSpecificPool(randomIndexCharacter);
        int randomIndexStory = Random.Range(0, textAssets.Length);
        TextAsset story = textAssets[randomIndexStory];
        DialogueManager.GetInstance().EnterDialogMode(story);
    }
}
