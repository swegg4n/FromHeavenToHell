﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class EnemyManager : MonoBehaviour
{
    #region singleton
    public static EnemyManager instance;


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

    private float timeBetweenSpawn;    //Tiden det tar mellan varje våg av fiender skapas. Mäts i sekunder
    private float delay;       //Tiden det tar från att platsen en fiende ska skapas på visas till att fienden skapas
    private int nrOfEnemiesToSpawn;    //Antal fiender som ska skapas i varje våg

    private float timeSinceLastSpawn;       //Tiden från förra vågen fiender skapades

    private List<Vector3> tilePositionList;     //Tillåtna platser för finder att uppstå på

    private List<GameObject> enemyList;     //Lista med alla fiender som skapats
    private List<int> tempIndexList;    
    private List<GameObject> spawnIndicatorList;    //Lista med alla spawn-indikatorer

    private Tilemap wallTileMap;        //Otillåten tile att spawna på
    private Tilemap topWallTileMap;     //Otillåten tile att spawna på

    [SerializeField] private GameObject spawnIndicator;
    [SerializeField] private GameObject enemy;

    private Random rnd = new Random();

    public List<Objective> BossObjectives { get; set; }

    /// <summary>
    /// Kallas innan första uppdateringen
    /// </summary>
    private void Start()
    {
        ResetRoom();
        BossObjectives = new List<Objective>();
    }

    /// <summary>
    /// Återställer rummet och räknar ut tillåtna spawn-positioner för nya rummet
    /// </summary>
    public void ResetRoom()
    {
        ResetVariabels();
        SpawnPositions();
    }

    /// <summary>
    /// Räknar ut var fiender får uppstå
    /// </summary>
    private void SpawnPositions()
    {
        Tilemap groundTileMap = GameManager.instance.CurrentRoom.GetComponent<Room>().GetTileMap(TileTypes.Ground);

        int roomSizeX = GameManager.instance.CurrentRoom.GetComponent<Room>().GetRoomSize().x;
        int roomSizeY = GameManager.instance.CurrentRoom.GetComponent<Room>().GetRoomSize().y;


        for (int x = GameManager.instance.CurrentRoom.GetComponent<Room>().RoomBounds.Item1.x; x < roomSizeX; x++)     //Loopar igenom alla tiles i bredd
        {
            for (int y = GameManager.instance.CurrentRoom.GetComponent<Room>().RoomBounds.Item2.x; y < roomSizeY; y++)     //Loopar igenom alla tiles i höjd
            {
                Vector3Int localPlace = new Vector3Int(x, y, 0);    //Tiles local-position
                Vector3 place = groundTileMap.CellToWorld(new Vector3Int(localPlace.x, localPlace.y, 0));   //Tilens world-position

                place.x += groundTileMap.cellSize.x / 2;    //Centrerar spawn-postionen i bredd
                place.y += groundTileMap.cellSize.y / 2;    //Centrerar spawn-positionen i höjd

                //Kontrollerar så att det finns en golv-tile och ingen vägg- eller top-tile
                if (GameManager.instance.CurrentRoom.GetComponent<Room>().CheckOnlyGroundTile(localPlace))
                {
                    tilePositionList.Add(place);    //Lägger till tilen i listan över tillåtna positioner att skapas på
                }
            }
        }
    }

    //Återställer rummet
    private void ResetVariabels()
    {
        nrOfEnemiesToSpawn = GameManager.instance.CurrentRoom.GetComponent<Room>().NrOfEnemiesToSpawn;
        delay = GameManager.instance.CurrentRoom.GetComponent<Room>().Delay;
        timeBetweenSpawn = GameManager.instance.CurrentRoom.GetComponent<Room>().TimeBetweenSpawn;
        timeSinceLastSpawn = timeBetweenSpawn;

        if (enemyList != null)
        {
            enemyList.ForEach(Destroy); //Förstör alla fiender i det gamla rummet
        }

        if (spawnIndicatorList != null)
        {
            spawnIndicatorList.ForEach(Destroy); //Förstör alla spawnindicators i det gamla rummet
        }

        tempIndexList = new List<int>();

        enemyList = new List<GameObject>();

        tilePositionList = new List<Vector3>();
    }

    private void Update()
    {
        if (GameManager.instance.Paused == false)
        {
            UpdateSpawnCooldown();
        }
    }

    /// <summary>
    /// Räknar ner tiden till fiender kan skapas
    /// </summary>
    private void UpdateSpawnCooldown()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn > timeBetweenSpawn)
        {
            StartCoroutine(Spawn());

            timeSinceLastSpawn = 0;
        }
    }

    /// <summary>
    /// Skapar fiender
    /// </summary>
    private IEnumerator Spawn()
    {
        tempIndexList = new List<int>();
        spawnIndicatorList = new List<GameObject>();

        for (int i = 0; i < nrOfEnemiesToSpawn; i++)
        {
            tempIndexList.Add(rnd.Next(0, tilePositionList.Count));

            spawnIndicatorList.Add(Instantiate(spawnIndicator, tilePositionList[tempIndexList[i]], Quaternion.identity));
        }

        yield return new WaitForSeconds(delay);

        for (int i = 0; i < nrOfEnemiesToSpawn; i++)
        {
            try
            {
                enemyList.Add(Instantiate(enemy, tilePositionList[tempIndexList[i]], Quaternion.identity));
                Destroy(spawnIndicatorList[i]);
            }
            catch (Exception)
            {

            }
        }

        spawnIndicatorList.Clear();
        tempIndexList.Clear();
    }
}