using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileTypes { Ground, Wall, TopWall, Teleport }


public class Room : MonoBehaviour
{

    private Tilemap groundTileMap;
    private Tilemap wallTileMap;
    private Tilemap topTileMap;
    private Tilemap teleportTileMap;

    public GameObject aboveRoom { get; private set; }
    public GameObject rightRoom { get; private set; }
    public GameObject leftRoom { get; private set; }
    public GameObject belowRoom { get; private set; }

    private List<Vector2> teleportPosList;

    public Tuple<Vector2Int,Vector2Int> roomBounds { get; private set; }


    void Awake()
    {
        Tilemap[] tileMapList = GetComponentsInChildren<Tilemap>();
        foreach (Tilemap t in tileMapList)
        {
            if (t.tag == "Ground")
            {
                groundTileMap = t;
            }
            else if (t.tag == "Wall")
            {
                wallTileMap = t;
            }
            else if (t.tag == "TopWall")
            {
                topTileMap = t;
            }
            else if (t.tag == "Teleport")
            {
                teleportTileMap = t;
            }
        }

        roomBounds = CalculateBoundsXY();


        //teleportPosList = new List<Vector2>();

        //for (int x = groundTileMap.cellBounds.xMin; x < groundTileMap.cellBounds.xMax; x++)
        //{
        //    for (int y = groundTileMap.cellBounds.yMin; y < groundTileMap.cellBounds.yMax; y++)
        //    {
        //        Vector3Int localPlace = new Vector3Int(x, y, (int)groundTileMap.transform.position.y);
        //        Vector3 place = groundTileMap.CellToWorld(new Vector3Int(localPlace.x, localPlace.y, localPlace.z));
        //        place.x += groundTileMap.cellSize.x / 2;
        //        if (teleportTileMap.HasTile(localPlace) == true)
        //        {
        //            teleportPosList.Add(place);
        //        }
        //    }

        //}

        aboveRoom = CheckSorrundingRoom(Vector2.up);
        belowRoom = CheckSorrundingRoom(Vector2.down);
        rightRoom = CheckSorrundingRoom(Vector2.right);
        leftRoom = CheckSorrundingRoom(Vector2.left);
    }

    private GameObject CheckSorrundingRoom(Vector2 direction)
    {
        RaycastHit2D[] rayCheck = Physics2D.RaycastAll(transform.position, direction);

        foreach (RaycastHit2D hit in rayCheck)
        {
            if (!wallTileMap.HasTile(Vector3Int.FloorToInt(hit.point)))
            {
                return hit.rigidbody.gameObject.transform.parent.gameObject;
            }
        }

        return null;
    }


    private Tuple<Vector2Int,Vector2Int> CalculateBoundsXY()
    {
        return new Tuple<Vector2Int, Vector2Int>
            (new Vector2Int(groundTileMap.cellBounds.xMin, groundTileMap.cellBounds.xMax),
            new Vector2Int(groundTileMap.cellBounds.yMin, groundTileMap.cellBounds.yMax));
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

    public bool CheckOnlyGroundTile(Vector3 targetPosition)
    {
        if (CheckWallTileAtPosition(Vector3Int.FloorToInt(targetPosition)) == false
            && CheckTopTileAtPosition(Vector3Int.FloorToInt(targetPosition)) == false
            && CheckGroundTileAtPosition(Vector3Int.FloorToInt(targetPosition)) == true)
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

