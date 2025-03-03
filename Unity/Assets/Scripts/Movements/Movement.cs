using UnityEngine;

public class Movement : MonoBehaviour
{
    public Sprite spriteFront;
    public Sprite spriteBack;
    private SpriteRenderer spriteRenderer;
    public float moveSpeed = 5f;
    private Vector3 targetPosition;
    private Vector2 lastInput;
    private bool isMoving = false;
    private const float TILE_SIZE = 1f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetPosition = transform.position;
    }

    void Update()
    {
        if (!isMoving)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (input != Vector2.zero)
            {
                // Update sprite according to the input direction
                UpdateSprite(input);

                // Converting movement isometric coordonate, calcul of the destination in isometric view
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
            // Smooth movement animation toward the target position
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            // Verify if the destination is reached
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    private void UpdateSprite(Vector2 input)
    {
        if (input.x > 0)
        {
            spriteRenderer.sprite = spriteBack;
            spriteRenderer.flipX = false;
        }
        else if (input.x < 0)
        {
            spriteRenderer.sprite = spriteFront;
            spriteRenderer.flipX = false;
        }
        else if (input.y > 0)
        {
            spriteRenderer.sprite = spriteBack;
            spriteRenderer.flipX = true;
        }
        else if (input.y < 0)
        {
            spriteRenderer.sprite = spriteFront;
            spriteRenderer.flipX = true;
        }
    }
}
