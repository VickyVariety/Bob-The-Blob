using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public float constantSpeed; // the speed at which the clouds move in a constant direction
    public bool invertConstantMovement; // whether or not to invert the constant movement of the clouds

    void Update()
    {
        // move the clouds
        MoveClouds();

        // wrap the clouds around the screen if necessary
        WrapClouds();
    }

    void MoveClouds()
    {
        // store the constant speed in a separate variable
        float speed = constantSpeed;

        // invert the constant speed if necessary
        if (constantSpeed != 0 && invertConstantMovement)
        {
            speed *= -1;
        }

        // move the clouds in a constant direction
        transform.position += new Vector3(speed, 0, 0);
    }

    void WrapClouds()
    {
        // get the width of the clouds
        float width = GetComponent<SpriteRenderer>().bounds.size.x;

        // move the clouds back to the starting position if they have moved off-screen
        if (transform.position.x > width)
        {
            transform.position = new Vector3(-width, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -width)
        {
            transform.position = new Vector3(width, transform.position.y, transform.position.z);
        }
    }
}

