using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    //[SerializeField] private Tilemap groundTileMap;
    //[SerializeField] private Tilemap wallTileMap;
    //[SerializeField] private Tilemap topTileMap;

    [SerializeField] private GameObject currentRoom;

    public int tileSize { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        tileSize = 32; //Vem hårdkodade detta? fyi inte Jonathan eller Oscar
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCurrentRoom()
    {

    }

    public GameObject GetCurrentRoom()
    {
        return currentRoom;
    }

    public bool CheckGroundTileAtPosition(Vector3 position)
    {
        //if (groundTileMap.HasTile(Vector3Int.FloorToInt(position)) == true)
        //{
        //    return true;
        //}
        //return false;
        return currentRoom.GetComponent<Room>().CheckGroundTileAtPosition(position);
    }
    public bool CheckWallTileAtPosition(Vector3 position)
    {
        //if (wallTileMap.HasTile(Vector3Int.FloorToInt(position)) == true)
        //{
        //    return true;
        //}
        //return false;
        return currentRoom.GetComponent<Room>().CheckWallTileAtPosition(position);
    }
    public bool CheckTopTileAtPosition(Vector3 position)
    {
        //if (topTileMap.HasTile(Vector3Int.FloorToInt(position)) == true)
        //{
        //    return true;
        //}
        //return false;
        return currentRoom.GetComponent<Room>().CheckTopTileAtPosition(position);
    }
    public Tilemap GetTileMap(TileTypes tileType)
    {
        //if(tileMapName == "Ground")
        //{
        //    return groundTileMap;
        //}else if(tileMapName == "Wall")
        //{
        //    return wallTileMap;
        //}else if(tileMapName == "Top")
        //{
        //    return topTileMap;
        //}
        //else
        //{
        //    return null;
        //}
        return currentRoom.GetComponent<Room>().GetTileMap(tileType);
    }
    public bool CheckOnlyGroundTile(Vector3 targetPosition)
    {
        //if (CheckWallTileAtPosition(Vector3Int.FloorToInt(targetPosition)) == false
        //    && CheckTopTileAtPosition(Vector3Int.FloorToInt(targetPosition)) == false
        //    && CheckGroundTileAtPosition(Vector3Int.FloorToInt(targetPosition)) == true)
        //{
        //    return true;
        //}
        //return false;
        return currentRoom.GetComponent<Room>().CheckOnlyGroundTile(targetPosition);
    }
}
