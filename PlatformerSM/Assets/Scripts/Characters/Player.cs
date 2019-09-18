using Assets.Scripts.SceneConfig;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : CharacterAbstract
{
    private Life life;
    private TimeMenager time;
    
    private int maxLife;
    public Life Life { get => life; }
    public int MaxLife { get => maxLife; }
    public bool isLocal;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        isLocal = true;
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        life = GameObject.FindObjectOfType<Life>();
        camera.GetComponent<PlayerCamera>().followingObject = gameObject;
        camera.GetComponent<PlayerCamera>().enabled = true;
        time = GameObject.FindObjectOfType<TimeMenager>();


        if (Application.platform == RuntimePlatform.Android)
        {
            UnityEvent jump = new UnityEvent();
            UnityEvent left = new UnityEvent();
            UnityEvent right = new UnityEvent();
            jump.AddListener(Jump);
            left.AddListener(MoveLeft);
            right.AddListener(MoveRight);
            GameObject.Find("ButtonJump").GetComponent<OnPress>().onLongPress = jump;
            GameObject.Find("ButtonLeft").GetComponent<OnPress>().onLongPress = left;
            GameObject.Find("ButtonRight").GetComponent<OnPress>().onLongPress = right;
            GameObject.Find("ButtonTime").GetComponent<Button>().onClick.AddListener(UseSlowmotion);
            
        }
        maxLife = 4;
    }
    void UseSlowmotion()
    {
        time.UseSlowmotion = true;
    }

    void MoveLeft()
    {
        Horizontal = -1;
    }
    void MoveRight()
    {
        Horizontal = 1;
    }
    GameObject GetChildWithName(string name)
    {
        Transform trans = transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
    private new void Update()
    {
        base.Update();
       
       if (hasAuthority)
       {
            IsJumping = Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire2");
            Horizontal = Input.GetAxisRaw("Horizontal");
            if (transform.position.y < -100)
            {
                SceneConfig.KillPlayer();
            }
        }
       
       
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasAuthority)
        {
            if (collision.collider.tag == "Enemy")
            {
                IsHurting = true;
                if (collision.transform.position.x > transform.position.x)
                {
                    rb.AddForce(new Vector2(-400, 0));
                }
                else if (collision.transform.position.x <= transform.position.x)
                {
                    rb.AddForce(new Vector2(400, 0));
                }
                
                life.CurrentHP--;
            }
        }
    }
}
