using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool left;
    [SerializeField] private bool right;

    void Start()
    {  
        Destroy(gameObject, 2f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.groundTag)
        {
            GameObjectDistroy(this.gameObject);
        }

        if (collision.gameObject.tag == Tags.playerTag)
        {
            GameObjectDistroy(this.gameObject);
            collision.GetComponent<Player_Shoot>().DecrementLife();
        }
    }

    private void GameObjectDistroy(GameObject go)
    {
        Destroy(go);
    }
}
