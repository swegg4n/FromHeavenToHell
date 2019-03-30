using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/New Ability", order = 1)]
public class AbilityProperty : ScriptableObject
{
    public enum AbilityType { Projectile, StaticBeam };

    public Sprite icon;
    public Color color;

    public AbilityType abilityType;

    public float
        damage,
        speed,
        fireRate,
        castTime;

    public GameObject projectilePrefab;
    public GameObject staticBeamPrefab;
}
