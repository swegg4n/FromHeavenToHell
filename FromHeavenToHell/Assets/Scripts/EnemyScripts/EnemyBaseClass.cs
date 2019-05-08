using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Classes;

public class EnemyBaseClass : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] private float speedFactor;
    public float SpeedFactor { get { return speedFactor; } }

    [SerializeField] private Ability ability;
    public Ability Ability { get; protected set; }

    private GameObject lastHitCastedBy;

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
            if(lastHitCastedBy.CompareTag(PlayerManager.instance.PlayerAngelInstance.tag) == true)
            {
                StatTracker.DemonEnemiesKilled++;
            }
            else if (lastHitCastedBy.CompareTag(PlayerManager.instance.PlayerDemonInstance.tag) == true)
            {
                StatTracker.AngelEnemiesKilled++;
            }

            Destroy(gameObject);
            GameManager.instance.GetComponent<ObjectiveController>().AddKill();
        }
    }

    public virtual void TakeDamage(int damage, GameObject player)
    {
        health -= damage;

        lastHitCastedBy = player;

        if (PlayerManager.instance.PlayerAngelInstance.CompareTag(player.tag) == true)
        {
            StatTracker.AngelDamageDealtToEnemies += damage;
        }
        else if (PlayerManager.instance.PlayerDemonInstance.CompareTag(player.tag) == true)
        {
            StatTracker.DemonDamageDealtToEnemies += damage;
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
