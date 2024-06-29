using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script is attached to player

public class Player_Movement : MonoBehaviour
{

    public static Player_Movement instance;

    [SerializeField] private float jumpSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask groungLayerMask;
    [SerializeField] private Transform groundPoint;
    
    [HideInInspector] public bool isFacingRight;

    private Rigidbody2D _rb;
    private Animator _animator;
    private bool _isGrounded;
    private bool _canDoubleJump;

    private int _runAnimationId;
    private int _groundedAnimationId;
    private int _jumpAnimationId;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _runAnimationId = Animator.StringToHash("Run");
        _groundedAnimationId = Animator.StringToHash("Grounded");
        _jumpAnimationId = Animator.StringToHash("Jump");
    }

    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groungLayerMask);

        if (_isGrounded)
        {
            _canDoubleJump = true;
            _animator.SetBool(_groundedAnimationId, _isGrounded);
        }
        else
        {
            _animator.SetTrigger(_jumpAnimationId);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isGrounded)
            {
                Jump();
            }
            else if (_canDoubleJump)
            {
                Jump();
                _canDoubleJump = false;
            }
        }
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float axisX = Input.GetAxis("Horizontal") * speed;
        _rb.velocity = new Vector2(axisX, _rb.velocity.y);
        _animator.SetBool(_runAnimationId, axisX != 0);
        Flip(axisX);
    }

    void Jump()
    {
        FindObjectOfType<Audio_Manager>().Play("Jump");
        _animator.SetTrigger(_jumpAnimationId);
        _rb.velocity = new Vector2(_rb.velocity.x, jumpSpeed);
        _isGrounded = false;
        _animator.SetBool(_groundedAnimationId, _isGrounded);
    }

    void Flip(float axisX)
    {

        if (axisX > 0f && isFacingRight || axisX < -0f && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 scaleX = transform.localScale;
            scaleX.x *= -1;
            transform.localScale = scaleX;
        }

        /* if(axisX > 0f && isFacingRight)
         {
             isFacingRight = !isFacingRight;
             Quaternion rotateX = transform.localRotation;
             rotateX.y = 0;
             transform.localRotation = rotateX;
         }
         if(axisX < 0f && !isFacingRight)
         {
             isFacingRight = !isFacingRight;
             Quaternion rotateX = transform.localRotation;
             rotateX.y = 180;
             transform.localRotation = rotateX;
         }*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);
    }

}
