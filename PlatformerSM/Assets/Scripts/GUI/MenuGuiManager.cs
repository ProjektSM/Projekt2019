using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGuiManager : MonoBehaviour
{
    Text IPADDRES;
    private void Start()
    {
        IPADDRES = GameObject.Find("IPADDRES").GetComponent<Text>();
        if(MyNetworkMenager.hostIP != null)
        {
            IPADDRES.text = MyNetworkMenager.hostIP;
        }
    }
    public void StartHost()
    {
        MyNetworkMenager.isHost = true;
        SceneManager.LoadScene("LVL_2");
    }
    public void StartClient()
    {
        MyNetworkMenager.isHost = false;
        MyNetworkMenager.hostIP = IPADDRES.text;
        SceneManager.LoadScene("LVL_2");
    }
}
