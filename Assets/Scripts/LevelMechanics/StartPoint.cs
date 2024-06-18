using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Transform resPoint;

    private void Start()
    {
        PlayerManager.instance.respawnPoint = resPoint;
        PlayerManager.instance.PlayerRespawn();
        AudioManager.instance.PlayBGM_Random();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player_Controller>() != null)
        {
            if (!GameManager.instance.startTimer)
            {
                GameManager.instance.startTimer = true;
            }
            if (other.transform.position.x > transform.position.x)
            {
                GetComponent<Animator>().SetTrigger("touch");
            }
        }
    }
}
