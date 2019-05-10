using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentObjectiveText : MonoBehaviour
{
    [SerializeField] Text surviveText, killText, bossText;
    [SerializeField] Slider bossHealthBar;

    private bool bossHealthBarSet;

    Objective currentObjective;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentObjective = GameManager.instance.CurrentRoom.GetComponent<Room>().Objective;

        if (currentObjective.IsSurviveObjective == true)
        {
            surviveText.text = "Stay Alive For "
            + GameManager.instance.GetComponent<ObjectiveController>().TimePassed.ToString("0.0") +
            " / "
            + currentObjective.SurvivalTime +
            " Seconds";
        }
        else
        {
            surviveText.text = "";
        }

        if (currentObjective.IsKillObjective == true)
        {
            killText.text = "Kill " + GameManager.instance.GetComponent<ObjectiveController>().KillCount + " / " + currentObjective.KillCount + " Enemies";
        }
        else
        {
            killText.text = "";
        }

        if(currentObjective.IsBossObjective == true)
        {
            if(bossHealthBarSet == false)
            {
                Instantiate(bossHealthBar, gameObject.transform);
                bossHealthBarSet = true;
            }

            if(GameManager.instance.GetComponent<ObjectiveController>().BossCompleted == true)
            {
                GameManager.instance.gameWon = true;
                bossText.text = "You Killed The Boss!";
            }
            else
            {
                bossText.text = "Kill The Boss!";
            }
        }
        else
        {
            bossText.text = "";
        }
    }
}
