using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player_Controller>() != null)
        {
            GetComponent<Animator>().SetTrigger("activate");
            Debug.Log("Winner");

            GameManager.instance.SaveBestTimer();
            GameManager.instance.saveCollectedFruits();
            GameManager.instance.SaveLevelInfo();
        }
    }
}
