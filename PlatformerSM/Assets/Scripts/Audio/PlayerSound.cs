using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;
    private AudioSource source;

    public AudioClip hurtClip;
    public AudioClip moveClip;
    public AudioClip jumpClip;

    void Start()
    {
        player = GetComponent<Player>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
  
        if(player.IsHurting)
        {
            source.clip = hurtClip;

            source.Play();
            source.volume = 1f;
            

        }
        else if(player.IsJumpSound)
        {
            source.clip = jumpClip;
            source.volume = 1f;
            source.Play();
        }
        else if (player.IsMoveSound && player.Grounded)
        {
            if (!source.isPlaying)
            {
                source.clip = moveClip;
                source.volume = 0.8f;
                source.PlayDelayed(0.05f);
            }
        }

        player.IsHurting = false;
        player.IsMoveSound = false;
        player.IsJumpSound = false;
    }
}
