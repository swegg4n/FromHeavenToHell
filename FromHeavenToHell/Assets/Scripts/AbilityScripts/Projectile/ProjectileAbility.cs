using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Projectile Ability")]
public class ProjectileAbility : Ability
{
    [SerializeField] private float speed;   //Projektilens fart. Mäts i ...
    [SerializeField] private int range;     //Avståndet projektilen kan färdas innan den förstörs. Mäts i ...
    [SerializeField] private int knockbackForce;    //Kraften projektilen påverkar objekt som kan ta skada. Mäts i ...
    [SerializeField] private float cooldown;    //Tiden användaren måste vänta innan abilityn kan användas. Mäts i sekunder
    private float offset;

    public GameObject caster { get; private set; }  //Objektet som använder abilityn



    /// <summary>
    /// Det som händer när abilityn används
    /// </summary>
    public override void TriggerAbility(GameObject caster)
    {
        this.caster = caster;

        offset = caster.GetComponent<BoxCollider2D>().size.magnitude;

        Collider2D hitbox = caster.GetComponent<Collider2D>();
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

            var projectile = Instantiate(abilityPrefab,
                (Vector2)caster.transform.position + offset * direction.normalized,
                Quaternion.identity);

            projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;

            projectile.GetComponent<ProjectileBehaviour>().projectileAbility = this;

            cdController.ResetCooldown(cooldown);
        }
    }

    public int GetKnockbackForce()  //Ändra till property
    {
        return knockbackForce;
    }

    public int GetRange()   //Ändra till property
    {
        return range;
    }
}