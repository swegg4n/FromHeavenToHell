using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Projectile Ability")]
public class ProjectileAbility : Ability
{
    [SerializeField] private float speed;
    [SerializeField] private int range;
    [SerializeField] private int knockbackForce;
    [SerializeField] private float cooldown;


    public override void TriggerAbility(GameObject caster)
    {
        CooldownController cdController = caster.GetComponent<CooldownController>();


        if (cdController.CooldownPassed() == true)
        {
            var projectile = Instantiate(abilityPrefab, caster.transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = caster.GetComponent<AimIndicator>().direction.normalized * speed;

            cdController.ResetCooldown(cooldown);
        }
    }

}