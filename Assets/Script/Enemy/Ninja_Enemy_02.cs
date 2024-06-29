using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja_Enemy_02 : Enemy
{
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private bool right;


    private float _cooldownTime = 0.8f;
    private float _attackCooldown;


    private bool _attack;

    private GameObject _bullets;
    private RaycastHit2D _hit;

    private void Start()
    {
        _attackCooldown = _cooldownTime;
    }

    private void Update()
    {
        if (_attackCooldown > 0)
        {
            _attackCooldown -= Time.deltaTime;
        }

        if (!right)
        {
            _hit = Physics2D.Raycast(bulletSpawn.position, -Vector2.right, lookDistance);
            Debug.DrawRay(bulletSpawn.position, -Vector2.right * lookDistance);
        }
        else if (right)
        {
            _hit = Physics2D.Raycast(bulletSpawn.position, Vector2.right, lookDistance);
            Debug.DrawRay(bulletSpawn.position, Vector2.right * lookDistance);
        }



        if (_hit.collider != null)
        {
            if (_hit.transform.gameObject.CompareTag(Tags.playerTag))
            {
                _attack = true;
            }
        }
        else
        {
            _attack = false;
        }

        if (_attack == true && _attackCooldown < 0)
        {
            Attack();
            _attackCooldown = _cooldownTime;
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void Attack()
    {
        _bullets = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        if (!right)
        {

            _bullets.GetComponent<Rigidbody2D>().velocity = -Vector2.right * bulletSpeed * Time.deltaTime;
        }
        else if (right)
        {
            _bullets.GetComponent<Rigidbody2D>().velocity = Vector2.right * bulletSpeed * Time.deltaTime;

        }
    }



}
