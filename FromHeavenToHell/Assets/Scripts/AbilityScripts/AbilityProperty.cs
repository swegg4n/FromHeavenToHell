using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/New Ability", order = 1)]
public class AbilityProperty : ScriptableObject
{
    public enum AbilityType { Projectile, Beam };

    public Sprite icon;
    public Color color;

    public AbilityType abilityType;

    public float
        damage,
        speed,
        fireRate,
        castTime;

    public GameObject projectilePrefab;
    public GameObject beamPrefab;



    public void UseAbility(Vector3 position, Vector3 direction)
    {
        Debug.Log("fire");


        if (abilityType == AbilityType.Projectile)
        {
            var projectile = Instantiate(projectilePrefab, position, Quaternion.identity);

            if (icon != null)
            {
                projectile.GetComponent<SpriteRenderer>().sprite = icon;
            }
            else
            {
                Debug.LogWarning("Ability has no assigned sprite!");
            }
            
            projectile.GetComponent<SpriteRenderer>().color = color;

            projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
        }

        if (abilityType == AbilityType.Beam)
        {
            Instantiate(beamPrefab, position, Quaternion.identity);

        }
    }

}
