using UnityEngine;

public class AoeBehaviour : MonoBehaviour
{
    public AoeBoxAbility AoeAbility { set; get; }
    public GameObject Caster { set; get; }
    private float timeSinceLastTick, timeSinceCast;
    private bool resetClock;
    [SerializeField] private bool selfDamage;
    private const float fadeTime = 1f;
    private const float rotationSpeed = -0.075f;


    void Update()
    {
        if (GameManager.instance.Paused == false)
        {
            if (resetClock == true)
            {
                timeSinceLastTick = 0;
                resetClock = false;
            }

            if (AoeAbility.GetActiveDuration() - timeSinceCast <= fadeTime)
            {
                Color abilityColor = GetComponent<SpriteRenderer>().color;
                abilityColor = new Color(abilityColor.r, abilityColor.b, abilityColor.g, abilityColor.a -= Time.deltaTime * fadeTime);
                GetComponent<SpriteRenderer>().color = abilityColor;
            }

            if (timeSinceCast > AoeAbility.GetActiveDuration())
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
            if (timeSinceLastTick > AoeAbility.GetTimeBetweenTicks())
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
        if (other.tag == GameManager.objectsTags[GameManager.Objects.Enemy])
        {
            other.GetComponent<EnemyBaseClass>().TakeDamage(AoeAbility.Damage, Caster);
        }
        else if (other.tag == GameManager.objectsTags[GameManager.Objects.PlayerAngel] || other.tag == GameManager.objectsTags[GameManager.Objects.PlayerDemon])
        {
            PlayerManager.instance.TakeDamage(AoeAbility.Damage, other.gameObject, Caster);
        }
    }
}
