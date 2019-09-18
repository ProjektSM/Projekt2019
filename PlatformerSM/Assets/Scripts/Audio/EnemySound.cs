using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    private CharacterAbstract enemy;
    private AudioSource source;

    public AudioClip hurtClip;

    void Start()
    {
        enemy = GetComponent<CharacterAbstract>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.IsHurting)
        {
            source.clip = hurtClip;

            source.Play();
            enemy.IsHurting = false;
        }

    }
}
