using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Saw_Trap : Danger
{
    [SerializeField] private Transform[] _movePoints;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _coolDown;

    private float _coolDownTimer;
    private int _movePointIndex;

    private Animator saw_Animator;

    private void Start()
    {
        transform.position = _movePoints[0].position;
        saw_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _coolDownTimer -= Time.time;

        bool isWorking = _coolDownTimer < 0;

        saw_Animator.SetBool("isWorking", isWorking);

        if(isWorking)
            transform.position = Vector3.MoveTowards(transform.position, _movePoints[_movePointIndex].position, _moveSpeed * Time.deltaTime);

        // to check the distance between object and point  // no 0 // yes 0.15f

        if (Vector2.Distance(transform.position, _movePoints[_movePointIndex].position) < 0.15f)
        {
            Flip();

            _coolDownTimer = _coolDown;

            _movePointIndex++;
            
            if(_movePointIndex >= _movePoints.Length)
            {
                _movePointIndex = 0;
            }
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(1, transform.localScale.y * -1, 1);
    }
}
