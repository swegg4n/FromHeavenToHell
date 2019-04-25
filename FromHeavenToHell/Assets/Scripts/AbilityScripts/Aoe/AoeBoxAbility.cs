using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AOE Box Ability")]
public class AoeBoxAbility : Ability
{
    [SerializeField] private float cooldown;    ////Tiden användaren måste vänta innan abilityn kan användas. Mäts i sekunder
    [SerializeField] private float activeDuration;  //Tiden abilityn existerar innan den förstörs. Mäts i sekunder
    [SerializeField] private float timeBetweenTicks;
    [SerializeField] private int range;

    public GameObject caster { get; private set; }

    private float timeSinceLastTick;


    /// <summary>
    /// Det som händer när abilityn används
    /// </summary>
    public override void TriggerAbility(GameObject caster)
    {
        this.caster = caster;

        CooldownController cdController = caster.GetComponent<CooldownController>();

        if (cdController.CooldownPassed() == true)
        {
            Vector2 direction;

            if (caster.tag == "PlayerDemon" || caster.tag == "PlayerAngel")
            {
                direction = caster.GetComponent<AimIndicator>().direction;
            }
            else
            {
                direction = caster.GetComponent<BaseEnemyAi>().GetClosestTargetPosition() - caster.transform.position;
            }

            //Konverterar vector2 till vinkel (skriven i radianer)
            float radAngle = Mathf.Atan2(direction.y, direction.x);
            //Konverterar vinkel skriven i radianer till vinkel skriven i grader
            float degAngle = radAngle / (2 * Mathf.PI) * 360;

            Vector2 targetPosition = (Vector2)caster.transform.position + direction.normalized * range / GameManager.instance.tileSize;

            var aoeBox = Instantiate(abilityPrefab, targetPosition, Quaternion.Euler(0, 0, degAngle));
            aoeBox.GetComponent<AoeBehaviour>().aoeAbility = this;

            aoeBox.GetComponent<AoeBehaviour>().Caster = caster;

            cdController.ResetCooldown(cooldown);
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