using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using Assets.Classes;

public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion



    [SerializeField] private GameObject currentRoom;
    public GameObject CurrentRoom { set { currentRoom = value; } get { return currentRoom; } }


    public bool GameLost { set; get; }
    public bool GameWon { set; get; }

    public int tileSize { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        tileSize = 32; //Vem hårdkodade detta? Inte Jonathan eller Oscar i all fall...
    }

    // Update is called once per frame
    void Update()
    {
        if (GameLost == true || GameWon == true)     // if(gameLost == true || gameWon || true)   <-- ok jonathan jag tror inte det är en bra ide
        {                                                                                          //att resize:a en array varje frame! ):<
            AddCurrentGameStats();

            if (GameLost == true)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }
            else if (GameWon == true)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }
        }
    }

    private void AddStats(PlayerPrefKey key, int stat)
    {
        int[] statArray = PlayerPrefsX.GetIntArray(key.ToString());
        int oldArrayLength = PlayerPrefsX.GetIntArray(key.ToString()).Length;

        Array.Resize(ref statArray, oldArrayLength + 1);

        statArray[oldArrayLength] = stat;

        PlayerPrefsX.SetIntArray(key.ToString(), statArray);
    }

    private void AddCurrentGameStats()
    {
        AddStats(PlayerPrefKey.AngelDamageDealtToEnemies, StatTracker.AngelDamageDealtToEnemies);
        AddStats(PlayerPrefKey.DemonDamageDealtToEnemies, StatTracker.DemonDamageDealtToEnemies);
        AddStats(PlayerPrefKey.AngelDamageTaken, StatTracker.AngelDamageTaken);
        AddStats(PlayerPrefKey.DemonDamageTaken, StatTracker.DemonDamageTaken);
        AddStats(PlayerPrefKey.AngelEnemiesKilled, StatTracker.AngelEnemiesKilled);
        AddStats(PlayerPrefKey.DemonEnemiesKilled, StatTracker.DemonEnemiesKilled);
        AddStats(PlayerPrefKey.AngelDamageDealtToDemon, StatTracker.AngelDamageDealtToDemon);
        AddStats(PlayerPrefKey.DemonDamageDealtToAngel, StatTracker.DemonDamageDealtToAngel);
        AddStats(PlayerPrefKey.AngelSelfDamage, StatTracker.AngelSelfDamage);
        AddStats(PlayerPrefKey.DemonSelfDamage, StatTracker.DemonSelfDamage);
    }
}
