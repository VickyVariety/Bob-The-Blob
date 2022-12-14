using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    Health health;
    private int damage = 1;
    private Vector3 respawnPoint;
    public GameObject fallDetector;

    private void Start()
    {
        health = GetComponent<Health>();
        SetRespawnPoint();
    }
    private void FixedUpdate()
    {
        TrackingPlayer();
    }

    public void SetRespawnPoint()
    {
        respawnPoint = transform.position;
    }
    public void RespawnPlayer()
    {
        transform.position = respawnPoint;
    }

    private void TrackingPlayer()
    {
        //The Fall Detector will follow the Player on the x axis
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            health.TakeDamage(damage);
            RespawnPlayer();
        }
        else if(collision.tag == "Checkpoint")
        {
            SetRespawnPoint();
        }
    }
}
