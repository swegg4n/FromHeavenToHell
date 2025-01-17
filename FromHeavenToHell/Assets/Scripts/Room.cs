﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileTypes { Ground, Wall, TopWall, Teleport }
public enum RoomTypes { StartRoom, HeavenRoom, HellRoom, BossRoom }

public class Room : MonoBehaviour
{
    [SerializeField] private Objective objective;       //Rummtes objektiv
    public Objective Objective { get { return objective; } }

    public bool IsStartRoom { get; private set; }
    public bool IsHeavenRoom { get; private set; }
    public bool IsHellRoom { get; private set; }
    public bool IsBossRoom { get; private set; }

    private Tilemap groundTileMap;
    private Tilemap wallTileMap;
    private Tilemap topTileMap;
    private Tilemap teleportTileMap;

    public GameObject AboveRoom { get; private set; }
    public GameObject RightRoom { get; private set; }
    public GameObject LeftRoom { get; private set; }
    public GameObject BelowRoom { get; private set; }

    private List<Vector2> teleportPosList;

    public Tuple<Vector2Int, Vector2Int> RoomBounds { get; private set; }

    [SerializeField] private float timeBetweenSpawn;    //Tiden det tar mellan varje våg av fiender skapas. Mäts i sekunder
    public float TimeBetweenSpawn { get { return timeBetweenSpawn; } }
    [SerializeField] private float delay;       //Tiden det tar från att platsen en fiende ska skapas på visas till att fienden skapas. Mäts i sekunder
    public float Delay { get { return delay; } }
    [SerializeField] private int nrOfEnemiesToSpawn;    //Antal fiender som ska skapas i varje våg
    public int NrOfEnemiesToSpawn { get { return nrOfEnemiesToSpawn; } }


    /// <summary>
    /// Sätter upp properties om rummets tiles
    /// </summary>
    void Awake()
    {
        teleportPosList = new List<Vector2>();

        Tilemap[] tileMapList = GetComponentsInChildren<Tilemap>();

        foreach (Tilemap t in tileMapList)
        {
            if (t.tag == TileTypes.Ground.ToString())
            {
                groundTileMap = t;
                t.GetComponent<TilemapRenderer>().receiveShadows = true;
            }
            else if (t.tag == TileTypes.Wall.ToString())
            {
                wallTileMap = t;
                t.GetComponent<TilemapRenderer>().receiveShadows = false;
            }
            else if (t.tag == TileTypes.TopWall.ToString())
            {
                topTileMap = t;
                t.GetComponent<TilemapRenderer>().receiveShadows = false;
            }
            else if (t.tag == TileTypes.Teleport.ToString())
            {
                teleportTileMap = t;
                t.GetComponent<TilemapRenderer>().receiveShadows = true;
            }
        }
        RoomBounds = CalculateBoundsXY();
        StartCoroutine(SetUpRoomRelations());
    }

    /// <summary>
    /// Sätter upp properties knytna till rummet samt 3D väggar
    /// </summary>
    private void Start()
    {
        if (gameObject.tag == RoomTypes.StartRoom.ToString())
        {
            IsStartRoom = true;
        }
        else if (gameObject.tag == RoomTypes.HeavenRoom.ToString())
        {
            IsHeavenRoom = true;
        }
        else if (gameObject.tag == RoomTypes.HellRoom.ToString())
        {
            IsHellRoom = true;
        }
        else if (gameObject.tag == RoomTypes.BossRoom.ToString())
        {
            IsBossRoom = true;
        }

        if (Objective.IsBossObjective == true)
        {
            EnemyManager.instance.BossObjectives.Add(Objective);
        }

        Create3DWalls();
    }

    /// <summary>
    /// Sätter upp rummets anknytningar till grann-rummen
    /// </summary>
    /// <returns>Rummets grann-rum</returns>
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

        AboveRoom = CheckSorrundingRoom(Vector2.up);
        BelowRoom = CheckSorrundingRoom(Vector2.down);
        RightRoom = CheckSorrundingRoom(Vector2.right);
        LeftRoom = CheckSorrundingRoom(Vector2.left);
    }

    /// <summary>
    /// Skapar 3D väggar för att kasta skuggor i rummet
    /// </summary>
    private void Create3DWalls()
    {
        int gridSizeX = GetRoomSize().x;
        int gridSizeY = GetRoomSize().y;

        Vector3Int firstTilePosition = new Vector3Int(RoomBounds.Item1.x, RoomBounds.Item2.x, 0);

        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                bool isWall = CheckTileTypeAtPosition(TileTypes.Wall, new Vector3(firstTilePosition.x + x, firstTilePosition.y + y, 0));

                if (isWall == true)
                {
                    Vector3 position = new Vector3(firstTilePosition.x + x + transform.position.x + 0.5f, firstTilePosition.y + y + transform.position.y + 0.5f, -0.5f);

                    GameObject shadowCube = Instantiate(GameManager.instance.ShadowCube, transform);
                    shadowCube.transform.position = position;
                }
            }
        }
    }

    /// <summary>
    /// Får om det finns ett annat rum brevid rummet
    /// </summary>
    /// <returns>Om det finns ett rum brevid  och isf. vilket rum det är</returns>
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

    /// <summary>
    /// Räknar ut rummets storlek i  tiles
    /// </summary>
    /// <returns>(worldMinX, worldMaxX), (worldMinY, worldMaxY)</returns>
    public Tuple<Vector2Int, Vector2Int> CalculateBoundsXY()
    {
        Vector2 localMinXY = new Vector2(groundTileMap.localBounds.min.x, groundTileMap.localBounds.min.y);
        Vector2 localMaxXY = new Vector2(groundTileMap.localBounds.max.x, groundTileMap.localBounds.max.y);

        Vector2Int worldMinXY = new Vector2Int((int)localMinXY.x, (int)localMinXY.y);
        Vector2Int worldMaxXY = new Vector2Int((int)localMaxXY.x, (int)localMaxXY.y);

        return new Tuple<Vector2Int, Vector2Int>
            (new Vector2Int(worldMinXY.x, worldMaxXY.x), new Vector2Int(worldMinXY.y, worldMaxXY.y));
    }

    /// <summary>
    /// Får rummets storlek
    /// </summary>
    /// <returns></returns>
    public Vector2Int GetRoomSize()
    {
        int roomSizeX = RoomBounds.Item1.y - RoomBounds.Item1.x;
        int roomSizeY = RoomBounds.Item2.y - RoomBounds.Item2.x;

        return new Vector2Int(roomSizeX, roomSizeY);
    }


    /// <summary>
    /// Kontrollerar om det finns en tile med en tiletyp vid positionen
    /// </summary>
    /// <param name="tileType">Tiletypen som ska kontrolleras om den finns</param>
    /// <param name="position">Positionen för tilen som ska kontrollers</param>
    /// <returns>Om en tile av typen tileType vid positionen position finns eller inte </returns>
    public bool CheckTileTypeAtPosition(TileTypes tileType, Vector3 position)
    {
        switch (tileType)
        {
            case TileTypes.Ground:
                {
                    if (groundTileMap.HasTile(Vector3Int.FloorToInt(position)) == true)
                    {
                        return true;
                    }
                }
                break;

            case TileTypes.Wall:
                {
                    if (wallTileMap.HasTile(Vector3Int.FloorToInt(position)) == true)
                    {
                        return true;
                    }
                }
                break;

            case TileTypes.TopWall:
                {
                    if (topTileMap.HasTile(Vector3Int.FloorToInt(position)) == true)
                    {
                        return true;
                    }
                }
                break;

            case TileTypes.Teleport:
                {
                    if (teleportTileMap.HasTile(Vector3Int.FloorToInt(position)) == true)
                    {
                        return true;
                    }
                }
                break;
        }

        return false;
    }

    /// <summary>
    /// Får tilemap associerad med rummet av typen tileType
    /// </summary>
    /// <param name="tileType">Typen av tilemap som ska fås</param>
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

    /// <summary>
    /// Får teleport-position i riktningen som kollas
    /// </summary>
    /// <param name="direction">Riktningen som ska kollas</param>
    /// <returns>Teleport-positionen</returns>
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

    /// <summary>
    /// Ser om det endast finns en ground-tile vid positionen
    /// </summary>
    /// <param name="targetPosition">Positionen som ska kollas</param>
    /// <returns></returns>
    public bool CheckOnlyGroundTile(Vector3 targetPosition)
    {
        if (CheckTileTypeAtPosition(TileTypes.Wall, targetPosition) == false
            && CheckTileTypeAtPosition(TileTypes.TopWall, targetPosition) == false
            && CheckTileTypeAtPosition(TileTypes.Ground, targetPosition) == true)
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