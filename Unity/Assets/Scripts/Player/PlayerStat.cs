using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int artStat;
    public int creativityStat;
    public int funStat;
    public int musicStat;

    public void increasePlayerTargetedStatByFive(GameObject targetedPlayer, string targetedStat)
    {
        // exemple of input (player, "art") / (player, "fun")
        int increaseValue = 5;

        PlayerStat playerStat = targetedPlayer.GetComponent<PlayerStat>();
        switch (targetedStat.ToLower())
        {
            case "art":
                playerStat.artStat += increaseValue;
                break;
            case "fun":
                playerStat.funStat += increaseValue;
                break;
            case "creativity":
                playerStat.creativityStat += increaseValue;
                break;
            case "music":
                playerStat.musicStat += increaseValue;
                break;
            default:
                Debug.LogWarning("Input stat not handled" + targetedStat);
                break;
        }
    }

    static public void savePlayerStat(GameObject targetedPlayer)
    {
        string playerName = targetedPlayer.name;
        PlayerStat playerStat = targetedPlayer.GetComponent<PlayerStat> ();
        Dictionary<string, int> statValuePairs = new Dictionary<string, int>();
        statValuePairs.Add ("artStat", playerStat.artStat);
        statValuePairs.Add ("musicStat", playerStat.musicStat);
        statValuePairs.Add ("funStat", playerStat.funStat);
        statValuePairs.Add ("creativityStat", playerStat.creativityStat);
        JsonSaveUtility.SaveDictionary(playerName, statValuePairs);
    }

    static public void loadPlayerStat(GameObject targetedPlayer)
    {
        string playerName = targetedPlayer.name;
        PlayerStat playerStat = targetedPlayer.GetComponent<PlayerStat>();
        Dictionary<string, int> statValuePairs = JsonSaveUtility.LoadDictionary(playerName);
        foreach (KeyValuePair<string, int> pair in statValuePairs)
        {
            switch (pair.Key)
            {
                case "artStat":
                    playerStat.artStat = pair.Value;
                    break;
                case "musicStat":
                    playerStat.musicStat = pair.Value;
                    break;
                case "funStat":
                    playerStat.funStat = pair.Value;
                    break;
                case "creativityStat":
                    playerStat.creativityStat = pair.Value;
                    break;
            }
        }
    }

    static public void saveAllPlayersStat()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            savePlayerStat(player);
        }
    }

    static public void loadAllPlayersStat()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            loadPlayerStat(player);
        }
    }
}
