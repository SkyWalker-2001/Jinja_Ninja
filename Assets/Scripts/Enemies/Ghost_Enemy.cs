using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ghost_Enemy : Enemy
{
    [Header("Ghost Info")]
    [SerializeField] private float activeTime;
    private float activeTimeCounter = 4;

    private SpriteRenderer sr;

    [SerializeField] private float[] xOffset;

    protected override void Start()
    {
        base.Start();

        aggresive = true;
        invincible = true;

        sr = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if (player == null)
        {
            anim.SetTrigger("disappear");

            return;
        }

        activeTimeCounter -= Time.deltaTime;
        idleTimeCounter -= Time.deltaTime;

        if (activeTimeCounter > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (activeTimeCounter < 0 && idleTimeCounter < 0 && aggresive)
        {
            anim.SetTrigger("disappear");
            aggresive = false;
            idleTimeCounter = idleTime;
        }

        if (activeTimeCounter < 0 && idleTimeCounter < 0 && !aggresive)
        {
            ChoosePosition();
            anim.SetTrigger("appear");
            aggresive = true;
            activeTimeCounter = activeTime;
        }

        FlipController();
    }

    private void FlipController()
    {
        if (player == null)
        {
            return;
        }

        if (_facingDirection == -1 && transform.position.x < player.transform.position.x)
            Flip();
        else if (_facingDirection == 1 && transform.position.x > player.transform.position.x)
            Flip();
    }

    private void ChoosePosition()
    {
        float _xoffset = xOffset[Random.Range(0, xOffset.Length)];
        float _yOffset = Random.Range(-7, 7);
        transform.position = new Vector2(player.transform.position.x + _xoffset, player.transform.position.y + _yOffset);
    }

    public void Disappear()
    // => line
    {
        sr.enabled = false;
        //sr.color = Color.clear;
    }

    public void Appear()
    {
        sr.enabled = true;
        //sr.color = Color.white;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (aggresive)
            base.OnTriggerEnter2D(collision);
    }
}
