using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private InGame_UI inGame_UI;

    private void Start()
    {
        inGame_UI = GameObject.Find("Canvas").GetComponent<InGame_UI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player_Controller>() != null)
        {
            GetComponent<Animator>().SetTrigger("activate");
            
            Destroy(other.gameObject);

            inGame_UI.On_LevelFinish();

            GameManager.instance.SaveBestTimer();
            GameManager.instance.saveCollectedFruits();
            GameManager.instance.SaveLevelInfo();
        }
    }
}
