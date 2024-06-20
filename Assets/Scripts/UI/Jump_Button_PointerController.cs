using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Jump_Button_PointerController : MonoBehaviour, IPointerDownHandler
{
    private Player_Controller player_Controller;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (PlayerManager.instance.currentPlayer != null)
        {
            player_Controller = PlayerManager.instance.currentPlayer.GetComponent<Player_Controller>();
            player_Controller.Jump_Button();
        }
    }
}
