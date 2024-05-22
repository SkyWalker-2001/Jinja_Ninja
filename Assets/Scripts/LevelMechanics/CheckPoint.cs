using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player_Controller>() != null)
        {
            GetComponent<Animator>().SetTrigger("activate");
            PlayerManager.instance.respawnPoint = transform;
        }
    }
}
