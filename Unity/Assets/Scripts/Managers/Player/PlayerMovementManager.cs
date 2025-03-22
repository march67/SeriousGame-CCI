using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.AddListener(EventManager.EventType.DayEnd, MovePlayersToSlot, 100);
        EventManager.AddListener(EventManager.EventType.DayStart, MovePlayersToChair, 100);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(EventManager.EventType.DayEnd, MovePlayersToSlot);
        EventManager.RemoveListener(EventManager.EventType.DayStart, MovePlayersToChair);
    }

    private void MovePlayersToSlot()
    {
        ChairManager.GetInstance().setAllChairStatusToAvailable(); // leaving the chair to go to the slot
        List<GameObject> players = new List<GameObject>();
        Grid grid = FindFirstObjectByType<Grid>();


        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            players.Add(playerObject);
        }

        foreach (GameObject player in players)
        {
            Vector3 targetWorldPositionWithPivotOffset = SlotManager.GetInstance().FindFirstAvailableSlotAndReturnWorldPosition() + new Vector3(0, 0.75f, 0);
            if (targetWorldPositionWithPivotOffset != Vector3Int.zero)
            {
                StartCoroutine(MovePlayerSmoothly(player, targetWorldPositionWithPivotOffset));
            }
        }
    }
    private void MovePlayersToChair()
    {
        SlotManager.GetInstance().setAllSlotStatusToAvailable(); // leaving the slot to go to the chair
        List<GameObject> players = new List<GameObject>();
        Grid grid = FindFirstObjectByType<Grid>();

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            players.Add(playerObject);
        }

        foreach (GameObject player in players)
        {
            Vector3 targetWorldPositionWithPivotOffset = ChairManager.GetInstance().FindFirstAvailableChairAndReturnWorldPosition() + new Vector3(0, 0.25f, 0);
            if (targetWorldPositionWithPivotOffset != Vector3Int.zero)
            {
                StartCoroutine(MovePlayerSmoothly(player, targetWorldPositionWithPivotOffset));
            }
        }
    }

    private IEnumerator MovePlayerSmoothly(GameObject player, Vector3 targetWorldPosition)
    {
        float duration = 1.0f; // Movement time in seconds
        float elapsedTime = 0;

        Vector3 startingPosition = player.transform.position;

        while (elapsedTime < duration)
        {
            player.transform.position = Vector3.Lerp(startingPosition, targetWorldPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Guarante that the player reaches final destination
        player.transform.position = targetWorldPosition;
        Debug.Log("Player Moved");
    }
}
