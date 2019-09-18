using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private bool facing;
    [SerializeField]//[Range(0, .3f)]
    private float lenght;

    [SerializeField][Range(0, .2f)]
    private float speed;

    private float startX;

    private bool isStart;
    void Start()
    {
        startX = transform.position.x;
        StartCoroutine(StartSlowdown());
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (facing)
            {
                collision.transform.position += Vector3.left * speed * Time.deltaTime * 50;
            }
            else
            {
                collision.transform.position += Vector3.right * speed * Time.deltaTime * 50;
            }
        }
    }
    void FixedUpdate()
    {
        if (isStart)
        {
            float currentLenght = startX - transform.position.x;

            if (currentLenght > lenght || currentLenght < -lenght)
            {
                facing = !facing;
            }

            Debug.Log(Time.deltaTime);
            if (facing)
            {
                transform.position += Vector3.left * speed*Time.deltaTime*50;
            }
            else
            {
                transform.position += Vector3.right * speed * Time.deltaTime*50;
            }
        }
    }


    public IEnumerator StartSlowdown()
    {
        float slowdown = Random.Range(0f, 4f);
        yield return new WaitForSeconds(slowdown);
        isStart = true;
    }

   
}
