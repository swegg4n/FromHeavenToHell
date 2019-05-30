using UnityEngine;

public class AoeBehaviour : MonoBehaviour
{
    public AoeBoxAbility AoeAbility { set; get; }       //Specialförmågan
    public GameObject Caster { set; get; }      //Objektet som använder specialförmågan
    private float timeSinceLastTick, timeSinceCast;     //Timer för tick-damage     //Timer för hela specialförmågan
    private bool resetClock;    //Om timern ska återställas eller inte
    [SerializeField] private bool selfDamage;   //Om specialförmågan ska skada den som använder den eller inte
    private const float fadeTime = 1f;  //Tiden det tar för specialförmågan att fadea-bort


    void Update()
    {
        if (GameManager.instance.Paused == false)
        {
            if (resetClock == true)
            {
                timeSinceLastTick = 0;
                resetClock = false;
            }

            //Om tiden specialförmågan ska vara aktiv är mindre eller lika med tiden det ska ta för den att försvinna
            if (AoeAbility.GetActiveDuration() - timeSinceCast <= fadeTime)
            {
                Color abilityColor = GetComponent<SpriteRenderer>().color;
                abilityColor = new Color(abilityColor.r, abilityColor.b, abilityColor.g, abilityColor.a -= Time.deltaTime * fadeTime);      //Sänker specialförmågnas alfa-värde gradvis
                GetComponent<SpriteRenderer>().color = abilityColor;
            }

            //Om tiden specialförmågan ska vara aktiv har passerat
            if (timeSinceCast > AoeAbility.GetActiveDuration())
            {
                Destroy(gameObject);
            }

            timeSinceLastTick += Time.deltaTime;
            timeSinceCast += Time.deltaTime;
        }
    }

    /// <summary>
    /// Hanterar trigger mellan specialförmågan och spelare/fiender
    /// </summary>
    private void OnTriggerStay2D(Collider2D other)
    {
        if (GameManager.instance.Paused == false)
        {
            if (timeSinceLastTick > AoeAbility.GetTimeBetweenTicks())
            {
                if (Caster == null || Caster.tag != other.tag || selfDamage == true)
                {
                    TakeDamage(other);
                }
                resetClock = true;
            }
        }
    }

    /// <summary>
    /// Kallar på att skada objekt baserat på vilken typ objekten är (PlayerAngel/PlayerDemon/Enemy)
    /// </summary>
    /// <param name="other">Objektet som ska ta skada</param>
    private void TakeDamage(Collider2D other)
    {
        if (other.tag == GameManager.objectsTags[GameManager.Objects.Enemy])    //Om other är en fiende
        {
            other.GetComponent<EnemyBaseClass>().TakeDamage(AoeAbility.Damage, Caster);
        }
        else if (other.tag == GameManager.objectsTags[GameManager.Objects.PlayerAngel] || other.tag == GameManager.objectsTags[GameManager.Objects.PlayerDemon])    //Om other är ängeln eller demonen
        {
            PlayerManager.instance.TakeDamage(AoeAbility.Damage, other.gameObject, Caster);
        }
    }
}
