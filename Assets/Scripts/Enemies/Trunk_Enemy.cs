
using UnityEngine;

public class Trunk_Enemy : Enemy
{
    [Header("Trunk spec")]
   [SerializeField] private float retreatBackTime;
   private float retreatTimeCounter;

    [Header("Collision system")]
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform groundBehindCheck;
    private bool wallBehind;
    private bool groundBehind;

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
        retreatTimeCounter -= Time.deltaTime;

        if(playerDetection && retreatTimeCounter < 0)
            retreatTimeCounter = retreatBackTime;
        if (playerDetection.collider.GetComponent<Player_Controller>() != null)
        {
            if (attackCoolDownCounter < 0)
            {
                attackCoolDownCounter = attackCoolDown;
                anim.SetTrigger("attack");
                canMove = false;
            }
            else if (playerDetection.distance < 3)
            {
                MoveBackwards(1.5f);
            }
        }
        else
        {
            if(retreatTimeCounter > 0)
                MoveBackwards(4);

            WalkAround();
        }

        anim.SetFloat("xVelocity", rb.velocity.x);

    }
    private void MoveBackwards(float multiplier)
    {
        if (wallBehind)
            return;

        if (!groundBehind)
            return;

        rb.velocity = new Vector2(speed * multiplier * -_facingDirection, rb.velocity.y);
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
        groundBehind = Physics2D.Raycast(groundBehindCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallBehind = Physics2D.Raycast(wallCheck.position, Vector2.right * (-_facingDirection + 1), wallCheckDistance, whatIsGround);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(transform.position, checkRadius);
        Gizmos.DrawLine(groundBehindCheck.position, new Vector2(groundBehindCheck.position.x, groundBehindCheck.position.y - groundCheckDistance));
    }
}
