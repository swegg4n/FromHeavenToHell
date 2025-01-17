﻿using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Projectile Ability")]
public class ProjectileAbility : Ability
{
    [SerializeField] private float speed;   //Projektilens fart
    [SerializeField] private int range;     //Avståndet projektilen kan färdas innan den förstörs
    [SerializeField] private int knockbackForce;    //Kraften projektilen påverkar objekt som kan ta skada
    [SerializeField] private float cooldown;    //Tiden användaren måste vänta innan abilityn kan användas
    [SerializeField] private int nrOfProjectiles;   //Antaler projektiler som ska skjutas samtidigt
    [SerializeField] private float spread; //Vinkeln offset från siktet som projektierna sprids från
    private float offset;    //Hur långt ifrån castern specialförmågnan ska användas


    /// <summary>
    /// Det som händer när abilityn används
    /// </summary>
    public override void TriggerAbility(GameObject caster)
    {
        offset = caster.GetComponent<BoxCollider2D>().size.magnitude;

        CooldownController cdController = caster.GetComponent<CooldownController>();    //Script som räknar ut hur länge objekt måste vänta innan de kan använda förmågor

        if (cdController.ProjectileCooldownPassed() == true)
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
            Vector3 directionInVector3 = new Vector3(direction.x, direction.y, 0);
            Vector3 startDirection = Quaternion.AngleAxis(-spread / 2, Vector3.back) * directionInVector3;  //Räknar ut var den första projektilen ska skjutas från så det blir jämn riktning på skotten jämfört med siktet.
            float spreadInterval = spread / (nrOfProjectiles + 1);  //Avståndet mellan projektilerna, om det bara finns 1 projektil så centreras den

            for (int i = 0; i < nrOfProjectiles; i++)
            {
                startDirection = Quaternion.AngleAxis(spreadInterval, Vector3.back) * startDirection;   //Roterar startdirection med värdet på spreadinterval
                var projectile = Instantiate(abilityPrefab,
                caster.transform.position + offset * startDirection.normalized, Quaternion.identity);

                projectile.GetComponent<Rigidbody2D>().velocity = startDirection.normalized * speed;
                projectile.GetComponent<ProjectileBehaviour>().ProjectileAbility = this;
                projectile.GetComponent<ProjectileBehaviour>().Caster = caster;
            }

            cdController.ResetProjectileCooldown(cooldown);
        }
    }

    public int GetKnockbackForce()
    {
        return knockbackForce;
    }

    public int GetRange()
    {
        return range;
    }
}