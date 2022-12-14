using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private Health health;
    public int damage = 1;
    public float damageInterval = 1.0f;
    public float blinkDuration = 0.5f;
    public float blinkInterval = 0.1f;

    public float knockbackForce = 10.0f;
    public float knockbackDuration = 0.5f; // duration of the knockback, in seconds

    private Color normalColor;
    private Color blinkColor = Color.red;
    private float lastDamageTime;
    private float lastBlinkTime;
    private bool isBlinking = false;

    private void Start()
    {
        health = GetComponent<Health>();
        normalColor = GetComponent<SpriteRenderer>().color;
        lastDamageTime = Time.time;
        lastBlinkTime = Time.time;
    }

    private void Update()
    {
        HandleBlinking();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "DangerZone")
        {
            HandleDamage();
        }
    }

    // added new method for handling blinking
    private void HandleBlinking()
    {
        if (health.currentHealth < health.maxHealth)
        {
            if (Time.time - lastDamageTime <= blinkDuration)
            {
                if (Time.time - lastBlinkTime >= blinkInterval)
                {
                    if (isBlinking)
                    {
                        GetComponent<SpriteRenderer>().color = normalColor;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().color = blinkColor;
                    }
                    isBlinking = !isBlinking;
                    lastBlinkTime = Time.time;
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().color = normalColor;
            }
        }
    }

    // added new method for handling damage and knockback
    private void HandleDamage()
    {
        if (Time.time - lastDamageTime >= damageInterval)
        {
            health.TakeDamage(damage);
            lastDamageTime = Time.time;

            // apply knockback force only if the knockback duration has not elapsed
            if (Time.time - lastDamageTime < knockbackDuration)
            {
                Vector2 knockback = new Vector2(0, knockbackForce);
                GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
            }
        }
    }
}
