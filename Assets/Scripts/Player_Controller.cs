using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rb;
    private Animator _player_AnimatorController;

    private float _movingInput;
    private bool canDoubleJump = true;

    private bool _facingRight = true;
    private int _facindDirection = 1;

    [Header("RayCast Ground")]

    [SerializeField] float _groundCheckDistance;
    [SerializeField] private bool _isGrounded;
    public LayerMask what_is_ground;



    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player_AnimatorController = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimationController();

        Collisiion_Check();

        Input_Checks();

        if (_isGrounded)
        {
            canDoubleJump = true;
        }

        Move();
       
        Flip_Controller();
    }

    private void AnimationController()
    {
        bool isMoving = _rb.velocity.x != 0;

        _player_AnimatorController.SetBool("isMoving", isMoving);
        _player_AnimatorController.SetBool("isGrounded", _isGrounded);
        _player_AnimatorController.SetFloat("yVelocity", _rb.velocity.y);
    }

    private void Input_Checks()
    {
        _movingInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump_Button();
        }
    }

    private void Flip_Controller()
    {
        if(_facingRight && _movingInput < 0)
        {
            Flip();
        }
        else if (!_facingRight && _movingInput > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _facindDirection = _facindDirection * -1;
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void Jump_Button()
    {
        if (_isGrounded)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canDoubleJump = false;
            Jump();
        }
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
