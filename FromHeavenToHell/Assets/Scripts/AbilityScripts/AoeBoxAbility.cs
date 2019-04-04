using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AOE Box Ability")]
public class AoeBoxAbility : Ability
{
    [SerializeField] private float cooldown;
    [SerializeField] private float activeDuration;



    public override void TriggerAbility(GameObject caster)
    {
        CooldownController cdController = caster.GetComponent<CooldownController>();


        if (cdController.CooldownPassed() == true)
        {
            float radAngle = Mathf.Atan2(caster.GetComponent<AimIndicator>().direction.y, caster.GetComponent<AimIndicator>().direction.x);//Konverterar vector2 till vinkel
            float degAngle = radAngle / (2 * Mathf.PI) * 360;

            var aoeBox = Instantiate(abilityPrefab, caster.transform.position, Quaternion.Euler(0, 0, degAngle));
            Destroy(aoeBox, activeDuration);

            cdController.ResetCooldown(cooldown);  
        }
    }

}