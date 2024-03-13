using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Damage()
    {
        Debug.Log("asd");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player_Controller>() != null)
        {
            Player_Controller player = collision.collider.GetComponent<Player_Controller>();

            player.KnockBack(transform);
        }
    }
}
