using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MyNetworkMenager : MonoBehaviour
{
    NetworkManager networkManager;
    void Start()
    {
        networkManager = GetComponent<NetworkManager>();
        //networkManager.StartHost();
        if(isHost)
        {
            networkManager.StartHost();
        }
        else
        {
            if (hostIP != null)
                if (hostIP.Length > 0)
                {
                    Debug.Log(hostIP);
                    networkManager.networkAddress = hostIP;
                }
            networkManager.StartClient();
            
        }
    }
    static public bool isHost = true;
    static public string hostIP;
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Backspace))
        {
            if (isHost)
            {
                networkManager.StopHost();
            }
            else
            {
                networkManager.StopClient();
            }
            SceneManager.LoadScene("Menu");
        }
    }
}
