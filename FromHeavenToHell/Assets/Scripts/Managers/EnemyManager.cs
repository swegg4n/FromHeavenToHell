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

    [SerializeField] private float timeBetweenSpawn;    //Tiden det tar mellan varje våg av fiender skapas. Mäts i sekunder
    [SerializeField] private float delay;       //Tiden det tar från att platsen en fiende ska skapas på visas till att fienden skapas. Mäts i sekunder
    [SerializeField] private int nrOfEnemiesToSpawn;    //Antal fiender som ska skapas i varje våg

    private float timeSinceLastSpawn;       //Tiden från förra vågen fiender skapades. Mäts i sekunder

    //private Tilemap groundTileMap;
    private List<Vector3> tilePositionList;

    //private Tilemap wallTileMap; // Ska hämta dessa från gameManager
    //private Tilemap topWallTileMap;

    [SerializeField] private GameObject spawnIndicator;
    [SerializeField] private GameObject enemy;

    private Random rnd = new Random();


    /// <summary>
    /// Kallas innan första uppdateringen
    /// </summary>
    private void Start()
    {
        DetectSpawnpoints();
    }

    /// <summary>
    /// Skapar en lista med tillåtna spawnpositioner
    /// </summary>
    private void DetectSpawnpoints()
    {
       // groundTileMap = GameManager.instance.GetTileMap("Ground");
       // wallTileMap = GameManager.instance.GetTileMap("Wall");
       // topWallTileMap = GameManager.instance.GetTileMap("Top");

        tilePositionList = new List<Vector3>();
        Tilemap groundTileMap = GameManager.instance.GetCurrentRoom().GetComponent<Room>().GetTileMap(TileTypes.Ground);

        for (int x = GameManager.instance.GetCurrentRoom().GetComponent<Room>().roomBounds.Item1.x; x < GameManager.instance.GetCurrentRoom().GetComponent<Room>().roomBounds.Item1.y; x++)     //Loopar igenom alla tiles i bredd
        {
            for (int y = GameManager.instance.GetCurrentRoom().GetComponent<Room>().roomBounds.Item2.x; y < GameManager.instance.GetCurrentRoom().GetComponent<Room>().roomBounds.Item2.y; y++)     //Loopar igenom alla tiles i höjd
            {
                Vector3Int localPlace = new Vector3Int(x, y, 0);    //Tiles local-position
                Vector3 place = groundTileMap.CellToWorld(new Vector3Int(localPlace.x, localPlace.y, 0));   //Tilens world-position

                place.x += groundTileMap.cellSize.x / 2;    //Centrerar spawn-postionen i bredd
                place.y += groundTileMap.cellSize.y / 2;    //Centrerar spawn-positionen i höjd

                //Kontrollerar så att det finns en golv-tile och ingen vägg- eller top-tile
                if (GameManager.instance.GetCurrentRoom().GetComponent<Room>().CheckOnlyGroundTile(localPlace))
                {
                    tilePositionList.Add(place);    //Lägger till tilen i listan över tillåtna positioner att skapas på
                }
            }
        }

    }

    /// <summary>
    /// Kallas varje frame
    /// </summary>
    private void Update()
    {
        UpdateSpawnCooldown();
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
        List<int> tempIndexList = new List<int>();
        List<GameObject> spawnIndicatorList = new List<GameObject>();

        for (int i = 0; i < nrOfEnemiesToSpawn; i++)
        {
            tempIndexList.Add(rnd.Next(0, tilePositionList.Count));
            spawnIndicatorList.Add(Instantiate(spawnIndicator, tilePositionList[tempIndexList[i]], Quaternion.identity));
        }

        yield return new WaitForSeconds(delay);

        for (int i = 0; i < nrOfEnemiesToSpawn; i++)
        {
            Instantiate(enemy, tilePositionList[tempIndexList[i]], Quaternion.identity);
            Destroy(spawnIndicatorList[i]);
        }

        spawnIndicatorList.Clear();
        tempIndexList.Clear();
    }
}