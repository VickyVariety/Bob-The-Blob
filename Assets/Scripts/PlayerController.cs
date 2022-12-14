using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();        
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
            rbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rbody.velocity.y > 0)
        {
            rbody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
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
        rbody.velocity = new Vector2(moveX * moveSpeed, rbody.velocity.y);

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

}
