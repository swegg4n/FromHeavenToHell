using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public ProjectileAbility projectileAbility { set; get;}
    private GameObject caster;
    private Vector2 startPosition;

    void Start()
    {
        caster = projectileAbility.caster;
        startPosition = caster.transform.position;
    }

    void Update()
    {
        if (Vector2.Distance(startPosition, transform.position) * GameManager.instance.tileSize > projectileAbility.GetRange())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (caster.tag != other.tag || caster == null)
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<EnemyBaseClass>().TakeDamage(projectileAbility.Damage);
                Destroy(gameObject);
            }
            else if (other.tag == "PlayerAngel" || other.tag == "PlayerDemon")
            {
                PlayerManager.instance.TakeDamage(projectileAbility.Damage);
                Destroy(gameObject);
            }
            else if (other.tag == "Wall")
            {
                Destroy(gameObject);
            }
        }
    }
}
