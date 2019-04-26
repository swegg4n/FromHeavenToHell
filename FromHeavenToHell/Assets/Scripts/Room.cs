﻿using System;
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

    public GameObject aboveRoom { get; private set; }
    public GameObject rightRoom { get; private set; }
    public GameObject leftRoom { get; private set; }
    public GameObject belowRoom { get; private set; }

    private List<Vector2> teleportPosList;

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

        teleportPosList = new List<Vector2>();

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

    private GameObject CheckSorrundingRoom(Vector2 direction)
    {
        RaycastHit2D[] rayCheck = Physics2D.RaycastAll(transform.position, direction);
        
        foreach (RaycastHit2D hit in rayCheck)
        {
            if (wallTileMap.HasTile(wallTileMap.WorldToCell(hit.point) + Vector3Int.up) == false 
                && wallTileMap.HasTile(wallTileMap.WorldToCell(hit.point) + Vector3Int.down) == false
                && teleportTileMap.HasTile(teleportTileMap.WorldToCell(hit.point) + Vector3Int.up) == false
                && teleportTileMap.HasTile(teleportTileMap.WorldToCell(hit.point) + Vector3Int.down) == false
                && groundTileMap.HasTile(teleportTileMap.WorldToCell(hit.point)) == false
                && groundTileMap.HasTile(teleportTileMap.WorldToCell(hit.point)) == false)
            {
                return hit.rigidbody.gameObject.transform.parent.gameObject;
            }
        }

        return null;
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

    public List<Vector2> GetTeleportPos()
    {
        return teleportPosList;
    }


}

