using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [SerializeField] protected GameObject abilityPrefab;
    [SerializeField] private string abilityName = "New Ability";
    [SerializeField] private Sprite abilityIcon;
    [SerializeField] private AudioClip abilitySound;
    [SerializeField] private int damage;
    public int Damage { get { return damage; } }

    //Det avståndet från vilket abilityn är optimal att användas
    public float optimalRange;


    public abstract void TriggerAbility(GameObject caster);

    public virtual void Update() { }
}