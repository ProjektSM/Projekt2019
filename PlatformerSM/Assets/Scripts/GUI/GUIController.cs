using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GUIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject buttons = GameObject.Find("Buttons");
        if (Application.platform != RuntimePlatform.Android)
        {
            buttons.SetActive(false);
        }
        else
        {
            buttons.SetActive(true);
        }


    }


}
