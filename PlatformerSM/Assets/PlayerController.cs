using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private Player playerPref;
    [SerializeField]
    private GameObject guiPref;
    [SerializeField]
    private GameObject timeMenagerPref;

    private Player player;
    private void Start()
    {
        
        if (!isLocalPlayer)
        {
            return;
        }
        CmdspawnOnServer();
       
    }
    
    [Command]
    void CmdspawnOnServer()
    {
        player = Instantiate(playerPref,transform.position,transform.rotation);

        TimeMenager timeMenager = FindObjectOfType<TimeMenager>();
        NetworkServer.Spawn(timeMenager.gameObject);
        NetworkServer.SpawnWithClientAuthority(player.gameObject, connectionToClient);
    }
}
