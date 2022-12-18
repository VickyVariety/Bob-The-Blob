using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 moveDirection = Vector2.right;    
    public Transform startPos; // Start position of the platform
    public Transform endPos; // End position of the platform
    public bool loop = true;

    private Rigidbody2D platformrb;
    // Enum to track the platform's movement direction
    private enum Direction { Right, Left };
    private Direction currentDirection;

    private void Start()
    {
        // Initializing the Rigidbody2D attached to the platform
        platformrb = GetComponent<Rigidbody2D>();
        //Set the initial movement direction
        MovementDirection();
    }

    private void FixedUpdate()
    {
        MovePlatform();
    }

    private void MovementDirection()
    {
        if (moveDirection.x > 0) 
        {
            currentDirection = Direction.Right;
        }
        else
        {
            currentDirection = Direction.Left;
        }
    }
    private void MovePlatform()
    {
        // Move the platform
        platformrb.velocity = moveDirection * speed;

        // Move the player along with the platform
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Check if the platform has reached its end position
        if ((currentDirection == Direction.Right && transform.position.x > endPos.position.x) || (currentDirection == Direction.Left && transform.position.x < startPos.position.x))
        {
            // Reverse move direction if the platform is set to be looping
            if (loop)
            {
                ReverseDirection();
            }
            // Stop the platform if it's not set to be looping
            else
            {
                platformrb.velocity = Vector2.zero;
            }

        }
    }
    // Method to reverse the movement direction of the platform
    private void ReverseDirection()
    {
        if (currentDirection == Direction.Right)
        {
            currentDirection = Direction.Left;
            moveDirection = Vector2.left;
        }
        else
        {
            currentDirection = Direction.Right;
            moveDirection = Vector2.right;
        }
    }

    // Called every fixed frame if the object is colliding with another object
    void OnCollisionStay2D(Collision2D collision)
    {        
        // Check if the object is colliding with the Player
        if (collision.collider.CompareTag("Player"))
        {
            // Make the Player a child of the platform, so it moves along with it.
            collision.collider.transform.parent = transform;
        }
    }

    // Called when the objects stops colliding with another object
    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the object is colliding with the Player
        if (collision.collider.CompareTag("Player"))
        {
            // Detach the Player from the platform
            collision.collider.transform.parent = null;
        }
    }
}
