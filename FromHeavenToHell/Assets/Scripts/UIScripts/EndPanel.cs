using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Classes;

public class EndPanel : MonoBehaviour
{
    private int numberOfStats;

    [SerializeField] private Text endText;
    [SerializeField] private Text angelDamageDealtToEnemiesText, angelDamageTakenText, angelDamageDealtToDemonText, angelSelfDamageText;
    [SerializeField] private Text demonDamageDealtToEnemiesText, demonDamageTakenText, demonDamageDealtToAngelText, demonSelfDamageText;


    private void Start()
    {
        foreach (PlayerPrefKey key in (PlayerPrefKey[])Enum.GetValues(typeof(PlayerPrefKey)))
        {
            numberOfStats++;
        }

        if (GameManager.instance.GameLost == true)
        {
            endText.text = "You Died!.\nPress 'Enter' To Continue";
        }
        else if (GameManager.instance.GameWon == true)
        {
            endText.text = "AWWW SNAP You Won!? Gratz i guess... Not Sure What You Expected Here... \n Press Enter To Continue";
        }

        SetStatText();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))   //TEMPORÄRT
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }


    private void SetStatText()
    {
        demonDamageDealtToEnemiesText.text = StatTracker.DemonDamageDealtToEnemies.ToString();
        angelDamageDealtToEnemiesText.text = StatTracker.AngelDamageDealtToEnemies.ToString();

        demonDamageTakenText.text = StatTracker.DemonDamageTaken.ToString();
        angelDamageTakenText.text = StatTracker.AngelDamageTaken.ToString();

        demonDamageDealtToAngelText.text = StatTracker.DemonDamageDealtToAngel.ToString();
        angelDamageDealtToDemonText.text = StatTracker.AngelDamageDealtToDemon.ToString();

        demonSelfDamageText.text = StatTracker.DemonSelfDamage.ToString();
        angelSelfDamageText.text = StatTracker.AngelSelfDamage.ToString();
    }
}
