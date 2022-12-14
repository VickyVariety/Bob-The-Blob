using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    public Transform player;
    public BoxCollider2D cameraBounds;

    private float xMin, xMax, yMin, yMax;
    private float camY, camX;
    private float OrthVertical;
    private float OrthHorizontal;
    private Camera mainCam;

    private void Start()
    {
        SetCameraBounds();        
    }
    private void SetCameraBounds()
    {
        xMin = cameraBounds.bounds.min.x;
        xMax = cameraBounds.bounds.max.x;
        yMin = cameraBounds.bounds.min.y;
        yMax = cameraBounds.bounds.max.y;
        mainCam = GetComponent<Camera>();
        OrthVertical = mainCam.orthographicSize;
        OrthHorizontal = (xMax + OrthVertical) / 2.0f;
    }
    
    void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        camY = Mathf.Clamp(player.position.y, yMin + OrthVertical, yMax - OrthVertical);
        camX = Mathf.Clamp(player.position.x, xMin + OrthHorizontal, xMax - OrthHorizontal);
        mainCam.transform.position = new Vector3(camX, camY, mainCam.transform.position.z);
    }
}
