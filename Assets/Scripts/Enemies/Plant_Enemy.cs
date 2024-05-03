using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Enemy : Enemy
{
    [Header("Bullet_Plant Info")]
    [SerializeField] private GameObject buller_Prefab;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private float bullet_Speed;

    [SerializeField] private bool facingRight;


    protected override void Start()
    {
        base.Start();

        if (facingRight)
            Flip();
    }


    void Update()
    {
        CheckCollision();

        if(!playerDetection){
            return;
        }
         
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
        GameObject newBullet = Instantiate(buller_Prefab, bulletPoint.transform.position, bulletPoint.transform.rotation);
        newBullet.GetComponent<Bullet>().SetupSpeed(bullet_Speed * _facingDirection, 0);
    }
}
