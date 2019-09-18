using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScene : MonoBehaviour
{
    private Text IPADDRES;

    [SerializeField]
    private bool isHost;
    [SerializeField]
    private int sceneNumber;

    private void Start()
    {

        if (MyNetworkMenager.hostIP != null)
        {
            IPADDRES.text = MyNetworkMenager.hostIP;
        }
    }

    public void Press()
    {
        if (isHost)
            StartHost();
        else
            StartClient();
    }


    private void StartHost()
    {
        MyNetworkMenager.isHost = true;
        SceneManager.LoadScene(sceneNumber);
    }
    private void StartClient()
    {
        IPADDRES = GameObject.Find("IPADDRES").GetComponent<Text>();
        MyNetworkMenager.isHost = false;
        MyNetworkMenager.hostIP = IPADDRES.text;
        SceneManager.LoadScene(sceneNumber);
    }
}
