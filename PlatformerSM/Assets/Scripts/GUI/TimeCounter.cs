using Assets.Scripts.SceneConfig;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeCounter : NetworkBehaviour
{
    private float startTime;
    [SyncVar]
    private float maxTime;

    private Text text;

    public float LastTime { get => maxTime; }

    void Start()
    {
        text = GetComponent<Text>();
        startTime = maxTime = float.Parse(text.text)/10;
    }


    // Update is called once per frame
    void Update()
    {
        maxTime -= Time.deltaTime;
        string time = maxTime.ToString();
        try
        {
            int toRemove;
            if (Application.platform != RuntimePlatform.Android)
            {
                toRemove = time.IndexOf(',');
            }
            else
            {
                toRemove = time.IndexOf('.');
            }

            time = time.Remove(toRemove, 1);
            time = time.Remove(toRemove + 1);

        }
        catch (Exception) { }
        text.text = time;
     
      

        // Reset Level when time ends
        if(maxTime<=0f)
        {
            maxTime = startTime;
            SceneConfig.ResetLevelALL();
        }
    }
}
