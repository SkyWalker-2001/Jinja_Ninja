using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Controller>() != null)
        {
            Player_Controller player = collision.GetComponent<Player_Controller>();
            player.KnockBack(transform); 
        }

    }
}
