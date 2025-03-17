using UnityEngine;

public class ChairCollision : MonoBehaviour
{
    private Sprite spriteOriginal; // Sprite original avant la collision du personnage 
    public Sprite personnageAssis; // Sprite du personnage assis
    private SpriteRenderer playerRenderer; // R�f�rence au SpriteRenderer du personnage


    private void OnTriggerEnter2D(Collider2D other)
    {
        // V�rifie si l'objet entrant est le personnage
        if (other.gameObject.CompareTag("Player"))
        {
            // R�cup�re le SpriteRenderer du personnage
            playerRenderer = other.gameObject.GetComponent<SpriteRenderer>();

            if (playerRenderer != null && personnageAssis != null)
            {
                // Sauvegarde le sprite original avant de le changer
                spriteOriginal = playerRenderer.sprite;

                // Change le sprite du personnage pour celui assis
                playerRenderer.sprite = personnageAssis;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // V�rifie si l'objet sortant est le personnage
        if (other.gameObject.CompareTag("Player"))
        {
            // V�rifie que le playerRenderer existe et que le sprite original a �t� sauvegard�
            if (playerRenderer != null && spriteOriginal != null)
            {
                // Restaure le sprite d'origine
                playerRenderer.sprite = spriteOriginal;
            }
        }
    }
}
