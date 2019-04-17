using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeBehaviour : MonoBehaviour
{
    public AoeBoxAbility aoeAbility { set; get; }
    public GameObject Caster { set; get; }
    private float timeSinceLastTick;
    private bool resetClock;
    [SerializeField] private bool selfDamage;

    void Start()
    {
        Destroy(gameObject, aoeAbility.GetActiveDuration());
    }

    void Update()
    {
        if(resetClock == true)
        {
            timeSinceLastTick = 0;
            resetClock = false;
        }

        timeSinceLastTick += Time.deltaTime;
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if(timeSinceLastTick > aoeAbility.GetTimeBetweenTicks())
        {
            if (Caster.tag != other.tag || selfDamage == true || Caster == null)
            {
                if (other.tag == "Enemy")
                {
                    Debug.Log("Hit" + timeSinceLastTick);
                    other.GetComponent<EnemyBaseClass>().TakeDamage(aoeAbility.Damage);
                }
                else if (other.tag == "PlayerAngel" || other.tag == "PlayerDemon")
                {
                    PlayerManager.instance.TakeDamage(aoeAbility.Damage);
                }
            }
            resetClock = true;
        }
    }
}
