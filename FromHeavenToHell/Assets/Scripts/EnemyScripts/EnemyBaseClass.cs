using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float speedFactor;
    public float SpeedFactor { get { return speedFactor; } }

    [SerializeField] private Ability ability;
    public Ability Ability { get { return ability; } }

    
    void Start()
    {
        health = 100;

        GetComponent<Rigidbody2D>().mass = 0.1f;
        GetComponent<Rigidbody2D>().drag = 1000f;
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
