using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to Bullet Prefab

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool left;
    [SerializeField] private bool right;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (Player_Movement.instance.isFacingRight)
        {
            left = true;
        }
        else
        {
            right = true;
        }

        Destroy(gameObject, 2f);
    }

    void Update()
    {
        if (left)
        {
            _rb.velocity = -Vector2.right * bulletSpeed * Time.deltaTime;
        }
        else if (right)
        {
            _rb.velocity = Vector2.right * bulletSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.groundTag)
        {
            GameObjectDistroy(this.gameObject);
        }

        if (collision.gameObject.tag == Tags.enemyTag)
        {
            GameObjectDistroy(this.gameObject);
            if (collision.GetComponent<Ninja_Enemy>() != null)
            {
                collision.GetComponent<Ninja_Enemy>().health -= 100;
            }

            if (collision.GetComponent<Ninja_Enemy_02>() != null)
            {
                collision.GetComponent<Ninja_Enemy_02>().health -= 100;
            }
            Game_Manager.instance.IncrementEnemyKilled();
        }

        if (collision.gameObject.tag == Tags.enemyBulletTag)
        {
            GameObjectDistroy(this.gameObject);
            GameObjectDistroy(collision.gameObject);
        }
    }

    private void GameObjectDistroy(GameObject go)
    {
        Destroy(go);
    }

}
