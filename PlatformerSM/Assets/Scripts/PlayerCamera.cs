using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{


    public GameObject followingObject;

    [Range(0.9f, 0.999f)][SerializeField]
    private float smoothness = 0.9f;

    private void Start()
    {


        if (followingObject == null)
        {
            enabled = false;
        }
    }
    Vector2 destination;
    void FixedUpdate()
    { 
        try
        {
            Vector2 followingObjectPosition = new Vector2(followingObject.transform.position.x, followingObject.transform.position.y);
            Vector2 cameraPosition = new Vector2(transform.position.x, transform.position.y);
            destination = Vector2.Lerp(followingObjectPosition, cameraPosition, smoothness);
        }
        catch(Exception)
        {
            enabled = false;
        }
        

    }
    private void LateUpdate()
    {
        transform.position = new Vector3(destination.x, destination.y, -10);
    }

}
