using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyncPosition : NetworkBehaviour
{
    private Rigidbody2D myTransform;
    private CharacterAbstract character;
    [SerializeField]
    float lerpRate = 5;
    [SyncVar]
    private Vector2 syncPos;
    [SyncVar]
    private Vector2 syncVelocity;

    private Vector3 lastPos;


    void Start()
    {
        myTransform = GetComponent<Rigidbody2D>();
        syncPos = GetComponent<Transform>().position;
        character = GetComponent<Player>();
       
    }


    void FixedUpdate()
    {
        TransmitPosition();
        LerpPosition();
    }

    void LerpPosition()
    {
        if (!hasAuthority)
        {
            float steps = 1 - (1/Vector2.Distance(myTransform.position, syncPos));
            myTransform.position = Vector2.Lerp(myTransform.position, syncPos, steps);

            
            myTransform.velocity = syncVelocity;
            if (Vector2.Distance(myTransform.position, syncPos) > 1f)
            {
                myTransform.velocity = Vector2.zero;
            }
        }
    }

    [Command]
    void Cmd_ProvidePositionToServer(Vector2 pos)
    {
        syncPos = pos;
    }

    [Command]
    void Cmd_ProvideVelocityToServer(Vector2 velocity)
    {
        syncVelocity = velocity;
    }
    [ClientCallback]
    void TransmitPosition()
    {
        if (hasAuthority)
        {
            Cmd_ProvidePositionToServer(myTransform.position);
            Cmd_ProvideVelocityToServer(myTransform.velocity);

            lastPos = myTransform.position;
        }
    }
}
