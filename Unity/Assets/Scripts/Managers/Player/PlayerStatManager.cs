using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{

    private static PlayerStatManager instance;

    public static PlayerStatManager GetInstance()
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
    }

    public void increaseAllPlayersAllStatsBy100()
    {
        GameObject[] playersObject = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playersObject)
        {
            PlayerStat playerStat = (playerObject.GetComponent<PlayerStat>());
            if (playerStat != null)
            {
                playerStat.creativityStat += 100;
                playerStat.artStat += 100;
                playerStat.funStat += 100;
                playerStat.musicStat += 100;
            }

        }
    }

    public void decreaseAllPlayersAllStatsBy50()
    {
        GameObject[] playersObject = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playersObject)
        {
            PlayerStat playerStat = (playerObject.GetComponent<PlayerStat>());
            if (playerStat != null)
            {
                playerStat.creativityStat -= 50;
                playerStat.artStat -= 50;
                playerStat.funStat -= 50;
                playerStat.musicStat -= 50;
            }

        }
    }

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
        PlayerStat playerStat = targetedPlayer.GetComponent<PlayerStat>();
        Dictionary<string, int> statValuePairs = new Dictionary<string, int>();
        statValuePairs.Add("artStat", playerStat.artStat);
        statValuePairs.Add("musicStat", playerStat.musicStat);
        statValuePairs.Add("funStat", playerStat.funStat);
        statValuePairs.Add("creativityStat", playerStat.creativityStat);
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

    public void saveAllPlayersStat()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            savePlayerStat(player);
        }
    }

    public void loadAllPlayersStat()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            loadPlayerStat(player);
        }
    }
}
