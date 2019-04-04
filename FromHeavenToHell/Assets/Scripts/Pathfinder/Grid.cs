using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid : MonoBehaviour
{
    //public Transform startPosition;
    public LayerMask wallMask;

    public float tileSize;
    public Tilemap groundTileMap;
    public Tilemap wallsTileMap;

    public Node[,] nodeArray;

    private int gridSizeX;
    private int gridSizeY;


    private void Awake()
    {
        tileSize = groundTileMap.cellSize.x;
        gridSizeX = groundTileMap.size.x;
        gridSizeY = groundTileMap.size.y;
        CreateGrid();
    }

    private void CreateGrid()
    {
        nodeArray = new Node[gridSizeX, gridSizeY];
        Vector3 firstTilePosition = new Vector3(-16f, -9f, 0);

        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                bool isWall = wallsTileMap.HasTile(new Vector3Int((int)(firstTilePosition.x + x * tileSize), (int)(firstTilePosition.y + y * tileSize), 0));
                
                nodeArray[x, y] = new Node(x, y, isWall, new Vector3(firstTilePosition.x + x * tileSize, firstTilePosition.y + y * tileSize, 0));
            }
        }
    }

    public List<Node> GetNeighboringNodes(Node node)
    {
        List<Node> neighborList = new List<Node>();

        int indexCheckX;
        int indexCheckY;

        #region Check if neighbor nodes are within range of the 2D array
        //right
        indexCheckX = node.indexGridX + 1;
        indexCheckY = node.indexGridY;

        if (indexCheckX >= 0 && indexCheckX < gridSizeX)
        {
            if (indexCheckY >= 0 && indexCheckY < gridSizeY)
            {
                if (nodeArray[indexCheckX, indexCheckY].isWall == false)
                {
                    neighborList.Add(nodeArray[indexCheckX, indexCheckY]);
                }
            }
        }

        //left
        indexCheckX = node.indexGridX - 1;
        indexCheckY = node.indexGridY;

        if (indexCheckX >= 0 && indexCheckX < gridSizeX)
        {
            if (indexCheckY >= 0 && indexCheckY < gridSizeY)
            {
                if (nodeArray[indexCheckX, indexCheckY].isWall == false)
                {
                    neighborList.Add(nodeArray[indexCheckX, indexCheckY]);
                }
            }
        }

        //top
        indexCheckX = node.indexGridX;
        indexCheckY = node.indexGridY - 1;

        if (indexCheckX >= 0 && indexCheckX < gridSizeX)
        {
            if (indexCheckY >= 0 && indexCheckY < gridSizeY)
            {
                if (nodeArray[indexCheckX, indexCheckY].isWall == false)
                {
                    neighborList.Add(nodeArray[indexCheckX, indexCheckY]);
                }
            }
        }

        //bottom
        indexCheckX = node.indexGridX;
        indexCheckY = node.indexGridY + 1;

        if (indexCheckX >= 0 && indexCheckX < gridSizeX)
        {
            if (indexCheckY >= 0 && indexCheckY < gridSizeY)
            {
                if (nodeArray[indexCheckX, indexCheckY].isWall == false)
                {
                    neighborList.Add(nodeArray[indexCheckX, indexCheckY]);
                }
            }
        }
        #endregion

        return neighborList;
    }

    public Node GetNodeFromWorldPoint(Vector3 worldPoint)
    {
        int xPos = Mathf.RoundToInt(worldPoint.x);
        int yPos = Mathf.RoundToInt(worldPoint.y);

        xPos = Mathf.Clamp(xPos, -16, 15);  //hardcode
        yPos = Mathf.Clamp(yPos, -9, 8);    //hardcode

        int xIndex = xPos + 16; //hardcode
        int yIndex = yPos + 9;  //hardcode

        return nodeArray[xIndex, yIndex];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundTileMap.transform.position, new Vector3(gridSizeX * tileSize, gridSizeY * tileSize, 1));

        if (nodeArray != null)
        {
            foreach (Node node in nodeArray)
            {
                if (node.isWall == true)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }

                Gizmos.DrawWireCube(new Vector3(node.worldPosition.x + tileSize / 2, node.worldPosition.y + tileSize / 2, 0), Vector3.one * tileSize / 3);
            }
        }
    }

}