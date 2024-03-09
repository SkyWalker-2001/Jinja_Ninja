using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw_Trap : Traps
{
    [SerializeField]public bool isWorking;

    private Animator saw_Animator;

    private void Start()
    {
        saw_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        saw_Animator.SetBool("isWorking", isWorking);
    }
}
