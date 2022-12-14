using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Health Settings:")]
    public int maxHealth = 3;
    public int currentHealth;

    [Header("UI Settings:")]
    public List<Image> hearts;
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        Heart();
    }

    private void Heart()
    {
        // Update the fill amount of each heart based on the player's current health
        for (int i = 0; i < hearts.Count; i++)
        {
            Image heart = hearts[i];

            // If this heart corresponds to the player's current health, set the fill amount to 1
            if (i < currentHealth)
            {
                heart.fillAmount = 1;
                heart.sprite = fullHeartSprite;
            }
            // Otherwise, set the fill amount to 0
            else
            {
                heart.fillAmount = 0;
                heart.sprite = emptyHeartSprite;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<Respawn>().RespawnPlayer();
        currentHealth = maxHealth;
    }
}
