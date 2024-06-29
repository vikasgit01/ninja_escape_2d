using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//This script is attached to Player

public class Player_Shoot : MonoBehaviour
{
    [SerializeField] private Text bulletCount;
    [SerializeField] private int totalbullets = 10;
    [SerializeField] private Image[] imageLife;
    public Transform bulletSpawn;
    public int lifes = 3;

    private float _cooldownTime = 1f;
    private float _attackCooldown;


    [SerializeField] private GameObject bulletPrefab;


    private void Start()
    {
        bulletCount.text = totalbullets.ToString();
    }

    void Update()
    {

        if (_attackCooldown > 0)
        {
            _attackCooldown -= Time.deltaTime;
        }

        if (lifes <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        Shoot();
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && _attackCooldown <= 0 && Game_Manager.instance.canShoot)
        {
            if (totalbullets > 0)
            {
                FindObjectOfType<Audio_Manager>().Play("PlayerShoot");
                totalbullets--;
                bulletCount.text = totalbullets.ToString();
                Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                _attackCooldown = _cooldownTime;
            }
        }
    }

    public void DecrementLife()
    {
        if (lifes > 0)
        {
            FindObjectOfType<Audio_Manager>().Play("PlayerDamage");
            lifes--;
            Color cl = imageLife[lifes].color;
            cl.a = 0.5f;
            imageLife[lifes].color = cl;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.fireTag)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
