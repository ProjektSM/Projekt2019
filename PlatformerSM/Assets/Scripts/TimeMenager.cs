using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TimeMenager : NetworkBehaviour
{
    private Player[] skipped;
    // Update is called once per frame
    [SerializeField]
    private GameObject enemySpawnedPrefab;
    //[SyncVar]
    private bool useSlowmotion = false;
    private bool disableSlowmotion = false;
    public bool UseSlowmotion { get => useSlowmotion; set => useSlowmotion = value; }
    public bool DisableSlowmotion { get => disableSlowmotion; set => disableSlowmotion = value; }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Debug.Log("!");
           useSlowmotion = true;
          
        }
     
    }
    private void FixedUpdate()
    {
        if(useSlowmotion)
        {
            Debug.Log(isLocalPlayer);
            if (isServer)
                RpcSlowMotion(0.1f);
            else
                CmdSlowMotion(0.1f);
           
         
            useSlowmotion = false;
        }
        if(disableSlowmotion)
        {
            disableSlowmotion = false;
            if (isServer)
                RpcSlowMotion(1f);
            else
                CmdSlowMotion(1f);
        }
    }

    [Command]
    private void CmdSlowMotion(float scale)
    {
        RpcSlowMotion(scale);
    }

    [ClientRpc]
    private void RpcSlowMotion(float scale)
    {
        skipped = GameObject.FindObjectsOfType<Player>();

        if (scale == Time.timeScale)
            scale = 1;


        if (scale == 1)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        else
        {
            foreach (Player player in skipped)
            { 
                Vector3 ghostDestination = player.transform.position;
                ghostDestination -= new Vector3(10, 10, 0);
                GameObject ghost = Instantiate(enemySpawnedPrefab, ghostDestination, transform.rotation, transform);
                ghost.GetComponent<Ghost>().Instantiate(player.gameObject);

                ghostDestination = player.transform.position;
                ghostDestination -= new Vector3(10, -10, 0);
                ghost = Instantiate(enemySpawnedPrefab, ghostDestination, transform.rotation, transform);
                ghost.GetComponent<Ghost>().Instantiate(player.gameObject);

                ghostDestination = player.transform.position;
                ghostDestination -= new Vector3(-10, -10, 0);
                ghost = Instantiate(enemySpawnedPrefab, ghostDestination, transform.rotation, transform);
                ghost.GetComponent<Ghost>().Instantiate(player.gameObject);

                ghostDestination = player.transform.position;
                ghostDestination -= new Vector3(-10, 10, 0);
                ghost = Instantiate(enemySpawnedPrefab, ghostDestination, transform.rotation, transform);
                ghost.GetComponent<Ghost>().Instantiate(player.gameObject);

            }

        }
        ///////////////////////////////////////////////////////////////////////////////

       
       

        foreach (Player player in skipped)
        {
            
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            Animator animator = player.GetComponent<Animator>();

            //rb.velocity = Vector2.zero;
            //player.Horizontal = 0;
            //player.IsJumping = false;
            //rb.gravityScale = 0;
            //rb.mass = 0;
            float localTimeScale = 1;
            if (scale != 0)
            {
                localTimeScale = 1 / scale;
            }
            animator.speed = localTimeScale;


            if (localTimeScale != 1)//if slowmotion
            {
                rb.gravityScale = 30 * localTimeScale;
                rb.mass = 1 / localTimeScale;
                player.GetComponent<TrailRenderer>().emitting = true;
            }
            else
            {

                rb.mass = 1;
                rb.gravityScale = 3;
                player.GetComponent<TrailRenderer>().emitting = false;
            }
            player.GetComponent<CharacterAbstract>().LocalTimeScale = localTimeScale;
        }
        Time.timeScale = scale;
        Time.fixedDeltaTime = 0.02f * scale;
    }
}
