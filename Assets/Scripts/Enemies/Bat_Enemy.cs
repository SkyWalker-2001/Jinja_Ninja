using System.Data.Common;
using UnityEngine;

public class Bat_Enemy : Enemy
{

    [Header("Bat Spec")]
    [SerializeField] private Transform[] idlePoint;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsPlayer;
    private Transform player;
    private bool playerDetected;
    private Vector2 destination;
    private bool canBeAggressive = true;

    float defaultSpeed;

    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player").transform;
        defaultSpeed = speed;
        destination = idlePoint[0].position;
        transform.position = idlePoint[0].position;
    }

    private void Update()
    {
        anim.SetBool("canBeAggressive", canBeAggressive);
        anim.SetFloat("speed", speed);

        idleTimeCounter -= Time.deltaTime;

        if (idleTimeCounter > 0)
        {
            return;
        }

        playerDetected = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);

        if (playerDetected && !aggresive && canBeAggressive)
        {
            aggresive = true;
            canBeAggressive = false;
            destination = player.transform.position;
        }

        if (aggresive)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, destination) < .1f)
            {
                aggresive = false;

                int i = Random.Range(0, idlePoint.Length);

                destination = idlePoint[i].position;

                speed = speed * .5f;
            }
        }

        else
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, destination) < .1f)
            {
                if (!canBeAggressive)
                {
                    idleTimeCounter = idleTime;
                }

                canBeAggressive = true;

                speed = defaultSpeed;
            }

        }

        FlipController();

    }

    public override void Damage()
    {
        base.Damage();

        idleTimeCounter = 5;
    }

    private void FlipController()
    {
        if (_facingDirection == -1 && transform.position.x < destination.x)
            Flip();
        else if (_facingDirection == 1 && transform.position.x > destination.x)
            Flip();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
