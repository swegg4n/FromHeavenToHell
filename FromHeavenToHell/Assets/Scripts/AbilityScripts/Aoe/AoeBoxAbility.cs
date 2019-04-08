using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AOE Box Ability")]
public class AoeBoxAbility : Ability
{
    [SerializeField] private float cooldown;
    [SerializeField] private float activeDuration;
    [SerializeField] private float timeBetweenTicks;
    [SerializeField] private int range;

    public GameObject caster { get; private set; }

    private float timeSinceLastTick;

    public override void TriggerAbility(GameObject caster)
    {
        this.caster = caster;

        CooldownController cdController = caster.GetComponent<CooldownController>();

        if (cdController.CooldownPassed() == true)
        {
            float radAngle = Mathf.Atan2(caster.GetComponent<AimIndicator>().direction.y, caster.GetComponent<AimIndicator>().direction.x);//Konverterar vector2 till vinkel
            float degAngle = radAngle / (2 * Mathf.PI) * 360;

            Vector2 targetPosition = (Vector2)caster.transform.position + caster.GetComponent<AimIndicator>().direction.normalized * range / GameManager.instance.tileSize;

            var aoeBox = Instantiate(abilityPrefab, targetPosition, Quaternion.Euler(0, 0, degAngle));

            aoeBox.GetComponent<AoeBehaviour>().aoeAbility = this;

            cdController.ResetCooldown(cooldown);
        }
    }

    public override void Update()
    {

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