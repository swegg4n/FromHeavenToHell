using Assets.Classes;
using System;
using System.Collections.Generic;
using UnityEngine;


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

    public enum Objects { PlayerAngel, PlayerDemon, Enemy, Wall }

    public static Dictionary<Objects, string> objectsTags = new Dictionary<Objects, string>()
    {
        { Objects.PlayerAngel, "PlayerAngel"},
        { Objects.PlayerDemon, "PlayerDemon"},
        { Objects.Enemy, "Enemy"},
        { Objects.Wall, "Wall" }
    };

    [SerializeField] private GameObject currentRoom;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject playerUICanvas;

    public GameObject CurrentRoom { set { currentRoom = value; } get { return currentRoom; } }

    [SerializeField] private GameObject shadowCube;
    public GameObject ShadowCube { get { return shadowCube; } }

    public bool GameLost { set; get; }
    public bool GameWon { set; get; }
    public bool Paused { set; get; }

    public int TileSize { get; private set; } = 32;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) == true)
        {
            Paused = !Paused;
        }

        if (Paused == true)
        {
            pauseMenuCanvas.SetActive(true);
            playerUICanvas.SetActive(false);
        }
        else
        {
            pauseMenuCanvas.SetActive(false);
            playerUICanvas.SetActive(true);
        }

        if (GameLost == true || GameWon == true)
        {
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