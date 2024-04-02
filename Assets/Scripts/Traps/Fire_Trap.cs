using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Trap : Danger
{
    public bool isWorking;
    //public bool hasSwitcher;
    public float repeatRate;

    private Animator fireTrap_animator;

    private void Start()
    {
        fireTrap_animator = GetComponent<Animator>();

        if(transform.parent == null)
            InvokeRepeating("FireSwitch", 0, repeatRate);
        //{
            //hasSwitcher = true;
        //}

        //if(!hasSwitcher)
    }

    private void Update()
    {
        fireTrap_animator.SetBool("isWorking", isWorking);
    }

    public void FireSwitch()
    {
        isWorking = !isWorking;
    }

    public void FireSwitchAfter(float second)
    {
        CancelInvoke();
        isWorking = false;
        Invoke("FireSwitch", second);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (isWorking)
            base.OnTriggerEnter2D(collision);
        else
            Debug.Log("Thanda pagya");
    }
}
