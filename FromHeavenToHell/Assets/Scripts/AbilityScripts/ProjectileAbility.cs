using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Projectile Ability")]
public class ProjectileAbility : Ability
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private int range;
    [SerializeField] private int knockbackForce;
    [SerializeField] private float fireRate;

    private float p1CountDown = 0;
    private float p2CountDown = 0;



    public override void TriggerAbility(GameObject player)
    {
        if (player.tag == "PlayerDemon" && p1CountDown <= 0)
        {
            Fire(player);

            p1CountDown = 1f / fireRate;
        }
        else if (player.tag == "PlayerAngel" && p2CountDown <= 0)
        {
            Fire(player);

            p2CountDown = 1f / fireRate;
        }
    }


    private void Fire(GameObject player)
    {
        var projectile = Instantiate(abilityPrefab, player.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = player.GetComponent<AimIndicator>().direction.normalized * speed;
    }


    public override void UpdateCooldown(GameObject player)
    {
        if (player.tag == "PlayerDemon" && p1CountDown > 0)
        {
            p1CountDown -= Time.deltaTime;
        }

        if (player.tag == "PlayerAngel" && p2CountDown > 0)
        {
            p2CountDown -= Time.deltaTime;
        }
    }

}