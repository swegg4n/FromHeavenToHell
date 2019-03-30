using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [SerializeField] protected GameObject abilityPrefab;
    [SerializeField] protected string abilityName = "New Ability";
    [SerializeField] protected Sprite abilityIcon;
    [SerializeField] protected AudioClip abilitySound;


    public abstract void TriggerAbility(GameObject player);

    public abstract void UpdateCooldown(GameObject player);

}