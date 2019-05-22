using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeBehaviour : MonoBehaviour
{
    public AoeBoxAbility aoeAbility { set; get; }
    public GameObject Caster { set; get; }
    private float timeSinceLastTick, timeSinceCast;
    private bool resetClock;
    [SerializeField] private bool selfDamage;

    void Start()
    {

    }

    void Update()
    {
        if (GameManager.instance.Paused == false)
        {
            if (resetClock == true)
            {
                timeSinceLastTick = 0;
                resetClock = false;
            }

            if (timeSinceCast > aoeAbility.GetActiveDuration())
            {
                Destroy(gameObject);
            }

            timeSinceLastTick += Time.deltaTime;
            timeSinceCast += Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (GameManager.instance.Paused == false)
        {
            if (timeSinceLastTick > aoeAbility.GetTimeBetweenTicks())
            {
                if (Caster == null)
                {
                    TakeDamage(other);
                }
                else if (Caster.tag != other.tag || selfDamage == true)
                {
                    TakeDamage(other);
                }
                resetClock = true;
            }
        }
    }

    private void TakeDamage(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyBaseClass>().TakeDamage(aoeAbility.Damage, Caster);
        }
        else if (other.tag == "PlayerAngel" || other.tag == "PlayerDemon")
        {
            PlayerManager.instance.TakeDamage(aoeAbility.Damage, other.gameObject, Caster);
        }
    }
}
