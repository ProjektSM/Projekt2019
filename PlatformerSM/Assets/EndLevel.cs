using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField]
    private int sceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            NetworkManager.FindObjectOfType<NetworkManager>().StopHost();
            SceneManager.LoadScene(sceneName);
        }
    }
}
