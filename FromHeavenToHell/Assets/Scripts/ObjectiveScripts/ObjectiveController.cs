using System.Collections;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    [SerializeField] private Objective objective;//temp serializeField, ska fås från rummet (currentRoom.objective  typ)
    public bool ObjectiveCompleted { get; private set; }

    private int killCount;
    private float timePassed;


    private void Awake()
    {
        ObjectiveCompleted = true;
    }

    /// <summary>
    /// Resettar objektivet
    /// Ska kallas vid varje rum-byte
    /// </summary>
    public void StartObjective()
    {
        /*set objective from room*/
        ObjectiveCompleted = false;
        killCount = 0;
        timePassed = 0;

        ValidateCompletion();
    }

    /// <summary>
    /// Timer som räknar upp tills spelarna överlevt tillräckligt länge 
    /// </summary>
    private void Update()
    {
        if (objective.IsSurviveObjective == true && ObjectiveCompleted == false)
        {
            if (timePassed < objective.SurvivalTime)
            {
                timePassed += Time.deltaTime;
                //Debug.Log(timePassed);
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
        killCount++;
        Debug.Log($"You Have {killCount} kills");
    }

    /// <summary>
    /// Kontrollerar om alla del-objektiv har utfötrs
    /// </summary>
    private void ValidateCompletion()
    {
        bool killCompleted;
        bool survivalCompleted;

        if (objective.IsKillObjective == false || killCount >= objective.KillCount)  //kontrollerar om objektivet är ett döda-objektiv och om spelarna isf. har dödat tillräckligt många fiender
        {
            killCompleted = true;
        }
        else
        {
            killCompleted = false;
        }

        if (objective.IsSurviveObjective == false || timePassed >= objective.SurvivalTime)   //kontrollerar om objektivet är ett överlevnads-objektiv och om spelarna isf. har överlevt tillräckligt länge
        {
            survivalCompleted = true;
        }
        else
        {
            survivalCompleted = false;
        }


        if (killCompleted == true && survivalCompleted == true)
        {
            ObjectiveCompleted = true;
            Debug.Log("OBJECTIVE COMPLETED!");

        }
    }

}