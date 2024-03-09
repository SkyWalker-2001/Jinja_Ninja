using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap_Switch : MonoBehaviour
{
    public Fire_Trap myTrap;

    private Animator fireSwitch_AnimController;

    private void Start()
    {
        fireSwitch_AnimController = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player_Controller>() != null)
        {
            fireSwitch_AnimController.SetTrigger("Pressed");
            myTrap.FireSwitchAfter(5);
        }
    }
}
