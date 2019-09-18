using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DoorKey : NetworkBehaviour
{
    [SyncVar]
    private bool isAlive = true;
    private AudioSource source;
    public AudioClip keySound;

    private void Start()
    {
        if(!isAlive)
        {
            Cmd_Destroy();
        }
        source = transform.parent.gameObject.GetComponent<AudioSource>();
        source.clip = keySound;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            source.Play();
            Cmd_Destroy();
        }
    }
    [Command]
    private void Cmd_Destroy()
    {
          transform.parent.GetComponent<ClosedDoor>().keyRemoved();
          Destroy(this.gameObject);
    }
}
