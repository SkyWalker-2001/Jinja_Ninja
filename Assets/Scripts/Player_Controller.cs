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
    private bool canMove;

    public Vector2 wallJumpDirection;
    public float doubleJumpForce;

    private float defaultJumpForce;
     
    private bool _facingRight = true;
    private int _facindDirection = 1;

    [Header("Collision Info")]
    [SerializeField] float _groundCheckDistance;
    [SerializeField] float _wallCheckDistance;
    public LayerMask what_is_ground;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isWallDetected;
    [SerializeField] private bool _canWallSlide;
    [SerializeField] private bool _isWallSliding;

    [SerializeField] private float _bufferJumpTime;
                     private float _bufferJumpCounter;

    [SerializeField] private float _cayoteJumpTimer;
                     private float _cayoteJumpCounter;
                     private bool _canHaveCayoteJump;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player_AnimatorController = GetComponent<Animator>();

        defaultJumpForce = _jumpForce;
    }

    private void Update()
    {
        AnimationController();

        Collisiion_Check();

        Input_Checks();

        _bufferJumpCounter -= Time.deltaTime;
        _cayoteJumpCounter -= Time.deltaTime;

        if (_isGrounded)
        {
            canDoubleJump = true;
            canMove = true;

            if(_bufferJumpCounter > 0)
            {
                _bufferJumpCounter = -1;
                Jump();
            }

            _canHaveCayoteJump = true;
        }
        else
        {
            if(_canHaveCayoteJump)
            {
                _canHaveCayoteJump = false;
                _cayoteJumpCounter = _cayoteJumpTimer;
            }
        }

        if (_canWallSlide)
        {
            _isWallSliding = true;
            
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.1f);
        }

        Move();

        Flip_Controller();
    }

    private void AnimationController()
    {
        bool isMoving = _rb.velocity.x != 0;

        _player_AnimatorController.SetBool("isMoving", isMoving);
        _player_AnimatorController.SetBool("isGrounded", _isGrounded);
        _player_AnimatorController.SetBool("isWallSliding", _isWallSliding);
        _player_AnimatorController.SetBool("isWallDetected", _isWallDetected);
        _player_AnimatorController.SetFloat("yVelocity", _rb.velocity.y);
    }

    private void Input_Checks()
    {
        _movingInput = Input.GetAxis("Horizontal");

        if (Input.GetAxis("Vertical") < 0)
        {
            _canWallSlide = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump_Button();
        }
    }

    private void Flip_Controller()
    {
        if(_facingRight && _rb.velocity.x < 0)
        {
            Flip();
        }
        else if (!_facingRight && _rb.velocity.x > 0)
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
        if (!_isGrounded)
        {
            _bufferJumpCounter = _bufferJumpTime;
        }

        if (_isWallSliding)
        {
            Wall_Jump();
            canDoubleJump = true;
        }

        else if (_isGrounded || _cayoteJumpCounter > 0 )
        {
            Jump();
        }

        else if (canDoubleJump)
        {
            canMove = true;
            canDoubleJump = false;
            _jumpForce = doubleJumpForce;
            Jump();
            _jumpForce = defaultJumpForce;
        }

        _canWallSlide = false;
    }
        
    private void Move()
    {
        if(canMove)
            _rb.velocity = new Vector2(_movingInput * _moveSpeed, _rb.velocity.y);
    }

    private void Wall_Jump()
    {
        canMove = false;

        _rb.velocity = new Vector2(wallJumpDirection.x * -_facindDirection, wallJumpDirection.y);
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }
    private void Collisiion_Check()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, what_is_ground);
        _isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * _facindDirection, _wallCheckDistance, what_is_ground);

        if(_isWallDetected && _rb.velocity.y < 0)
        {
            _canWallSlide = true;
        }

        if (!_isWallDetected)
        {
            _canWallSlide = false;
            _isWallSliding = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - _groundCheckDistance, transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + _wallCheckDistance * _facindDirection, transform.position.y));
    }
}
