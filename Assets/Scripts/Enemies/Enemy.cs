using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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

    protected bool wallDetected;
    protected bool groundDetected;

    public bool invincible;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void Damage()
    {
        if (!invincible)
        {
            anim.SetTrigger("gotHit");
        }
    }

    public void Destroy_Me()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * _facingDirection, wallCheck.position.y));
    }
}
