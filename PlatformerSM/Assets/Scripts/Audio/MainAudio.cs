using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudio : MonoBehaviour
{
    private AudioSource source;

    [SerializeField]
    private AudioClip dyingClip;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void DyingSound()
    {
        source.clip = dyingClip;
        source.Play();
    }
}
