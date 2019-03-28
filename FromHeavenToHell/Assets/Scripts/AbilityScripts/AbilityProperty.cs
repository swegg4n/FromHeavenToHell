using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/New Ability", order = 1)]
public class AbilityProperty : ScriptableObject
{
    public enum AbilityType { Projectile, Ray };

    public Texture2D icon;
    public Color color;

    public AbilityType abilityType;

    public float
        damage,
        speed,
        fireRate,
        castTime;

    public GameObject prefab;



    public void UseAbility(Vector3 position, Vector3 direction)
    {
        Debug.Log("fire");

        var projectile = Instantiate(prefab, position, Quaternion.identity);

        projectile.GetComponent<SpriteRenderer>().color = color;

        projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;

    }

}
