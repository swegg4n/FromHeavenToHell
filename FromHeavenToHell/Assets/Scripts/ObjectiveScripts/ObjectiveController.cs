using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    private Objective objective;    //Rummets objektiv

    public bool ObjectiveCompleted { get; private set; }    //Om objektivet är färdigt eller inte
    public int KillCount { get; private set; }      //Antal dödade fiender i rummet
    public float TimePassed { get; private set; }       //Tiden spelarna varit i rummet
    public bool BossCompleted { get; private set; }     //Om bossen är dödad eller inte

    private bool killCompleted;     //Om döda-objektivet är avklarat eller inte
    private bool survivalCompleted;     //Om överlev-objektivet är avklarat eller inte

    public int NrOfBossesCompleted { get; private set; }    //Antal bossar dödade


    private void Start()
    {
        StartObjective();
        NrOfBossesCompleted = 0;
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
        objective.ObjectiveCompleted = false;
        KillCount = 0;
        TimePassed = 0;

        ValidateCompletion();
    }

    /// <summary>
    /// Timer som räknar upp tills spelarna överlevt tillräckligt länge 
    /// </summary>
    private void Update()
    {
        if (GameManager.instance.Paused == false)
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
    }

    /// <summary>
    /// Lägger till en dödad till antal dödade fiender
    /// </summary>
    public void AddKill()
    {
        KillCount++;
    }

    /// <summary>
    /// Kontrollerar om alla del-objektiv har utfötrs
    /// </summary>
    private void ValidateCompletion()
    {
        //kontrollerar om objektivet är ett döda-objektiv och om spelarna isf. har dödat tillräckligt många fiender
        if (objective.IsKillObjective == false || KillCount >= objective.KillCount)  
        {
            killCompleted = true;
        }
        else
        {
            killCompleted = false;
        }
        //kontrollerar om objektivet är ett överlevnads-objektiv och om spelarna isf. har överlevt tillräckligt länge
        if (objective.IsSurviveObjective == false || TimePassed >= objective.SurvivalTime)   
        {
            survivalCompleted = true;
        }
        else
        {
            survivalCompleted = false;
        }

        //Kontrollerar om objektivet är ett boss-objektiv och om bossen isf. dödats
        if (objective.IsBossObjective == false || GameManager.instance.CurrentRoom.GetComponentInChildren<EnemyBaseClass>() == null)
        {
            BossCompleted = true;
        }
        else
        {
            BossCompleted = false;
        }

        //Om alla objektiv klarats av
        if (killCompleted == true && survivalCompleted == true && BossCompleted == true)
        {
            ObjectiveCompleted = true;
            if (objective.IsBossObjective == true)
            {
                NrOfBossesCompleted++;
                PlayerManager.instance.Health = PlayerManager.instance.MaxHealth;
            }
            Debug.Log("OBJECTIVE COMPLETED!");

            //Om alla bossar besegrats
            if (NrOfBossesCompleted == EnemyManager.instance.BossObjectives.Count)
            {
                GameManager.instance.GameWon = true;
            }
        }
    }

}