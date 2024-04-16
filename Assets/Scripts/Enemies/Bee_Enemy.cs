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
    [SerializeField] private float agroSpeed;

    private Transform player;
    private int idlePointIndex;
    private bool playerDetected;
    private float defauyltSpeed;


    [Header("Bullet Spec")]
    [SerializeField] private GameObject buller_Prefab;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private float bulletSpeed;

    protected override void Start()
    {
        base.Start();

        defauyltSpeed = speed;
        player = GameObject.Find("Player").transform;

    }

    private void Update()
    {
        bool idle = idleTimeCounter > 0;
        anim.SetBool("idle", idle);
        idleTimeCounter -= Time.deltaTime;

        if (idle)
        {
            return;
        }

        playerDetected = Physics2D.OverlapCircle(playerCheckPoint.position, checkRadius, whatIsPlayer);

        if (playerDetected && !aggresive)
        {
            aggresive = true;
            speed = agroSpeed;
        }

        if (!aggresive)
        {
            transform.position = Vector2.MoveTowards(transform.position, idlePoint[idlePointIndex].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, idlePoint[idlePointIndex].position) < .1f)
            {
                idlePointIndex++;

                if (idlePointIndex >= idlePoint.Length)
                {
                    idlePointIndex = 0;
                }
            }
        }
        else
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

    public void AttackEvent()
    {
        speed = defauyltSpeed;
        GameObject newBullet = Instantiate(buller_Prefab, bulletPoint.transform.position, bulletPoint.transform.rotation);
        newBullet.GetComponent<Bullet>().SetupSpeed(0, -bulletSpeed);
        Destroy(newBullet,3f);

        idleTimeCounter = idleTime;
        aggresive = false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(playerCheckPoint.position, checkRadius);
    }

 
}
