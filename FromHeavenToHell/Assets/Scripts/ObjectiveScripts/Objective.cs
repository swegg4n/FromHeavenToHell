using UnityEngine;

[CreateAssetMenu(menuName = "Objective/Multi-Objective")]
public class Objective : ScriptableObject
{
    [SerializeField] bool isKillObjective;      //om detta är ett döda-objektiv
    public bool IsKillObjective { get { return isKillObjective; } }

    [SerializeField] bool isSurviveObjective;       //om detta är ett överlevnads-objektiv
    public bool IsSurviveObjective { get { return isSurviveObjective; } }

    [SerializeField] bool isBossObjective;       //om detta är ett boss-objektiv
    public bool IsBossObjective { get { return isBossObjective; } }


    [Tooltip("The amount of enemies required to kill to complete the objective")]
    [SerializeField] private int killCount;       //Antal fiender som måster dödas
    public int KillCount { get { return killCount; } }

    [Tooltip("The number of seconds the players need to survive before completing the objective")]
    [SerializeField] private float survivalTime;        //Tiden man måste överleva. Mäts i sekunder
    public float SurvivalTime { get { return survivalTime; } }
}