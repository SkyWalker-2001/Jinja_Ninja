using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] public bool pc_Testing;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    [Header("Particle")]
    [SerializeField] private ParticleSystem dustFX;

    private float dustFxTimer;

    private Rigidbody2D _rb;
    private Animator _player_AnimatorController;

    private bool canDoubleJump = true;
    private bool canMove;

    [Header("Move Info")]
    public Vector2 wallJumpDirection;
    public float doubleJumpForce;
    private bool canBeControlled;

    private float defaultJumpForce;
    private bool _facingRight = true;
    private int _facindDirection = 1;
    private float defaultGravityScale;

    private bool readyToLand;

    [Header("Collision Info")]
    [SerializeField] float _groundCheckDistance;
    [SerializeField] float _wallCheckDistance;
    [SerializeField] public LayerMask what_is_ground;
    [SerializeField] public LayerMask what_is_wall;
    [SerializeField] private Transform enemyCheck;
    [SerializeField] private float enemyCheckRadius;
    private bool _isGrounded;
    private bool _isWallDetected;
    private bool _canWallSlide;
    private bool _isWallSliding;

    [SerializeField] private float _bufferJumpTime;
    private float _bufferJumpCounter;

    [SerializeField] private float _cayoteJumpTimer;
    private float _cayoteJumpCounter;
    private bool _canHaveCayoteJump;

    [Header("KnockBacked")]
    [SerializeField] private Vector2 _knockBackDirection;
    [SerializeField] private float _knockbackTime;
    [SerializeField] private float _knockbackProtectionTime;
    private bool _isKnocked;
    [SerializeField] private bool canBeKnocked = true;

    [Header("Controls Touch")]

    private float _h_Input;
    private float _v_Input;
     public VariableJoystick variableJoystick;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player_AnimatorController = GetComponent<Animator>();

        SetPlayerAnimationLayer();

        defaultJumpForce = _jumpForce;

        defaultGravityScale = _rb.gravityScale;
        Debug.Log(defaultGravityScale);
        _rb.gravityScale = 0;
    }

    private void Update()
    {
        AnimationController();

        if (_isKnocked)
        {
            return;
        }

        // no further execution ///  Knockback function Player

        Collisiion_Check();

        Input_Checks();

        CheckForEnemy();

        _bufferJumpCounter -= Time.deltaTime;
        _cayoteJumpCounter -= Time.deltaTime;

        if (_isGrounded)
        {
            canDoubleJump = true;
            canMove = true;

            if (_bufferJumpCounter > 0)
            {
                _bufferJumpCounter = -1;
                Jump();
            }

            _canHaveCayoteJump = true;

            if (readyToLand)
            {
                dustFX.Play();
                readyToLand = false;
            }
        }
        else
        {

            if (!readyToLand)
            {
                readyToLand = true;
            }

            if (_canHaveCayoteJump)
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

    private void CheckForEnemy()
    {
        // Enemy

        Collider2D[] hitedColliders = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius);

        foreach (var enemy in hitedColliders)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                Enemy newEnemy = enemy.GetComponent<Enemy>();

                if (newEnemy.invincible)
                {
                    return;
                }

                if (_rb.velocity.y < 0)
                {
                    AudioManager.instance.PlaySFX(1);

                    newEnemy.Damage();
                    _player_AnimatorController.SetBool("flipping", true);
                    Jump();
                }
            }
        }
    }

    private void StopFlippingAnimation()
    {
        _player_AnimatorController.SetBool("flipping", false);
    }

    private void SetPlayerAnimationLayer()
    {
        int skinIndex = PlayerManager.instance.choosenSkinId;
        for (int i = 0; i < _player_AnimatorController.layerCount; i++)
        {
            _player_AnimatorController.SetLayerWeight(i, 0);
            // aada karan naal saria layer da weight 0 means koi ve select nai hunde 1 karna panda weight nuu
        }

        _player_AnimatorController.SetLayerWeight(skinIndex, 1);

    }

    private void AnimationController()
    {
        bool isMoving = _rb.velocity.x != 0;

        _player_AnimatorController.SetBool("isKnocked", _isKnocked);
        _player_AnimatorController.SetBool("isMoving", isMoving);
        _player_AnimatorController.SetBool("isGrounded", _isGrounded);
        _player_AnimatorController.SetBool("isWallSliding", _isWallSliding);
        _player_AnimatorController.SetBool("isWallDetected", _isWallDetected);
        _player_AnimatorController.SetBool("canBeControlled", canBeControlled);
        _player_AnimatorController.SetFloat("yVelocity", _rb.velocity.y);
    }

    private void Input_Checks()
    {
        if (!canBeControlled) { return; }

        if (pc_Testing)
        {
            _h_Input = Input.GetAxisRaw("Horizontal");
            _v_Input = Input.GetAxisRaw("Vertical");
        }
        else
        {
            _h_Input = variableJoystick.Horizontal;
            _v_Input = variableJoystick.Vertical;
        }

        if (_v_Input < 0)
        {
            _canWallSlide = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump_Button();
        }
    }

    public void ReturnControl()
    {
        _rb.gravityScale = defaultGravityScale;
        canBeControlled = true;
    }

    private void Flip_Controller()
    {
        dustFxTimer -= Time.deltaTime;

        if (_facingRight && _rb.velocity.x < 0)
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
        if (dustFxTimer < 0)
        {
            dustFX.Play();
            dustFxTimer = .7f;
        }

        _facindDirection = _facindDirection * -1;
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void Jump_Button()
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

        else if (_isGrounded || _cayoteJumpCounter > 0)
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

    public void KnockBack(Transform damageTransform)
    {
        AudioManager.instance.PlaySFX(9);

        if (!canBeKnocked)
        {
            return;
        }

        if (GameManager.instance.difficulty > 1)
        {
            PlayerManager.instance.OnTakeDamage();
        }

        PlayerManager.instance.ScreenShake(-_facindDirection);

        _isKnocked = true;
        canBeKnocked = false;

        #region KNOCKBACK DIRECTION

        int hDirection = 0;

        if (transform.position.x > damageTransform.position.x)
        {
            hDirection = 1;
        }
        else if (transform.position.x < damageTransform.position.x)
        {
            hDirection = -1;
        }
        #endregion

        _rb.velocity = new Vector2(_knockBackDirection.x * hDirection, _knockBackDirection.y);


        Invoke("CancelKnock", _knockbackTime);
        Invoke("AllowKnockBack", _knockbackProtectionTime);
    }

    private void CancelKnock()
    {
        _isKnocked = false;
    }

    private void AllowKnockBack()
    {
        canBeKnocked = true;
    }

    private void Move()
    {
        if (canMove)
            _rb.velocity = new Vector2(_h_Input * _moveSpeed, _rb.velocity.y);
    }

    private void Wall_Jump()
    {
        AudioManager.instance.PlaySFX(3);

        dustFX.Play();

        canMove = false;

        _rb.velocity = new Vector2(wallJumpDirection.x * -_facindDirection, wallJumpDirection.y);
    }

    private void Jump()
    {
        AudioManager.instance.PlaySFX(3);

        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);

        if (_isGrounded)
        {
            dustFX.Play();
        }
    }

    public void Push_UP(float pushForce)
    {
        _rb.velocity = new Vector2(_rb.velocity.x, pushForce);
    }

    private void Collisiion_Check()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, what_is_ground);
        _isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * _facindDirection, _wallCheckDistance, what_is_wall);

        if (_isWallDetected && _rb.velocity.y < 0)
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
        Gizmos.DrawWireSphere(enemyCheck.position, enemyCheckRadius);
    }
}
