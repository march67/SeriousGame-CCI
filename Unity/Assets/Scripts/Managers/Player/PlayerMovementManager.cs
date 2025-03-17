using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    private Vector3Int visualOffset = new Vector3Int(0, 1, 0);

    private void OnEnable()
    {
        EventManager.OnDayEnd += MovePlayersToPosition;
    }

    private void OnDisable()
    {
        EventManager.OnDayEnd -= MovePlayersToPosition;
    }

    private void MovePlayersToPosition()
    {
        List<GameObject> players = new List<GameObject>();
        Grid grid = FindFirstObjectByType<Grid>();
        Slot slot = new Slot();


        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            players.Add(playerObject);
        }

        foreach (GameObject player in players)
        {
            Vector3Int targetPosition = slot.FindFirstAvailableSlotAndReturnGridPosition();
            if (targetPosition != Vector3Int.zero)
            {
                Vector3 gridPosition = grid.CellToWorld(targetPosition) + visualOffset;
                StartCoroutine(MovePlayerSmoothly(player, gridPosition));
            }
        }
    }

    private IEnumerator MovePlayerSmoothly(GameObject player, Vector3 targetPosition)
    {
        float duration = 3.0f; // Time deplacement in seconds
        float elapsedTime = 0;

        Vector3 startingPosition = player.transform.position;

        while (elapsedTime < duration)
        {
            player.transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Guaranteed that the player reaches final destination
        player.transform.position = targetPosition;
        Debug.Log("Player Moved");
    }
}
