using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit_Dropped_By_Enemy : Fruit_DroppedByPlayer
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Vector2[] dropDirection;
    [SerializeField] private float force;

    protected override void Start()
    {
        // _rb.GetComponentInParent<Rigidbody2D>();

        base.Start();

        int random = Random.Range(0, dropDirection.Length);
        _rb.velocity = dropDirection[random] * force;
    }

    protected override IEnumerator BlinkImage()
    {
        anim.speed = 0;
        sr.color = transparentColor;

        yield return new WaitForSeconds(.1f);
        sr.color = Color.white;

        yield return new WaitForSeconds(.1f);
        sr.color = transparentColor;

        yield return new WaitForSeconds(.1f);
        sr.color = Color.white;

        yield return new WaitForSeconds(.1f);
        sr.color = transparentColor;

        yield return new WaitForSeconds(.2f);
        sr.color = Color.white;

        yield return new WaitForSeconds(.2f);
        sr.color = transparentColor;

        yield return new WaitForSeconds(.2f);
        sr.color = Color.white;

        anim.speed = 1;
        canPickUp = true;
    }
}
