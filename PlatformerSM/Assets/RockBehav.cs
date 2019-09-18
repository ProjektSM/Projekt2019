using Assets.Scripts.SceneConfig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehav : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float fallSpeed = GetComponent<Rigidbody2D>().velocity.y;
        if (collision.collider.tag == "Player" && fallSpeed < -1)
        {
            SceneConfig.KillPlayer();
        }
    }
}
