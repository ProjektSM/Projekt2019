using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public abstract class CharacterAbstract : NetworkBehaviour
{
    private bool isHurting;
    private bool isJumpSound;
    private bool isMoveSound;
    public bool IsHurting { get => isHurting; set => isHurting = value; }
    public bool IsJumpSound { get => isJumpSound; set => isJumpSound = value; }
    public bool IsMoveSound { get => isMoveSound; set => isMoveSound = value; }

    [Range(0f,1f)] [SerializeField] private float speed = 1;
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.

    const float k_GroundedRadius = .3f; // Radius of the overlap circle to determine if grounded
    [SerializeField]
    private bool grounded;            // Whether or not the player is grounded.
    private float horizontal;

    protected  Rigidbody2D rb;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    protected Animator animator;
    private float localTimeScale = 1;

    public float LocalTimeScale { get => localTimeScale; set => localTimeScale = value; }

    private float prevHorizontal = 0;
    private Vector2 beginPosition;//Where player starts level
    private Vector2 spawnPosition;//Position where player will restart

    public float Horizontal
    {
        get => horizontal;
        set
        {
            
                prevHorizontal = horizontal;
                horizontal = value;
            
            
        }
    }

    public bool IsJumping { get; set; }
    public Vector2 SpawnPosition { get => spawnPosition; set => spawnPosition = value; }
    public float Speed { get => speed; set => speed = value; }
    public Vector2 BeginPosition {get => beginPosition;}
    public bool Grounded { get => grounded; set => grounded = value; }

    protected void Start()
    {
        beginPosition = spawnPosition = transform.position;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

 

    protected void Update()
    {
        
       
        MoveHorizontaly(Horizontal);
        if (IsJumping)
        {
            isJumpSound = true;
            Jump();
            IsJumping = false;
        }

        animator.SetBool("isJumping", !Grounded);

        Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject && !colliders[i].isTrigger)
            {
                Grounded = true;
                break;
            }
        }
      
    }
    private float prevScale; 

    public void MoveHorizontaly(float move)
    {
        Vector3 targetVelocity;
        if(move != 0)
            isMoveSound = true;
        if (prevScale != LocalTimeScale)
        {
            rb.velocity = new Vector2(move * 10 * speed * LocalTimeScale, 0f);
        }
        else
        {
            targetVelocity = new Vector2(move * 10 * speed * LocalTimeScale, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing / LocalTimeScale);
        }
      
    
        prevScale = LocalTimeScale;


        

        if (Mathf.Abs(rb.velocity.x) > 0.8f)
        {
            animator.SetBool("move", true);
        }
        else
        {
            animator.SetBool("move", false);
        }
        // player facing
        if (rb.velocity.x >= 1 && !m_FacingRight)
        {
            Flip();
        }
        else if (rb.velocity.x <= -1 && m_FacingRight)
        {
            Flip();
        }


    }
    public void Jump()
    {
        
        if (Grounded)
        {
            
            // Add a vertical force to the player.
            Grounded = false;

            rb.velocity = (new Vector2(rb.velocity.x, m_JumpForce*LocalTimeScale));

        }
    }

    private void Flip()
    {
        GetComponent<SpriteRenderer>().flipX = m_FacingRight;
        m_FacingRight = !m_FacingRight;

        
    }
}
