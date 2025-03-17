using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    private Vector3Int visualOffset = new Vector3Int(0, 1, 0);

    private void OnEnable()
    {
        EventManager.OnDayEnd += MovePlayerToPosition;
    }

    private void OnDisable()
    {
        EventManager.OnDayEnd -= MovePlayerToPosition;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TeleportPlayerToPosition()
    {
        Grid grid = FindFirstObjectByType<Grid>();
        Vector3 gridPosition = grid.CellToWorld(new Vector3Int(-14, 7, 0));

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = gridPosition + visualOffset;
            Debug.Log("Player Moved");
        }
    }

    private void MovePlayerToPosition()
    {
        Grid grid = FindFirstObjectByType<Grid>();
        Vector3 gridPosition = grid.CellToWorld(new Vector3Int(-14, 7, 0)) + visualOffset;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            StartCoroutine(MovePlayerSmoothly(player, gridPosition));
        }
    }

    private IEnumerator MovePlayerSmoothly(GameObject player, Vector3 targetPosition)
    {
        float duration = 3.0f; // Durée du déplacement en secondes
        float elapsedTime = 0;

        Vector3 startingPosition = player.transform.position;

        while (elapsedTime < duration)
        {
            player.transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // S'assurer que le joueur atteint exactement la position cible
        player.transform.position = targetPosition;
        Debug.Log("Player Moved");
    }
}
