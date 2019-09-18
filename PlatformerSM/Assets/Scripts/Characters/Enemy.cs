using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy : CharacterAbstract
{
    [SyncVar]
    public bool isAlive;
    private CapsuleCollider2D polygonCollider2D;
    private BoxCollider2D boxCollider2D;

    public override void OnStartClient()
    {
        polygonCollider2D = GetComponent<CapsuleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        if(isAlive)
        {
            polygonCollider2D.enabled = true;
            boxCollider2D.enabled = true;
        }
        else
        {
            polygonCollider2D.enabled = false;
            boxCollider2D.enabled = false;
        }
    }
    new void Update()
    {
        base.Update();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            IsHurting = true;
            rb.freezeRotation = false;
            CapsuleCollider2D polygonCollider2D = GetComponent<CapsuleCollider2D>();
            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
            polygonCollider2D.enabled = false ;
            boxCollider2D.enabled = false;
            Horizontal = 0;
            rb.AddForce(new Vector2(0, 500));
            rb.AddForceAtPosition(new Vector2(100, 0),Vector2.zero);
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000));
        }
    }
}
