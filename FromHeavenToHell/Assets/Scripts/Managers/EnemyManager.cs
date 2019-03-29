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

    [SerializeField] private float timeBetweenSpawn;
    private float timeSinceLastSpawn;
    [SerializeField] private int nrOfEnemiesToSpawn;

    [SerializeField] private Tilemap groundTileMap;
    private Vector2Int[] tilePositionsArray;

    Vector2 bounds;
    BoundsInt nrOfCells;

    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Tilemap topWallTilemap;

    [SerializeField] private GameObject bunnyDog;

    private Random rnd = new Random();

    void Start()
    {
        timeBetweenSpawn = 2;

        bounds = groundTileMap.cellSize;
        nrOfCells = groundTileMap.cellBounds;
        tilePositionsArray = new Vector2Int[nrOfCells.x * nrOfCells.y];
        int indexCounter = 0;
        for (int x = 0; x < nrOfCells.x; x =+ (int)bounds.x)
        {
            for (int y = 0; y < nrOfCells.y; y =+ (int)bounds.y)
            {
                tilePositionsArray[indexCounter] = new Vector2Int(x, y);
                indexCounter++;
            }
        }
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn > timeBetweenSpawn)
        {
            Spawn(4);
            timeSinceLastSpawn = 0;
        }
        
    }

    void Spawn(int nrOfSpawns)
    {
        int counter = nrOfSpawns;

        while (counter > 0)
        {
            for (int i = 0; i < nrOfSpawns; i++)
            {
                int tempSpawnTile = rnd.Next(0, tilePositionsArray.Length);

                if (!wallTilemap.HasTile((Vector3Int)tilePositionsArray[tempSpawnTile]) && !topWallTilemap.HasTile((Vector3Int)tilePositionsArray[tempSpawnTile]))
                {
                    Instantiate(bunnyDog);
                    nrOfSpawns--;
                }

            }
        }
    }
}
