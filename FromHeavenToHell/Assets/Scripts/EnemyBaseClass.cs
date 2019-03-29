﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float speedFactor;

    public float SpeedFactor { get { return speedFactor; } }

    void Start()
    {
        health = 100;
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
        health = health - damage;
    }
}
