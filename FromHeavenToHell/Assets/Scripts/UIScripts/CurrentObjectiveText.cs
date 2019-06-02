using UnityEngine;
using UnityEngine.UI;

public class CurrentObjectiveText : MonoBehaviour
{
    [SerializeField] private Text surviveText, killText, bossText;  //Olika objective texter
    [SerializeField] private GameObject tutPanel;   //panel för kort genomgång
    [SerializeField] private Slider bossHealthBar;  //Bossens health bar

    private Color hellAmbientColor;     //Färgschema för helvetet
    private Color heavenAmbientColor;   //Färgschema för himeln

    private bool bossHealthBarSet;

    private Color textColor;


    private void Start()
    {
        hellAmbientColor = new Color(0.75f, 0.1f, 0.1f);
        heavenAmbientColor = new Color(0.75f, 1f, 1f);
    }

    private void Update()
    {
        Room currentRoom = GameManager.instance.CurrentRoom.GetComponent<Room>();
        Objective currentObjective = GameManager.instance.CurrentRoom.GetComponent<Room>().Objective;
        bossText.gameObject.SetActive(true);
        bossText.text = GameManager.instance.GetComponent<ObjectiveController>().NrOfBossesCompleted + "/" + EnemyManager.instance.BossObjectives.Count;

        UpdateRoomSettings(currentRoom);
        UpdateObjectiveText(currentObjective);
    }

    /// <summary>
    /// Uppdaterar färgschema i rummet
    /// </summary>
    private void UpdateRoomSettings(Room room)
    {
        //om rummet är ett himmelrum
        if (room.IsHeavenRoom == true)
        {
            RenderSettings.ambientLight = heavenAmbientColor;

            bossText.color = Color.red;
            surviveText.color = Color.red;
            killText.color = Color.red;
        }
        //om rummet är ett helvetesrum
        else if (room.IsHellRoom == true)
        {
            RenderSettings.ambientLight = hellAmbientColor;

            bossText.color = Color.green;
            surviveText.color = Color.green;
            killText.color = Color.green;
        }
        //om rummet är ett bossrum
        else if (room.IsBossRoom == true)
        {
            RenderSettings.ambientLight = heavenAmbientColor;

            bossText.color = Color.red;
            surviveText.color = Color.red;
            killText.color = Color.red;
        }

        //om rummet är startrummet
        if (room.IsStartRoom == true)
        {
            tutPanel.SetActive(true);
        }
        else
        {
            tutPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Uppdaterar objective UI i rummet
    /// </summary>
    private void UpdateObjectiveText(Objective objective)
    {
        if (objective.IsSurviveObjective == true && GameManager.instance.CurrentRoom.GetComponent<Room>().IsBossRoom == false &&
            GameManager.instance.CurrentRoom.GetComponent<Room>().IsStartRoom == false)
        {
            surviveText.text = GameManager.instance.GetComponent<ObjectiveController>().TimePassed.ToString("0.0") +
                " / " + objective.SurvivalTime;
            surviveText.gameObject.SetActive(true);
        }
        else
        {
            surviveText.gameObject.SetActive(false);
        }

        if (objective.IsKillObjective == true && GameManager.instance.CurrentRoom.GetComponent<Room>().IsBossRoom == false &&
                GameManager.instance.CurrentRoom.GetComponent<Room>().IsStartRoom == false)
        {
            killText.text = GameManager.instance.GetComponent<ObjectiveController>().KillCount + " / " + objective.KillCount;
            killText.gameObject.SetActive(true);
        }
        else
        {
            killText.gameObject.SetActive(false);
        }

        if (objective.IsBossObjective == true)
        {
            bossHealthBar.gameObject.SetActive(true);
        }
        else
        {
            bossHealthBar.gameObject.SetActive(false);
        }
    }
}
