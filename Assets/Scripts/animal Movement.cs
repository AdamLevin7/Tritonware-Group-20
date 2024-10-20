using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalMovement : MonoBehaviour
{
    Vector2 lastPos;

    // Set the offset based on the size of your object
    public float offset = 5.0f;

    public Rigidbody2D animalRB;
    public SpriteRenderer animalSprite;
    public int speed;
    public int choice;
    public int frames = 0;
    public int randomFrames = 0; // the animal moves in a certain direction for a random amount of frames

    // Minimum and maximum bounds for the animal movement
    public Vector2 minBounds; // Bottom-left corner of the rectangle
    public Vector2 maxBounds; // Top-right corner of the rectangle

    private InteractionObjectModel animalObjectModel = null;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
        animalObjectModel = GetComponent<InteractionObjectModel>();
        Debug.Log(animalObjectModel.IsRunningAround);
    }

    // Update is called once per frame
    void Update()
    {
        if (animalObjectModel.IsRunningAround) // if IsRunningAround is true, the animal will move
        {
            if (frames > randomFrames || randomFrames == 0)
            {
                choice = Random.Range(4, 9);
                randomFrames = Random.Range(100, 200);
                frames = 0;
            }

            // Four directions the animal will move
            Vector2 movementDirection = Vector2.zero;
            if (choice == 4)
            {
                movementDirection = Vector2.right;
            }
            else if (choice == 5)
            {
                movementDirection = Vector2.left;
            }
            else if (choice == 6)
            {
                movementDirection = Vector2.up;
            }
            else if (choice == 7)
            {
                movementDirection = Vector2.down;
            }

            move(movementDirection, speed);

            frames++;
        }
        else
        {
            move(Vector2.zero, speed);
        }
    }

    void FixedUpdate()
    {
        // Calculate current position and direction
        Vector2 currentPos = transform.position;
        Vector2 direction = (currentPos - lastPos).normalized;

        // Check for bounds and teleport back inside if necessary
        CheckBounds(currentPos);

        // Update last position for the next frame
        lastPos = currentPos;
    }

    private void CheckBounds(Vector2 currentPosition)
    {
        // Check if the current position is outside the bounds
        if (currentPosition.x < minBounds.x || currentPosition.x > maxBounds.x ||
            currentPosition.y < minBounds.y || currentPosition.y > maxBounds.y)
        {
            // Teleport the animal back inside the bounds
            Vector2 newPosition = new Vector2(
                Mathf.Clamp(currentPosition.x, minBounds.x + offset, maxBounds.x - offset),
                Mathf.Clamp(currentPosition.y, minBounds.y + offset, maxBounds.y - offset)
            );

            transform.position = newPosition; // Set the new position
        }
    }

    private void move(Vector2 direction, int speed)
    {
        animalRB.velocity = direction.normalized * speed;

        if (animalRB.velocity.x > 0)
        {
            animalSprite.flipX = true;
        }
        else if (animalRB.velocity.x < 0)
        {
            animalSprite.flipX = false;
        }
    }
}
