using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{           
    Health health; // Reference to the Health component of the player
    private int damage = 1; // The amount of damage that the player takes    
    private Vector3 respawnPoint; // The position that the player will respawn at
    public GameObject fallDetector; // Reference to the game object that will be used to detect when the player falls off the map

    private void Start()
    {
        // Get the Health component of the player
        health = GetComponent<Health>();

        // Set the initial respawn point to the player's starting position
        SetRespawnPoint();
    }

    private void FixedUpdate()
    {
        // Keep the fall detector in sync with the player's x position
        TrackingPlayer();
    }

    // Respawn point to the player's current position
    public void SetRespawnPoint()
    {
        respawnPoint = transform.position;
    }

    // Respawn the player at the current respawn point
    public void RespawnPlayer()
    {
        transform.position = respawnPoint;
    }

    // Keep the fall detector in sync with the player's x position
    private void TrackingPlayer()
    {        
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player has collided with the fall detector or a checkpoint

        if (collision.tag == "FallDetector")
        {
            // If the player has fallen off the map, deal damage and respawn.
            health.TakeDamage(damage);
            RespawnPlayer();
        }
        else if (collision.tag == "Checkpoint")
        {
            // If the player has collided with a checkpoint, update the respawn point to the checkpoint's position
            SetRespawnPoint();
        }
    }
}

