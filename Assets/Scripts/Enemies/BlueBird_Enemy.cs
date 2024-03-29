using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBird_Enemy : Enemy
{
    private RaycastHit2D ceillingDetected;

    [Header("BlueBird Info")]
    [SerializeField] private float ceillingDistance;
    [SerializeField] private float groundDistance;

    [SerializeField] private float flyUpForce;
    [SerializeField] private float flyDownForce;
                     private float flyForce;

    private bool canFly = true;

    public override void Damage()
    {
        canFly = false;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        base.Damage();
    }

    protected override void Start()
    {
        base.Start();
        flyForce = flyUpForce; 
    }

    private void Update()
    {
        CheckCollision();

        if (ceillingDetected)
        {
            flyForce = flyDownForce;
        }

        else if(groundDetected)
        {
            flyForce = flyUpForce;
        }

        if (wallDetected)
        {
            Flip();
        }
    }

    [SerializeField] private Transform movePoint;
    [SerializeField] private float xMultiplier;
    [SerializeField] private float yMultiplier;

    public void FlyUpEvent()
    {
        if(canFly)
            rb.velocity = new Vector2(speed * _facingDirection, flyForce);

        /*if (canFly)
        {
            Vector2 direction = transform.position - movePoint.position;
            rb.velocity = new Vector2(-direction.x * xMultiplier, -direction.y * yMultiplier);
        }*/
    }

    protected override void CheckCollision()
    {
        base.CheckCollision();

        ceillingDetected = Physics2D.Raycast(transform.position, Vector2.up, ceillingDistance, whatIsGround);
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + ceillingDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundDistance));
    }
}
