using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected enum enemyState
    {
        patrolling,
        attack,
        dead
    }

    protected enemyState eState;

    [SerializeField] protected int bulletSpeed;
    [SerializeField] protected float speed;
    [SerializeField] protected float lookDistance;

    public int health = 100;
}
