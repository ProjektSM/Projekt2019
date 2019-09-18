using Assets.Scripts.SceneConfig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private GameObject player;
    float step;
    float stepSpeed;
    void Start()
    {
        step = Random.Range(0.001f, 0.01f);
    }
    public void Instantiate(GameObject player)
    {
        this.player = player;
    }
    void FixedUpdate()
    {
   
        Vector3 destination = Vector3.MoveTowards(transform.position, player.transform.position, step);
 

        Vector3 vectorToTarget = player.transform.position - transform.position;
        if(vectorToTarget.x>0)
        {
            Vector3 theScale = transform.localScale;
            theScale.y = 6;
            transform.localScale = theScale;
        }
        else
        {
            Vector3 theScale = transform.localScale;
            theScale.y = -6;
            transform.localScale = theScale;
        }
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 20);

        transform.position = destination;
        step += 0.0001f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
        
            SceneConfig.KillPlayer();
        }
    }
}
