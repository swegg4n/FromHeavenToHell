using Assets.Classes;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{
    [SerializeField] protected int health;   //Fiendens liv
    [SerializeField] private float speedFactor;     //Fiendens fart
    public float SpeedFactor { get { return speedFactor; } }

    public Ability Ability { get; protected set; }      //Fiendens specialförmåga

    private GameObject lastHitCastedBy;     //Objektet som skadar fienden

    [SerializeField] private List<Ability> availableAbilities;      //Förmågor som fienden kan använda


    void Start()
    {
        if (availableAbilities.Count == 0)
        {
            Debug.LogWarning("Enemies has no ability, assing at least 1 availavle abillity in 'EnemyBaseClass'");
            return;
        }

        Ability = availableAbilities[Random.Range(0, availableAbilities.Count)];
    }

    /// <summary>
    /// Tar bort fienden om dess liv når 0
    /// </summary>
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

    /// <summary>
    /// Skadar fienden 
    /// </summary>
    /// <param name="damage">Hur mycket skada som ska göras</param>
    /// <param name="player">Spelaren som, gjorde skadan</param>
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

        DeathCheck();
    }

    protected virtual void Update() { }

    public int GetHealth()
    {
        return health;
    }
}
