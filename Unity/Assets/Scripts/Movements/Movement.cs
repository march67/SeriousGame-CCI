using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private const float TILE_SIZE = 1f;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (!isMoving)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (input != Vector2.zero)
            {
                // Conversion du mouvement en coordonnées isométriques, calcule de la destination en vue isométrique
                Vector3 direction = new Vector3(
                     (input.x - input.y) * (TILE_SIZE / 2),
                     (input.x + input.y) * (TILE_SIZE / 4),
                    0
                );

                targetPosition = transform.position + direction;
                isMoving = true;
            }
        }
        else
        {
            // Déplacement fluide vers la position cible
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            // Vérification si on est arrivé à destination
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }
}
