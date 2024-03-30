using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Enemy : Enemy
{
    protected override void Start()
    {
        base.Start();
    }


    void Update()
    {
        CheckCollision();
         
        idleTimeCounter -= Time.deltaTime;

        bool playerDetected = playerDetection.collider.GetComponent<Player_Controller>() != null;

        if (idleTimeCounter < 0 && playerDetected)
        {
            idleTimeCounter = idleTime;
            anim.SetTrigger("attack");
        }
    }

    public void AttackEvent()
    {
        Debug.Log("Attack" + playerDetection.collider.name); 
    }
}
