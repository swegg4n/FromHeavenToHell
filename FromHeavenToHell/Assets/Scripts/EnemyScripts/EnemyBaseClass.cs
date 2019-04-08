using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float speedFactor;

    void Start()
    {

    }

    void Update()
    {
        DeathCheck();
    }

    void DeathCheck()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
