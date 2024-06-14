using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit_DroppedByPlayer : Fruit_Item
{
    [SerializeField] private Vector2 speed;
    [SerializeField] protected  Color transparentColor;
    protected bool canPickUp;

    protected virtual void Start()
    {
        StartCoroutine(BlinkImage());
    }

    private void Update()
    {
        transform.position += new Vector3(speed.x, speed.y) * Time.deltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (canPickUp)
            base.OnTriggerEnter2D(other);
    }

    protected virtual IEnumerator BlinkImage()
    {
        anim.speed = 0;
        sr.color = transparentColor;

        speed.x *= -1;
        yield return new WaitForSeconds(.1f);
        sr.color = Color.white;

        speed.x *= -1;
        yield return new WaitForSeconds(.1f);
        sr.color = transparentColor;

        speed.x *= -1;
        yield return new WaitForSeconds(.1f);
        sr.color = Color.white;

        speed.x *= -1;
        yield return new WaitForSeconds(.1f);
        sr.color = transparentColor;

        speed.x *= -1;
        yield return new WaitForSeconds(.2f);
        sr.color = Color.white;

        speed.x *= -1;
        yield return new WaitForSeconds(.2f);
        sr.color = transparentColor;

        speed.x *= -1;
        yield return new WaitForSeconds(.3f);
        sr.color = Color.white;

        speed.x *= -1;
        yield return new WaitForSeconds(.3f);
        sr.color = transparentColor;

        speed.x *= -1;
        yield return new WaitForSeconds(.3f);
        sr.color = Color.white;

        speed.x *= -1;
        yield return new WaitForSeconds(.3f);

        speed.x = 0;

        anim.speed = 1;
        canPickUp = true;
    }
}
