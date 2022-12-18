using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;

    private float moveX;
    [Header("Move Settings:")]
    [Range(0f, 20f)]
    public float moveSpeed = 5.5f;
    private bool isFacingRight = true;

    [Header("Jump Settings:")]
    [Range(0f, 20f)]
    public float jumpForce = 10f;
    [Range(0f, 20f)]
    public float fallMultiplier = 2.5f;
    [Range(0f, 20f)]
    public float lowJumpMultiplier = 2f;

    [Header("Groundchecking Settings:")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundRadius = 0.2f;

    // Bool to track whether the Player is colliding with a moving platform
    private bool isOnPlatform = false;
    
    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        // Store the Player's default gravity scale value        
    }

    private void FixedUpdate()
    {
        PlayerMove();
        CrispierJump();
    }
    private void CrispierJump()
    {
        if (rbody.velocity.y < 0)
        {
            rbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rbody.velocity.y > 0)
        {
            rbody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveX = context.ReadValue<Vector2>().x;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            PlayerJump();
        }
    }
    private void PlayerMove()
    {
        
        if (isOnPlatform)
        {
            // Move the player's transform along with the platform's transform
            transform.position += new Vector3(moveX * moveSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            // Control the player's movement directly using the player's rigidbody
            rbody.velocity = new Vector2(moveX * moveSpeed, rbody.velocity.y);
        }

        if (!isFacingRight && moveX > 0f)
        {
            FlipPlayer();
        }
        else if (isFacingRight && moveX < 0f)
        {
            FlipPlayer();
        }
    }

    private void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void PlayerJump()
    {        
        rbody.velocity = new Vector2(rbody.velocity.x, jumpForce);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    public void ApplyKnockback(Vector2 knockbackDirection)
    {
        Debug.Log("Knockback direction: " + knockbackDirection);
        Debug.Log("Player's rigidbody velocity before knockback: " + rbody.velocity);
        rbody.AddForce(knockbackDirection, ForceMode2D.Impulse);
        Debug.Log("Player's rigidbody velocity after knockback: " + rbody.velocity);
    }
    void OnCollisionStay2D(Collision2D collision)
    {        
        MovingPlatform platform = collision.collider.GetComponent<MovingPlatform>();

        // Check if the object is colliding with a moving platform
        if (platform != null)
        {
            // Get the player's Rigidbody2D component
            Rigidbody2D playerRb = collision.collider.GetComponent<Rigidbody2D>();

            // Calculate the new position for the player based on the platform's movement
            Vector2 newPos = playerRb.position + platform.moveDirection * platform.speed * Time.deltaTime;

            // Use the Rigidbody2D.MovePosition method to move the player to the new position
            playerRb.MovePosition(newPos);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the object is colliding with a moving platform
        if (collision.collider.GetComponent<MovingPlatform>() != null)
        {
            // Set isCollidingWithPlatform to false
            isOnPlatform = false;
        }
    }
}