using System.Collections;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    [SerializeField] private Objective objective;//temp serializeField, ska fås från rummet (currentRoom.objective  typ)
    private bool objectiveCompleted;

    private int killCount;
    private float timePassed;


    /// <summary>
    /// Resettar objektivet
    /// Ska kallas vid varje rum-byte
    /// </summary>
    private void StartObjective()
    {
        /*set objective from room*/
        objectiveCompleted = false;
        killCount = 0;
        timePassed = 0;

        ValidateCompletion();
    }

    /// <summary>
    /// Timer som räknar upp tills spelarna överlevt tillräckligt länge 
    /// </summary>
    private void Update()
    {
        if (objective.IsSurviveObjective == true && objectiveCompleted == false)
        {
            if (timePassed < objective.SurvivalTime)
            {
                timePassed += Time.deltaTime;
                Debug.Log(timePassed);
            }
            else
            {
                ValidateCompletion();
            }
        }
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
            objectiveCompleted = true;
            Debug.Log("OBJECTIVE COMPLETED!");
        }
    }

}