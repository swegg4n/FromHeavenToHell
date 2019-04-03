using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid : MonoBehaviour
{
    public Transform startPosition;
    public LayerMask wallMask;

    public float tileSize;
    public Tilemap groundTileMap;
    public Tilemap wallsTileMap;

    public Node[,] nodeArray;
    public List<Node> finalPath;

    private int gridSizeX;
    private int gridSizeY;



    private void Start()
    {
        tileSize = groundTileMap.cellSize.x;
        gridSizeX = groundTileMap.size.x;
        gridSizeY = groundTileMap.size.y;
        CreateGrid();
    }


    private void CreateGrid()
    {
        nodeArray = new Node[gridSizeX, gridSizeY];
        Vector3 firstTilePosition = new Vector3(-15.5f, -8.5f, 0);

        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                bool isWall = wallsTileMap.HasTile(new Vector3Int((int)(firstTilePosition.x + x * tileSize - 0.5f), (int)(firstTilePosition.y + y * tileSize - 0.5f), 0));

                nodeArray[x, y] = new Node(x, y, isWall, new Vector3(firstTilePosition.x + x * tileSize, firstTilePosition.y + y * tileSize, 0));
            }
        }
    }


    public List<Node> GetNeighboringNodes(Node neighborNode)
    {
        List<Node> neighborList = new List<Node>();

        int indexCheckX;
        int indexCheckY;

        #region Check if neighbor nodes are within range of the 2D array
        //right
        indexCheckX = neighborNode.indexGridX + 1;
        indexCheckY = neighborNode.indexGridY;

        if (indexCheckX >= 0 && indexCheckX < gridSizeX)
        {
            if (indexCheckY >= 0 && indexCheckY < gridSizeY)
            {
                neighborList.Add(nodeArray[indexCheckX, indexCheckY]);
            }
        }

        //left
        indexCheckX = neighborNode.indexGridX - 1;
        indexCheckY = neighborNode.indexGridY;

        if (indexCheckX >= 0 && indexCheckX < gridSizeX)
        {
            if (indexCheckY >= 0 && indexCheckY < gridSizeY)
            {
                neighborList.Add(nodeArray[indexCheckX, indexCheckY]);
            }
        }

        //top
        indexCheckX = neighborNode.indexGridX;
        indexCheckY = neighborNode.indexGridY - 1;

        if (indexCheckX >= 0 && indexCheckX < gridSizeX)
        {
            if (indexCheckY >= 0 && indexCheckY < gridSizeY)
            {
                neighborList.Add(nodeArray[indexCheckX, indexCheckY]);
            }
        }

        //bottom
        indexCheckX = neighborNode.indexGridX;
        indexCheckY = neighborNode.indexGridY + 1;

        if (indexCheckX >= 0 && indexCheckX < gridSizeX)
        {
            if (indexCheckY >= 0 && indexCheckY < gridSizeY)
            {
                neighborList.Add(nodeArray[indexCheckX, indexCheckY]);
            }
        }
        #endregion

        return neighborList;
    }

    
    public Node GetNodeFromWorldPoint(Vector3 worldPoint)
    {
        int xPos = Mathf.RoundToInt(worldPoint.x);
        int yPos = Mathf.RoundToInt(worldPoint.y);

        int xIndex = groundTileMap.WorldToCell(new Vector3Int(xPos, yPos, 0)).x;
        int yIndex = groundTileMap.WorldToCell(new Vector3Int(xPos, yPos, 0)).y;


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

                if (finalPath != null)
                {
                    if (finalPath.Contains(node))
                    {
                        Gizmos.color = Color.blue;
                    }
                }

                Gizmos.DrawCube(node.worldPosition, Vector3.one * tileSize / 3);    
            }
        }
    }

}