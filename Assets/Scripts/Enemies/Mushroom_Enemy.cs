using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_Enemy : Enemy
{
    [SerializeField] private float speed;
    [SerializeField] private float idleTime = 2f;
                     private float idleTimeCounter;

    protected override void Start()
    {
        base.Start();
        _facindDirection = -1;
    }

    private void Update()
    {
        if(idleTimeCounter <= 0)
        {
            rb.velocity = new Vector2(speed * _facindDirection, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        idleTimeCounter -= Time.deltaTime;

        CheckCollision();

        if (wallDetected || !groundDetected)
        {
            idleTimeCounter = idleTime;
            Flip();
        }

        anim.SetFloat("xVelocity", rb.velocity.x);
    }
}
