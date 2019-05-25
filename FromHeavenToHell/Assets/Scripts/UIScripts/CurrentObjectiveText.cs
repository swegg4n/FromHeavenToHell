using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentObjectiveText : MonoBehaviour
{
    [SerializeField] private Text surviveText, killText, bossText;
    [SerializeField] private GameObject tutPanel;
    [SerializeField] private Slider bossHealthBar;

    private Color hellAmbientColor;
    private Color heavenAmbientColor;

    private bool bossHealthBarSet;

    private Color textColor;

    Objective currentObjective;



    // Start is called before the first frame update
    void Start()
    {
        hellAmbientColor = new Color(0.75f, 0.1f, 0.1f);
        heavenAmbientColor = new Color(0.75f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        currentObjective = GameManager.instance.CurrentRoom.GetComponent<Room>().Objective;
        bossText.gameObject.SetActive(true);
        bossText.text = GameManager.instance.GetComponent<ObjectiveController>().NrOfBossesCompleted + "/" + EnemyManager.instance.BossObjectives.Count;

        if (GameManager.instance.CurrentRoom.GetComponent<Room>().IsHeavenRoom == true)
        {
            RenderSettings.ambientLight = heavenAmbientColor;

            bossText.color = Color.red;
            surviveText.color = Color.red;
            killText.color = Color.red;
        }
        else if (GameManager.instance.CurrentRoom.GetComponent<Room>().IsHellRoom == true)
        {
            RenderSettings.ambientLight = hellAmbientColor;

            bossText.color = Color.green;
            surviveText.color = Color.green;
            killText.color = Color.green;
        }
        else if (GameManager.instance.CurrentRoom.GetComponent<Room>().IsBossRoom == true)
        {
            RenderSettings.ambientLight = heavenAmbientColor;

            bossText.color = Color.red;
            surviveText.color = Color.red;
            killText.color = Color.red;
        }

        if (currentObjective.IsSurviveObjective == true && GameManager.instance.CurrentRoom.GetComponent<Room>().IsBossRoom == false &&
            GameManager.instance.CurrentRoom.GetComponent<Room>().IsStartRoom == false)
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

        if (currentObjective.IsKillObjective == true && GameManager.instance.CurrentRoom.GetComponent<Room>().IsBossRoom == false &&
                GameManager.instance.CurrentRoom.GetComponent<Room>().IsStartRoom == false)
        {
            killText.text = GameManager.instance.GetComponent<ObjectiveController>().KillCount + " / " + currentObjective.KillCount;
            killText.gameObject.SetActive(true);
        }
        else
        {
            killText.gameObject.SetActive(false);
        }

        if (GameManager.instance.CurrentRoom.GetComponent<Room>().IsStartRoom == true)
        {
            tutPanel.SetActive(true);
        }
        else
        {
            tutPanel.SetActive(false);
        }

        if (currentObjective.IsBossObjective == true)
        {
            //if (GameManager.instance.GetComponent<ObjectiveController>().BossCompleted == true)
            //{
            //    GameManager.instance.GameWon = true;
            //    bossText.text = "1 / 1";
            //}
            //else
            //{
            //    bossText.text = "0 / 1";
            //}
            bossHealthBar.gameObject.SetActive(true);
        }
        else
        {
            bossHealthBar.gameObject.SetActive(false);
        }
    }
}
