using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] private float speedFactor;
    public float SpeedFactor { get { return speedFactor; } }

    [SerializeField] private Ability ability;
    public Ability Ability { get; protected set; }


    void Start()
    {
        GetComponent<Rigidbody2D>().mass = 0.1f;
        GetComponent<Rigidbody2D>().drag = 1000f;
        Ability = ability;
    }

    protected virtual void Update()
    {
        DeathCheck();
    }

    protected void DeathCheck()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.GetComponent<ObjectiveController>().AddKill();
        }
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
    }
}
