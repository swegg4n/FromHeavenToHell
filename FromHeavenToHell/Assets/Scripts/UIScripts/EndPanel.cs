using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Classes;

public class EndPanel : MonoBehaviour
{
    private int numberOfStats;

    [SerializeField] private Text endText, demonDamageDealtToEnemiesText, angelDamageDealtToEnemiesText, demonDamageTakenText, angelDamageTakenText, demonDamageDealtToAngelText, angelDamageDealtToDemonText, demonSelfDamageText, angelSelfDamageText;

    // Start is called before the first frame update
    void Start()
    {
        foreach (PlayerPrefKey key in (PlayerPrefKey[])Enum.GetValues(typeof(PlayerPrefKey)))
        {
            numberOfStats++;
        }

        if (GameManager.instance.gameLost == true)
        {
            endText.text = "HAHA You lost scrub git gud N00B REEEEEEEE!!! \n Press Enter To Continue";
        }
        else if (GameManager.instance.gameWon == true)
        {
            endText.text = "AWWW SNAP You Won!? Gratz i guess... Not Sure What You Expected Here... \n Press Enter To Continue";
        }

        SetStatText();
    }

    // Update is called once per frame
    void Update()
    {

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
