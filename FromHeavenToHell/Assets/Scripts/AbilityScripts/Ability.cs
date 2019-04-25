using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [SerializeField] protected GameObject abilityPrefab;        //Prefaben som instansieras som abilityn
    [SerializeField] private string abilityName = "New Ability";    //Abilityns namn
    [SerializeField] private Sprite abilityIcon;        //Ikon för abilityn
    [SerializeField] private AudioClip abilitySound;    //Ljud som spelas vid användning av abilityn
    [SerializeField] private int damage;        //Skada abilityn gör vid träff av ojekt som kan ta skada. Mäts i enheter
    public int Damage { get { return damage; } }

    [SerializeField] private float optimalRange;
    public float OptimalRange { get { return optimalRange; } }

    /// <summary>
    /// Metod som används för att aktivera abilities
    /// </summary>
    /// <param name="caster">Objektet som använder abilityn</param>
    public abstract void TriggerAbility(GameObject caster); 

    public virtual void Update() { }
}