using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAudio : MonoBehaviour
{
    public AudioClip rockClip;
    private AudioSource source;
    private Rigidbody2D rb;

    void Start()
    {

        source = GetComponent<AudioSource>();
        source.clip = rockClip;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            source = GetComponent<AudioSource>();
            rb = GetComponent<Rigidbody2D>();
            float velocityY = rb.velocity.y;
          
            if (velocityY < 3f && velocityY > -3f)
            {
                source.Play();
            }
        }
    }

}
