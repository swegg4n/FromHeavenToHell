using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentObjectiveText : MonoBehaviour
{
    [SerializeField] private Text surviveText, killText, bossText;
    [SerializeField] private Slider bossHealthBar;

    private bool bossHealthBarSet;

    private Color textColor;

    Objective currentObjective;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentObjective = GameManager.instance.CurrentRoom.GetComponent<Room>().Objective;
        bossText.gameObject.SetActive(true);
        bossText.text = GameManager.instance.GetComponent<ObjectiveController>().NrOfBossesCompleted + "/" + EnemyManager.instance.BossObjectives.Count;

        if (currentObjective.IsSurviveObjective == true)
        {
            surviveText.text = GameManager.instance.GetComponent<ObjectiveController>().TimePassed.ToString("0.0") +
            " / "
            + currentObjective.SurvivalTime;
            surviveText.gameObject.SetActive(true);
        }
        else
        {
            surviveText.gameObject.SetActive(false);
        }

        if (currentObjective.IsKillObjective == true)
        {
            killText.text = GameManager.instance.GetComponent<ObjectiveController>().KillCount + " / " + currentObjective.KillCount;
            killText.gameObject.SetActive(true);
        }
        else
        {
            killText.gameObject.SetActive(false);
        }

        if (currentObjective.IsBossObjective == true)
        {
            if (GameManager.instance.GetComponent<ObjectiveController>().BossCompleted == true)
            {
                GameManager.instance.GameWon = true;
                bossText.text = "1 / 1";
            }
            else
            {
                bossText.text = "0 / 1";
            }
        }
        else
        {
            bossHealthBar.gameObject.SetActive(false);
        }
    }
}
