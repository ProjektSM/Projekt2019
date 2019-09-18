using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Slime : Enemy
{
    // Start is called before the first frame update
    private GameObject player;
    private float startSpeed;
    new void Start()
    {
        Horizontal = -1;
        player = GameObject.FindGameObjectWithTag("Player");
        base.Start();
        startSpeed = Speed;
        
    }
    void CmdSpawn()
    {
        NetworkServer.Spawn(gameObject);
    }
    // Update is called once per frame
  
    void FixedUpdate()
    {
        
        RaycastHit2D hitH = Physics2D.Raycast(transform.position, new Vector2(Horizontal,0));
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, new Vector2(Horizontal, -1),0.8f);

        if (hitH.collider != null)
        {
            if (hitH.collider.tag != "Player" && !hitH.collider.isTrigger )
            {
                float distance = Mathf.Abs(hitH.distance);

                if (distance < 2)
                {
                    Horizontal = -Horizontal;
            
                }
            }
       
        }

        if (hitDown.collider == null)
        {
            Horizontal = -Horizontal;
        }
    }
}
