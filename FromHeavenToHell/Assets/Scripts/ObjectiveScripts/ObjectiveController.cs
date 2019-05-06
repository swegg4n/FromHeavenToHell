using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    private Objective objective;

    public bool ObjectiveCompleted { get; private set; }

    public int KillCount { get; private set; }

    public float TimePassed { get; private set; }

    public bool BossCompleted { get; private set; }

    bool killCompleted;
    bool survivalCompleted;

    private void Start()
    {
        StartObjective();
    }

    /// <summary>
    /// Resettar objektivet
    /// Ska kallas vid varje rum-byte
    /// </summary>
    public void StartObjective()
    {
        objective = GameManager.instance.CurrentRoom.GetComponent<Room>().Objective;    //Får objektivet från rummet

        if (objective == null)
        {
            Debug.LogError($"{GameManager.instance.CurrentRoom.name} has no objective! Assign an objective to the Room-script");
        }

        ObjectiveCompleted = false;
        KillCount = 0;
        TimePassed = 0;

        ValidateCompletion();
    }

    /// <summary>
    /// Timer som räknar upp tills spelarna överlevt tillräckligt länge 
    /// </summary>
    private void Update()
    {
        if (ObjectiveCompleted == false)
        {
            if (TimePassed < objective.SurvivalTime)
            {
                TimePassed += Time.deltaTime;
            }
            else
            {
                ValidateCompletion();
            }
        }
    }

    /// <summary>
    /// Lägger till en dödad till antal dödade fiender
    /// </summary>
    public void AddKill()
    {
        KillCount++;
        Debug.Log($"{KillCount}/{objective.KillCount} kills");
    }

    /// <summary>
    /// Kontrollerar om alla del-objektiv har utfötrs
    /// </summary>
    private void ValidateCompletion()
    {
        if (objective.IsKillObjective == false || KillCount >= objective.KillCount)  //kontrollerar om objektivet är ett döda-objektiv och om spelarna isf. har dödat tillräckligt många fiender
        {
            killCompleted = true;
        }
        else
        {
            killCompleted = false;
        }

        if (objective.IsSurviveObjective == false || TimePassed >= objective.SurvivalTime)   //kontrollerar om objektivet är ett överlevnads-objektiv och om spelarna isf. har överlevt tillräckligt länge
        {
            survivalCompleted = true;
        }
        else
        {
            survivalCompleted = false;
        }

        if(objective.IsBossObjective == false || GameManager.instance.CurrentRoom.GetComponentInChildren<EnemyBaseClass>() == null)
        {
            BossCompleted = true;
        }
        else
        {
            BossCompleted = false;
        }


        if (killCompleted == true && survivalCompleted == true && BossCompleted)
        {
            ObjectiveCompleted = true;
            Debug.Log("OBJECTIVE COMPLETED!");
        }
    }

}