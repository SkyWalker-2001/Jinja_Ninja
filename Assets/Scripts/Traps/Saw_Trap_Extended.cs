using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw_Trap_Extended : Traps
{
    [SerializeField] private Transform[] _movePoints;
    [SerializeField] private float _moveSpeed;
    
    private int _movePointIndex;
    private bool goingForward = true;

    private Animator saw_Animator;

    private void Start()
    {
        transform.position = _movePoints[0].position;
        saw_Animator = GetComponent<Animator>();
        saw_Animator.SetBool("isWorking", true);
        Flip();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _movePoints[_movePointIndex].position, _moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _movePoints[_movePointIndex].position) < 0.15f)
        {
            if (_movePointIndex == 0)
            {
                Flip();
                goingForward = true;
            }

            if(goingForward)
                _movePointIndex++;
            else
                _movePointIndex--;

            if (_movePointIndex >= _movePoints.Length)
            {
                _movePointIndex = _movePoints.Length - 1;
                goingForward = false;
                Flip();
            }
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(1, transform.localScale.y * -1, 1);
    }
}