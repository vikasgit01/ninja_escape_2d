using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja_Enemy : Enemy
{
    [SerializeField] private float groundRadius;
    [SerializeField] private Transform edgeGroundCheck;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;

    [HideInInspector] public bool isMovingRight;

    private bool _isFacingRight;
    private bool _isEdgeGround;
    private bool _patrolling;
    private bool _attack;

    private float _cooldownTime = 0.8f;
    private float _attackCooldown;

    private GameObject _bullets;
    private Animator _animator;
    private Rigidbody2D _rb;
    private Vector2 _dir;

    int _runAnimationId;


    void Start()
    {

        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();

        //_attack = true;
        isMovingRight = true;
        _patrolling = true;
        _attackCooldown = _cooldownTime;
        _runAnimationId = Animator.StringToHash("Run");
        _animator.SetBool(_runAnimationId, true);

    }

    void Update()
    {
        if (isMovingRight)
        {
            _dir = Vector2.right;
        }
        else
        {
            _dir = -Vector2.right;
        }

        RaycastHit2D hit = Physics2D.Raycast(bulletSpawn.position, _dir, lookDistance);
        Debug.DrawRay(bulletSpawn.position, _dir * lookDistance);

        if (hit.collider != null)
        {
            if (hit.transform.gameObject.CompareTag(Tags.playerTag))
            {
                eState = enemyState.attack;
            }
        }
        else
        {
            eState = enemyState.patrolling;
        }


        if (_attackCooldown > 0)
        {
            _attackCooldown -= Time.deltaTime;
        }

        if (health <= 0)
        {
            eState = enemyState.dead;
        }

        EnemyState();
        Flip();

        if (_patrolling == true)
        {
            Patrolling();
        }

        if (_attack == true && _attackCooldown < 0)
        {
            Attack();
            _attackCooldown = _cooldownTime;
        }
    }

    void EnemyState()
    {
        switch (eState)
        {
            case enemyState.patrolling:
                _animator.SetBool(_runAnimationId, true);
                _patrolling = true;
                _attack = false;
                break;
            case enemyState.attack:
                _animator.SetBool(_runAnimationId, false);
                _patrolling = false;
                _attack = true;
                break;
            case enemyState.dead:
                Dead();
                break;
        }
    }

    void Patrolling()
    {
        _isEdgeGround = Physics2D.OverlapCircle(edgeGroundCheck.position, groundRadius, groundLayerMask);

        if (!_isEdgeGround)
        {
            isMovingRight = !isMovingRight;
        }

        if (isMovingRight == false)
        {
            _rb.velocity = new Vector2(-speed, _rb.velocity.y);
        }

        if (isMovingRight == true)
        {
            _rb.velocity = new Vector2(speed, _rb.velocity.y);
        }

    }

    void Attack()
    {
        _rb.velocity = Vector2.zero;
        _bullets = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        if (isMovingRight)
        {
            _bullets.GetComponent<Rigidbody2D>().velocity = Vector2.right * bulletSpeed * Time.deltaTime;
        }
        else
        {
            _bullets.GetComponent<Rigidbody2D>().velocity = -Vector2.right * bulletSpeed * Time.deltaTime;
        }
    }

    void Flip()
    {
        if (isMovingRight && _isFacingRight || !isMovingRight && !_isFacingRight)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 scaleX = transform.localScale;
            scaleX.x *= -1;
            transform.localScale = scaleX;
        }
    }

    void Dead()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(edgeGroundCheck.position, groundRadius);
    }

}
