using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public ProjectileAbility ProjectileAbility { set; get;}
    public GameObject Caster { set; get; }
    private Vector2 startPosition;
    private Vector2 velocity;

    void Start()
    {
        velocity = GetComponent<Rigidbody2D>().velocity;
        startPosition = Caster.transform.position;
    }

    void Update()
    {
        if (GameManager.instance.Paused == false)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = velocity;

            if (Vector2.Distance(startPosition, transform.position) * GameManager.instance.TileSize > ProjectileAbility.GetRange())
            {
                Destroy(gameObject);
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
