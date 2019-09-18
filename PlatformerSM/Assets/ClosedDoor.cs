using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosedDoor : MonoBehaviour
{
    private int KeyNum;
    Text guiText;
    void Start()
    {
        guiText = GameObject.Find("NumKeys").GetComponent<Text>();
        KeyNum = transform.childCount;
        guiText.text = KeyNum.ToString();
        if (KeyNum <= 0)
        {
            OpenDoor();
        }
    }

    public void keyRemoved()
    {
        KeyNum = transform.childCount-1;
        guiText.text = KeyNum.ToString();

        if (KeyNum <= 0)
        {
            OpenDoor();
        }
    }
    public void OpenDoor()
    {
        Destroy(this.gameObject);
    }
}
