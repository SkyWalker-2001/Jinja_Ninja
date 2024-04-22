using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Transform resPoint; 
    private void Awake()
    {
        PlayerManager.instance.respawnPoint = resPoint;
        PlayerManager.instance.PlayerRespawn();
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player_Controller>() != null)
        {
            if (other.transform.position.x > transform.position.x)
            {
                GetComponent<Animator>().SetTrigger("touch");
            }
        }
    }
}
