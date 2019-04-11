using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{

    private Tilemap groundTileMap;
    private Tilemap wallTileMap;
    private Tilemap topTileMap;
    private Tilemap teleportTileMap;

    private Room aboveRoom;
    private Room rightRoom;
    private Room leftRoom;
    private Room bottomRoom;

    List<Vector3> teleportPosList;

    void Start()
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

        teleportPosList = new List<Vector3>();

        for (int x = groundTileMap.cellBounds.xMin; x < groundTileMap.cellBounds.xMax; x++)
        {
            for (int y = groundTileMap.cellBounds.yMin; y < groundTileMap.cellBounds.yMax; y++)
            {
                Vector3Int localPlace = new Vector3Int(x, y, (int)groundTileMap.transform.position.y);
                Vector3 place = groundTileMap.CellToWorld(new Vector3Int(localPlace.x, localPlace.y, localPlace.z));
                place.x += groundTileMap.cellSize.x / 2;
                if (teleportTileMap.HasTile(localPlace) == true)
                {
                    teleportPosList.Add(place);
                }
            }

        }
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

    public Tilemap GetTileMap(string tileMapName)
    {
        if (tileMapName == "Ground")
        {
            return groundTileMap;
        }
        else if (tileMapName == "Wall")
        {
            return wallTileMap;
        }
        else if (tileMapName == "Top")
        {
            return topTileMap;
        }
        //test till teleporten 
        else if (tileMapName == "Teleport")
        {
            return teleportTileMap;
        }
        else
        {
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



    public List<Vector3> GetTeleportPos()
    {
        return teleportPosList;
    }


}

