using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public ProjectileAbility ProjectileAbility { set; get;}
    public GameObject Caster { set; get; }
    private Vector2 startPosition;

    void Start()
    {
        startPosition = Caster.transform.position;
    }

    void Update()
    {
        if (Vector2.Distance(startPosition, transform.position) * GameManager.instance.TileSize > ProjectileAbility.GetRange())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Caster == null)
        {
            HandleCollision(other);
        }
        else if(Caster.tag != other.tag)
        {
            HandleCollision(other);
        }
    }

    private void HandleCollision(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyBaseClass>().TakeDamage(ProjectileAbility.Damage, Caster);
            Destroy(gameObject);
        }
        else if (other.tag == "PlayerAngel" || other.tag == "PlayerDemon")
        {
            PlayerManager.instance.TakeDamage(ProjectileAbility.Damage, other.gameObject, Caster);
            Destroy(gameObject);
        }
        else if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
