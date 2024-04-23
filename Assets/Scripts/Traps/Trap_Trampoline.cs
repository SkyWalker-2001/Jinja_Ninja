using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Trampoline : MonoBehaviour
{
    [SerializeField] private float pushForce = 20;
    [SerializeField] private bool canBeUsed = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player_Controller>() != null && canBeUsed)
        {
            canBeUsed = false;
            GetComponent<Animator>().SetTrigger("activated");
            other.GetComponent<Player_Controller>().Push_UP(pushForce);
        }
    }

    public void ToggleForTramp(){
        canBeUsed = true;
    }
}
