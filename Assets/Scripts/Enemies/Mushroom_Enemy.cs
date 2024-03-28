using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_Enemy : Enemy
{

  

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        WalkAround();

        CheckCollision();

        anim.SetFloat("xVelocity", rb.velocity.x);
    }
}
