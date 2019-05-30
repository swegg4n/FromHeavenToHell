using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AOE Box Ability")]
public class AoeBoxAbility : Ability
{
    [SerializeField] private float cooldown;    //Tiden användaren måste vänta innan abilityn kan användas.
    [SerializeField] private float activeDuration;  //Tiden abilityn existerar innan den förstörs.
    [SerializeField] private float timeBetweenTicks;    //Tiden det ska ta mellan varje gång specialförmågan gör skada
    [SerializeField] private int range;     //Hur långt ifrån castern specialförmågnan ska användas

    private float timeSinceLastTick;    //Timer för tick-skada


    /// <summary>
    /// Det som händer när abilityn används
    /// </summary>
    public override void TriggerAbility(GameObject caster)
    {
        CooldownController cdController = caster.GetComponent<CooldownController>();    //Script som räknar ut hur länge objekt måste vänta innan de kan använda förmågor

        if (cdController.AoeCooldownPassed() == true)   //Om objektet kan använda förmågan
        {
            Vector2 direction;

            //Om objektet som använder förmågan är demonen eller ängeln
            if (caster.tag == GameManager.objectsTags[GameManager.Objects.PlayerDemon] || caster.tag == GameManager.objectsTags[GameManager.Objects.PlayerAngel])
            {
                direction = caster.GetComponent<AimIndicator>().Direction;
            }
            else
            {
                direction = caster.GetComponent<BaseEnemyAi>().GetClosestTargetPosition() - caster.transform.position;
            }

            
            float radAngle = Mathf.Atan2(direction.y, direction.x);     //Konverterar vector2 till vinkel (skriven i radianer)
            float degAngle = radAngle / (2 * Mathf.PI) * 360;       //Konverterar vinkel skriven i radianer till vinkel skriven i grader

            Vector2 targetPosition = (Vector2)caster.transform.position + direction.normalized * range / GameManager.instance.TileSize;

            var aoeBox = Instantiate(abilityPrefab, targetPosition, Quaternion.Euler(0, 0, degAngle));
            aoeBox.GetComponent<AoeBehaviour>().AoeAbility = this;

            aoeBox.GetComponent<AoeBehaviour>().Caster = caster;

            cdController.ResetAoeCooldown(cooldown);
        }
    }

    public float GetTimeBetweenTicks()
    {
        return timeBetweenTicks;
    }

    public float GetActiveDuration()
    {
        return activeDuration;
    }
}