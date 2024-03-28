using UnityEngine;

public class Rhino_Enemy : Enemy
{

    [Header("Rhino Info")]
    [SerializeField] private float aggresive_Speed;


    [SerializeField] private float shockTime;
    private float shockTimeCounter;


    private RaycastHit2D playerDetection;
    private bool isAggresive;

    protected override void Start()
    {
        base.Start();
        invincible = true;
    }

    private void Update()
    {
        playerDetection = Physics2D.Raycast(wallCheck.position, Vector2.right * _facingDirection, 25, ~whatToIgnore);

        if (playerDetection.collider.GetComponent<Player_Controller>() != null)
            isAggresive = true;

        if (!isAggresive)
        {

            WalkAround();
        }

        else
        {
            rb.velocity = new Vector2(aggresive_Speed * _facingDirection, rb.velocity.y);

            if (wallDetected && invincible)
            {
                invincible = false;
                shockTimeCounter = shockTime;
            }

            if (shockTimeCounter <= 0 && !invincible)
            {
                invincible = true;
                Flip();
                isAggresive = false;
            }
        }

        shockTimeCounter -= Time.deltaTime;

        CheckCollision();
        Animator_Controller();
    }

    private void Animator_Controller()
    {
        anim.SetBool("invincible", invincible);
        anim.SetFloat("xVelocity", rb.velocity.x);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos(); 

        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + playerDetection.distance * _facingDirection, wallCheck.position.y));
    }
}
