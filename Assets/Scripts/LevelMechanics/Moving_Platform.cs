using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
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

        if (isWorking)
            transform.position = Vector3.MoveTowards(transform.position, _movePoints[_movePointIndex].position, _moveSpeed * Time.deltaTime);

        // to check the distance between object and point  // no 0 // yes 0.15f

        if (Vector2.Distance(transform.position, _movePoints[_movePointIndex].position) < 0.15f)
        {
            _coolDownTimer = _coolDown;

            _movePointIndex++;

            if (_movePointIndex >= _movePoints.Length)
            {
                _movePointIndex = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player_Controller>() != null)
        {
            other.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player_Controller>() != null)
        {
            other.transform.SetParent(null);
        }
    }

    
}
