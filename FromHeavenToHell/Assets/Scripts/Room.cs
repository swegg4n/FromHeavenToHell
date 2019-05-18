using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileTypes { Ground, Wall, TopWall, Teleport }


public class Room : MonoBehaviour
{
    [SerializeField] private Objective objective;
    public Objective Objective { get { return objective; } }

    private Tilemap groundTileMap;
    private Tilemap wallTileMap;
    private Tilemap topTileMap;
    private Tilemap teleportTileMap;

    public GameObject aboveRoom { get; private set; }
    public GameObject rightRoom { get; private set; }
    public GameObject leftRoom { get; private set; }
    public GameObject belowRoom { get; private set; }

    private List<Vector2> teleportPosList;

    public Tuple<Vector2Int, Vector2Int> RoomBounds { get; private set; }

    [SerializeField] private float timeBetweenSpawn;    //Tiden det tar mellan varje våg av fiender skapas. Mäts i sekunder
    public float TimeBetweenSpawn { get { return timeBetweenSpawn; } }
    [SerializeField] private float delay;       //Tiden det tar från att platsen en fiende ska skapas på visas till att fienden skapas. Mäts i sekunder
    public float Delay { get { return delay; } }
    [SerializeField] private int nrOfEnemiesToSpawn;    //Antal fiender som ska skapas i varje våg
    public int NrOfEnemiesToSpawn { get { return nrOfEnemiesToSpawn; } }


    void Awake()
    {
        teleportPosList = new List<Vector2>();

        Tilemap[] tileMapList = GetComponentsInChildren<Tilemap>();

        foreach (Tilemap t in tileMapList)
        {
            if (t.tag == "Ground")
            {
                groundTileMap = t;
                t.GetComponent<TilemapRenderer>().receiveShadows = true;
            }
            else if (t.tag == "Wall")
            {
                wallTileMap = t;
                t.GetComponent<TilemapRenderer>().receiveShadows = false;
            }
            else if (t.tag == "TopWall")
            {
                topTileMap = t;
                t.GetComponent<TilemapRenderer>().receiveShadows = false;
            }
            else if (t.tag == "Teleport")
            {
                teleportTileMap = t;
                t.GetComponent<TilemapRenderer>().receiveShadows = true;
            }
        }

        RoomBounds = CalculateBoundsXY();

        StartCoroutine(SetUpRoomRelations());
    }
    private void Start()
    {
        if (Objective.IsBossObjective == true)
        {
            EnemyManager.instance.BossObjectives.Add(Objective);
        }

        Create3DWalls();
    }

    private IEnumerator SetUpRoomRelations()
    {
        yield return new WaitForSecondsRealtime(1);

        for (int x = teleportTileMap.cellBounds.xMin; x < teleportTileMap.cellBounds.xMax; x++)
        {
            for (int y = teleportTileMap.cellBounds.yMin; y < teleportTileMap.cellBounds.yMax; y++)
            {
                Vector3Int localPlace = new Vector3Int(x, y, 0);

                Vector3 place = teleportTileMap.CellToWorld(new Vector3Int(localPlace.x, localPlace.y, localPlace.z));

                place.x += teleportTileMap.cellSize.x / 2;

                if (teleportTileMap.HasTile(localPlace) == true)
                {
                    teleportPosList.Add(place);
                }
            }
        }

        aboveRoom = CheckSorrundingRoom(Vector2.up);
        belowRoom = CheckSorrundingRoom(Vector2.down);
        rightRoom = CheckSorrundingRoom(Vector2.right);
        leftRoom = CheckSorrundingRoom(Vector2.left);
    }

    private void Create3DWalls()
    {
        int gridSizeX = GetRoomSize().x;
        int gridSizeY = GetRoomSize().y;

        Vector3Int firstTilePosition = new Vector3Int(RoomBounds.Item1.x, RoomBounds.Item2.x, 0);

        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                bool isWall = CheckWallTileAtPosition(new Vector3(firstTilePosition.x + x, firstTilePosition.y + y, 0));

                if (isWall == true)
                {
                    Vector3 position = new Vector3(firstTilePosition.x + x + transform.position.x + 0.5f, firstTilePosition.y + y + transform.position.y + 0.5f, -0.5f);

                    //Instantiate(GameManager.instance.ShadowCube, position, Quaternion.identity);
                    GameObject shadowCube = Instantiate(GameManager.instance.ShadowCube, transform);
                    shadowCube.transform.position = position;
                }
            }
        }
    }

    private GameObject CheckSorrundingRoom(Vector2 direction)
    {
        Vector3 rayCastOffset = Vector3.zero;

        if (direction == Vector2.up || direction == Vector2.down)
        {
            rayCastOffset = new Vector3(0, GetRoomSize().y * direction.y, 0);
        }
        else if (direction == Vector2.left || direction == Vector2.right)
        {
            rayCastOffset = new Vector3(GetRoomSize().x * direction.x, 0, 0);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position + rayCastOffset, direction, 1000);

        if (hit == true)
        {
            try
            {
                return hit.transform.parent.gameObject;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }


    public Tuple<Vector2Int, Vector2Int> CalculateBoundsXY()    //<(worldMinX, worldMaxX), (worldMinY, worldMaxY)>
    {
        Vector2 localMinXY = new Vector2(groundTileMap.localBounds.min.x, groundTileMap.localBounds.min.y);
        Vector2 localMaxXY = new Vector2(groundTileMap.localBounds.max.x, groundTileMap.localBounds.max.y);

        Vector2Int worldMinXY = new Vector2Int((int)localMinXY.x, (int)localMinXY.y);
        Vector2Int worldMaxXY = new Vector2Int((int)localMaxXY.x, (int)localMaxXY.y);

        return new Tuple<Vector2Int, Vector2Int>
            (new Vector2Int(worldMinXY.x, worldMaxXY.x), new Vector2Int(worldMinXY.y, worldMaxXY.y));
    }

    public Vector2Int GetRoomSize()
    {
        int roomSizeX = RoomBounds.Item1.y - RoomBounds.Item1.x;
        int roomSizeY = RoomBounds.Item2.y - RoomBounds.Item2.x;

        return new Vector2Int(roomSizeX, roomSizeY);
    }

    public bool CheckGroundTileAtPosition(Vector3 position)
    {
        if (groundTileMap.HasTile(Vector3Int.FloorToInt(position)) == true)
        {
            return true;
        }
        return false;
    }
    public bool CheckWallTileAtPosition(Vector3 position)
    {
        if (wallTileMap.HasTile(Vector3Int.FloorToInt(position)) == true)
        {
            return true;
        }
        return false;
    }
    public bool CheckTopTileAtPosition(Vector3 position)
    {
        if (topTileMap.HasTile(Vector3Int.FloorToInt(position)) == true)
        {
            return true;
        }
        return false;
    }


    //test till teleporten
    public bool CheckTeleportAtPosition(Vector3 position)
    {
        if (teleportTileMap.HasTile(Vector3Int.FloorToInt(position)) == true)
        {
            return true;
        }
        return false;
    }
    //1

    public Tilemap GetTileMap(TileTypes tileType)
    {
        switch (tileType)
        {
            case TileTypes.Ground:
                return groundTileMap;

            case TileTypes.Wall:
                return wallTileMap;

            case TileTypes.TopWall:
                return topTileMap;

            case TileTypes.Teleport:
                return teleportTileMap;

            default:
                return null;
        }
    }

    public Vector2 CheckTeleportInDirecction(Vector2 direction)
    {
        foreach (Vector2 tpPos in teleportPosList)
        {
            Vector2 normalizedDirection = (tpPos - (Vector2)transform.position).normalized;
            float koeficient = normalizedDirection.y / normalizedDirection.x;

            if ((koeficient < 1 && koeficient > -1) && normalizedDirection.x > 0 && direction == Vector2.left)
            {
                return tpPos;
            }
            else if ((koeficient < 1 && koeficient > -1) && normalizedDirection.x < 0 && direction == Vector2.right)
            {
                return tpPos;
            }
            else if ((koeficient > 1 || koeficient < -1) && normalizedDirection.y > 0 && direction == Vector2.down)
            {
                return tpPos;
            }
            else if ((koeficient > 1 || koeficient < -1) && normalizedDirection.y < 0 && direction == Vector2.up)
            {
                return tpPos;
            }
        }

        return Vector2.zero;
    }

    public bool CheckOnlyGroundTile(Vector3 targetPosition)
    {
        if (CheckWallTileAtPosition(targetPosition) == false
            && CheckTopTileAtPosition(targetPosition) == false
            && CheckGroundTileAtPosition(targetPosition) == true)
        {
            return true;
        }
        return false;
    }

    public bool CheckOnlyGroundTileWorldToCell(Vector3 targetPosition)
    {
        if (wallTileMap.HasTile(wallTileMap.WorldToCell(targetPosition)) == false
            && topTileMap.HasTile(wallTileMap.WorldToCell(targetPosition)) == false
            && groundTileMap.HasTile(wallTileMap.WorldToCell(targetPosition)) == true)
        {
            return true;
        }
        return false;
    }

    public List<Vector2> GetTeleportPos()
    {
        return teleportPosList;
    }


}

