using UnityEngine;

public class ChairCollision : MonoBehaviour
{
    public Sprite personnageAssis; // Sprite du personnage assis
    private SpriteRenderer playerRenderer; // Référence au SpriteRenderer du personnage

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie si l'objet entrant est le personnage
        if (other.gameObject.CompareTag("Player"))
        {
            // Récupère le SpriteRenderer du personnage
            playerRenderer = other.gameObject.GetComponent<SpriteRenderer>();

            if (playerRenderer != null && personnageAssis != null)
            {
                // Change le sprite du personnage pour celui assis
                playerRenderer.sprite = personnageAssis;
            }
        }
    }
}
