
using UnityEngine;

public class Trunk_Enemy : Enemy
{
    [Header("Trunk spec")]
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsPlayer;

    private bool checkPlayer;

    [Header("Bullet spec")]
    [SerializeField] private float attackCoolDown;
    private float attackCoolDownCounter;
    [SerializeField] private float bullet_Speed;
    [SerializeField] private GameObject bullet_Prefab;
    [SerializeField] private Transform bulletOriginPoint;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        CheckCollision();

        if (!canMove)
        {
            rb.velocity = new Vector2(0, 0);
        }

        attackCoolDownCounter -= Time.deltaTime;

        if (playerDetection.collider.GetComponent<Player_Controller>() != null)
        {
            if (attackCoolDownCounter < 0)
            {
                attackCoolDownCounter = attackCoolDown;
                anim.SetTrigger("attack");
                canMove = false;
            }
        }
        else
        {
            WalkAround();
        }

        anim.SetFloat("xVelocity", rb.velocity.x);

    }

    private void AttackEvent()
    {
        GameObject newBullet = Instantiate(bullet_Prefab, bulletOriginPoint.transform.position, bulletOriginPoint.transform.rotation);
        newBullet.GetComponent<Bullet>().SetupSpeed(bullet_Speed * _facingDirection, 0);
        Destroy(newBullet, 3f);
    }

    private void ReturnMovement()
    {
        canMove = true;
    }

    protected override void CheckCollision()
    {
        base.CheckCollision();

        checkPlayer = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
    }
}
