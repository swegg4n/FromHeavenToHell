using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeBehaviour : MonoBehaviour
{
    public AoeBoxAbility aoeAbility { set; get; }
    public GameObject Caster { set; get; }
    private float timeSinceLastTick;
    [SerializeField] private bool selfDamage;

    void Start()
    {
        Destroy(gameObject, aoeAbility.GetActiveDuration());
    }

    void Update()
    {
        timeSinceLastTick += Time.deltaTime;
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("TriggerStay");
        if(timeSinceLastTick > aoeAbility.GetTimeBetweenTicks())
        {
            if (Caster.tag != other.tag || selfDamage == true || Caster == null)
            {
                Debug.Log("1");

                if (other.tag == "Enemy")
                {
                    Debug.Log("2");
                    other.GetComponent<EnemyBaseClass>().TakeDamage(aoeAbility.Damage);
                }
                else if (other.tag == "PlayerAngel" || other.tag == "PlayerDemon")
                {
                    Debug.Log("3");
                    PlayerManager.instance.TakeDamage(aoeAbility.Damage);
                }
            }
            timeSinceLastTick = 0;
        }
    }
}
