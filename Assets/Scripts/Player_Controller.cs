using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rb;

    private float _movingInput;

    [Header("RayCast Ground")]

    [SerializeField] float _groundCheckDistance;
    [SerializeField] private bool _isGrounded;
    public LayerMask what_is_ground;



    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Collisiion_Check();

        _movingInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isGrounded)
            {
                Jump();
            }
        }

        Move();
    }

    private void Move()
    {
        _rb.velocity = new Vector2(_movingInput * _moveSpeed, _rb.velocity.y);
    }


    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }
    private void Collisiion_Check()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, what_is_ground);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - _groundCheckDistance, transform.position.y));
    }
}
