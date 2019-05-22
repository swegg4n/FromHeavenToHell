using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Classes;

public class EnemyBaseClass : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] private float speedFactor;
    public float SpeedFactor { get { return speedFactor; } }

    public Ability Ability { get; protected set; }

    private GameObject lastHitCastedBy;

    [SerializeField] private List<Ability> availableAbilities;


    void Start()
    {
        GetComponent<Rigidbody2D>().mass = 0.1f;
        GetComponent<Rigidbody2D>().drag = 1000f;

        if (availableAbilities.Count == 0)
        {
            Debug.LogError("Enemies has no ability, assing at least 1 availavle abillity in 'EnemyBaseClass'");
        }
        Ability = availableAbilities[Random.Range(0, availableAbilities.Count)];
    }

    protected virtual void Update()
    {
        if (GameManager.instance.Paused == false)
        {
            DeathCheck();
        }
    }

    protected void DeathCheck()
    {
        if (health <= 0)
        {
            try
            {
                if (lastHitCastedBy.CompareTag(PlayerManager.instance.PlayerAngelInstance.tag) == true)
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
            catch { }
        }
    }

    public virtual void TakeDamage(int damage, GameObject player)
    {
        try
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
        catch { }
    }

    public int GetHealth()
    {
        return health;
    }
}
