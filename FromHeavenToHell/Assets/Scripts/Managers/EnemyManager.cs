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
    [SerializeField] private float delay;
    [SerializeField] private int nrOfEnemiesToSpawn;
    private float timeSinceLastSpawn;

    [SerializeField] private Tilemap groundTileMap;
    private List<Vector3> tilePositionList;

    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Tilemap topWallTilemap;

    [SerializeField] private GameObject spawnIndicator;
    [SerializeField] private GameObject enemy;

    private Random rnd = new Random();

    /// <summary>
    /// Skapar en lista med tillåtna spawnpositioner
    /// </summary>
    private void Start()
    {
        tilePositionList = new List<Vector3>();
        
        for (int x = groundTileMap.cellBounds.xMin; x < groundTileMap.cellBounds.xMax; x++)
        {
            for (int y = groundTileMap.cellBounds.yMin; y < groundTileMap.cellBounds.yMax; y++)
            {
                Vector3Int localPlace = new Vector3Int(x, y, (int)groundTileMap.transform.position.y);
                Vector3 place = groundTileMap.CellToWorld(new Vector3Int(localPlace.x, localPlace.y, localPlace.z));
                place.x += groundTileMap.cellSize.x / 2;
                if (groundTileMap.HasTile(localPlace) == true
                    && wallTilemap.HasTile(localPlace) == false
                    && topWallTilemap.HasTile(localPlace) == false)
                {
                    tilePositionList.Add(place);
                }
            }
        }
    }
    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn > timeBetweenSpawn)
        {
            StartCoroutine(Spawn(nrOfEnemiesToSpawn, delay));

            timeSinceLastSpawn = 0;
        }
        
    }

    private IEnumerator Spawn(int nrOfSpawns, float delay)
    {
        List<int> tempIndexList = new List<int>();
        List<GameObject> spawnIndicatorList = new List<GameObject>();
        for (int i = 0; i < nrOfSpawns; i++)
        {
            tempIndexList.Add(rnd.Next(0, tilePositionList.Count));
            spawnIndicatorList.Add(Instantiate(spawnIndicator, tilePositionList[tempIndexList[i]], Quaternion.identity));
        }

        yield return new WaitForSeconds(delay);

        for (int i = 0; i < nrOfSpawns; i++)
        {
            Instantiate(enemy, tilePositionList[tempIndexList[i]], Quaternion.identity);
            Destroy(spawnIndicatorList[i]);
        }

        spawnIndicatorList.Clear();
        tempIndexList.Clear();
    }
}
