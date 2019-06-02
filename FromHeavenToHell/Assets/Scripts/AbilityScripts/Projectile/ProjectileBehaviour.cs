using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public ProjectileAbility ProjectileAbility { set; get; }    //Specialförmågan
    public GameObject Caster { set; get; }   //Objektet som använder specialförmågan
    private Vector2 startPosition;      //Positionen projektilen användes fårn
    private Vector2 velocity;    //projektilens hastighet


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

            //Om projektilen har färdats den maximala distans den kan färdas
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
        if (Caster == null || Caster.tag != other.tag)
        {
            HandleCollision(other);
        }
    }

    /// <summary>
    /// Hanterar kollision mellan projektil och objekt
    /// </summary>
    /// <param name="other"></param>
    private void HandleCollision(Collider2D other)
    {
        if (other.tag == GameManager.objectsTags[GameManager.Objects.Enemy])     //Om other är en fiende
        {
            other.GetComponent<EnemyBaseClass>().TakeDamage(ProjectileAbility.Damage, Caster);
            Destroy(gameObject);
        }
        else if (other.tag == GameManager.objectsTags[GameManager.Objects.PlayerAngel] || other.tag == GameManager.objectsTags[GameManager.Objects.PlayerDemon])     //Om other är ängeln eller demonen
        {
            PlayerManager.instance.TakeDamage(ProjectileAbility.Damage, other.gameObject, Caster);  
            Destroy(gameObject);
        }
        else if (other.tag == GameManager.objectsTags[GameManager.Objects.Wall])    //Om other är en vägg
        {
            Destroy(gameObject);
        }
    }
}
