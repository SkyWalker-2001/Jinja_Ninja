using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Danger
{
    protected Rigidbody2D rb;
    protected Animator anim;

    // -1 If the Sprite Facing Left Side
    protected int _facingDirection = -1;

    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;

    protected RaycastHit2D playerDetection;

    protected bool wallDetected;
    protected bool groundDetected;

    [HideInInspector] public bool invincible;

    [Header("Move Info")]
    [SerializeField] protected float speed;
    [SerializeField] protected float idleTime = 2f;
                     protected float idleTimeCounter;

    [SerializeField] protected LayerMask whatToIgnore;

    protected bool canMove = true;
    protected bool aggresive;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (groundCheck == null)
            groundCheck = transform;
        if(wallCheck == null)
            wallCheck = transform;
    }

    protected virtual void WalkAround()
    {
        if (idleTimeCounter <= 0 && canMove)
        {
            rb.velocity = new Vector2(speed * _facingDirection, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        idleTimeCounter -= Time.deltaTime;


        if (wallDetected || !groundDetected)
        {
            idleTimeCounter = idleTime;
            Flip();
        }
    }

    public virtual void Damage()
    {
        if (!invincible)
        {
            canMove = false;
            anim.SetTrigger("gotHit");
        }
    }

    public void Destroy_Me()
    {
        Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Controller>() != null)
        {
            Player_Controller player = collision.GetComponent<Player_Controller>();

            player.KnockBack(transform);
        }
    }


    protected virtual void Flip()
    {
        _facingDirection = _facingDirection * -1;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void CheckCollision()
    {
        groundDetected = Physics2D.Raycast(groundCheck .position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck .position, Vector2.right * _facingDirection, wallCheckDistance, whatIsGround);
        playerDetection = Physics2D.Raycast(wallCheck.position, Vector2.right * _facingDirection, 100, ~whatToIgnore);

    }

    protected virtual void OnDrawGizmos()
    {
        if(groundCheck != null)
            Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));

        if(wallCheck != null)
        {
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * _facingDirection, wallCheck.position.y));
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + playerDetection.distance * _facingDirection, wallCheck.position.y));
        }

    }
}
