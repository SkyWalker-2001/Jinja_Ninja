using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneBee_Enemymy_Bee : Enemy
{
    [Header("Bee Spec")]

    [SerializeField] private Transform[] idlePoint;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheckPoint;
    [SerializeField] private float yOffset;
    private Transform player;
    private bool playerDetected;
    private bool attackOver;
    private float defauyltSpeed;

    [Header("Bullet Spec")]
    [SerializeField] private GameObject buller_Prefab;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private float bulletSpeed;

    protected override void Start()
    {
        base.Start();

        defauyltSpeed = speed;
    }

    private void Update()
    {
        idleTimeCounter -= Time.deltaTime;

        if (idleTimeCounter > 0)
        {
            return;
        }

        playerDetected = Physics2D.OverlapCircle(playerCheckPoint.position, checkRadius, whatIsPlayer);

        if (playerDetected)
        {
            aggresive = true;
        }

        if (!aggresive)
        {
            transform.position = Vector2.MoveTowards(transform.position, idlePoint[0].position, speed * Time.deltaTime);
        }
        else
        {
            if (!attackOver)
            {
                Vector2 newPos = new Vector2(player.transform.position.x, player.transform.position.y + yOffset);

                transform.position = Vector2.MoveTowards(transform.position, newPos, speed * Time.deltaTime);

                float xDifference = transform.position.x - player.position.x;

                if (Mathf.Abs(xDifference) < 1.5f)
                {
                    anim.SetTrigger("attack");
                }
            }
        }
    }

    private void AttackEvent()
    {
        GameObject newBullet = Instantiate(buller_Prefab, bulletPoint.transform.position, bulletPoint.transform.rotation);
        newBullet.GetComponent<Bullet>().SetupSpeed(0, -bulletSpeed);

        idleTimeCounter = idleTime;
        aggresive = false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(playerCheckPoint.position,checkRadius);
    }
}
