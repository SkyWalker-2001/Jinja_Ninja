using UnityEngine;

public class Rhino_Enemy : Enemy
{

    [Header("Rhino Info")]
    [SerializeField] private float aggresive_Speed;


    [SerializeField] private float shockTime;
    private float shockTimeCounter;



    protected override void Start()
    {
        base.Start();
        invincible = true;
    }

    private void Update()
    {

        if (playerDetection.collider.GetComponent<Player_Controller>() != null)
            aggresive = true;

        if (!aggresive)
        {

            WalkAround();
        }

        else
        {
            if (!groundDetected)
            {
                aggresive = false;
                Flip();
            }

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
                aggresive = false;
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


}
