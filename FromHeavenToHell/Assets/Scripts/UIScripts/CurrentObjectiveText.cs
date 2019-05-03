using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentObjectiveText : MonoBehaviour
{
    Objective currentObjective;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currentObjective = GameManager.instance.CurrentRoom.GetComponent<Room>().Objective;

        if(currentObjective.IsKillObjective == true 
            && currentObjective.IsSurviveObjective == false)
        {
            text.text = "Current Objective: Kill " + GameManager.instance.GetComponent<ObjectiveController>().KillCount + " / " + currentObjective.KillCount + " Enemies";
        }
        else
        {
            text.text = "Current Objective: Stay Alive For "
            + GameManager.instance.GetComponent<ObjectiveController>().TimePassed.ToString("0.0") +
            " / "
            + currentObjective.SurvivalTime +
            " Seconds";
        }
    }
}
