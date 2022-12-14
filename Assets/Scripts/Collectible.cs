using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    private int coinAmount = 0;
    private int collectiblesAmount = 0;
    private int hotdogAmount = 0;
    private int cheeseAmount = 0;
    private int donutAmount = 0;

    // References to UI Sprites that represent the collecibles (hotdog, cheese, and donut)
    public Image hotdogSprite;
    public Image cheeseSprite;
    public Image donutSprite;

    // References to empty and full sprites for hotdog, cheese, and donut.
    public Sprite hotdogEmptySprite;
    public Sprite hotdogFullSprite;
    public Sprite cheeseEmptySprite;
    public Sprite cheeseFullSprite;
    public Sprite donutEmptySprite;
    public Sprite donutFullSprite;

    private void Update()
    {
        CollectiblesUI();
    }

    private void CollectiblesUI()
    {
        // Update the UI Sprites for hotdog, cheese, and donut depending on whether the items has been collected.
        hotdogSprite.sprite = hotdogAmount > 0 ? hotdogFullSprite : hotdogEmptySprite;
        cheeseSprite.sprite = cheeseAmount > 0 ? cheeseFullSprite : cheeseEmptySprite;
        donutSprite.sprite = donutAmount > 0 ? donutFullSprite : donutEmptySprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);

            coinAmount += 1;
        }
        if (collision.gameObject.CompareTag("Hotdog"))
        {
            Destroy(collision.gameObject);

            collectiblesAmount += 1;
            hotdogAmount += 1;
        }
        if (collision.gameObject.CompareTag("Donut"))
        {
            Destroy(collision.gameObject);

            collectiblesAmount += 1;
            donutAmount += 1;
        }
        if (collision.gameObject.CompareTag("Cheese"))
        {
            Destroy(collision.gameObject);

            collectiblesAmount += 1;
            cheeseAmount += 1;
        }
    }
}

