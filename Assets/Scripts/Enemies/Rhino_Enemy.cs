using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino_Enemy : Enemy
{
    
    [Header("Move Info")]
    [SerializeField] private float speed;
    [SerializeField] private float idleTime = 2f;
                     private float idleTimeCounter;

    private bool isAggresive;

    protected override void Start()
    {
        base.Start();
        invincible = true;
    }

    private void Update()
    {
        if(!isAggresive)
        {

        
        if (idleTimeCounter <= 0)
        {
            rb.velocity = new Vector2(speed * _facindDirection, rb.velocity.y);
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

        CheckCollision();
        anim.SetFloat("xVelocity", rb.velocity.x);
    }
}
