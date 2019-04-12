using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeGrid : MonoBehaviour
{
    private float cellSize;

    [SerializeField] private Tilemap groundTileMap;
    [SerializeField] private Tilemap wallsTileMap;

    public Node[,] NodeArray { get; private set; }

    private int gridSizeX;
    private int gridSizeY;


    private void Awake()
    {
        cellSize = groundTileMap.cellSize.x;
        gridSizeX = groundTileMap.size.x;
        gridSizeY = groundTileMap.size.y;
        CreateGrid();
    }

    /// <summary>
    /// Skapar noder för alla tiles i rummet, lägger de i en 2d array
    /// </summary>
    private void CreateGrid()
    {
        NodeArray = new Node[gridSizeX, gridSizeY];
        Vector3 firstTilePosition = new Vector3(-16f, -9f, 0);

        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                bool isWall = wallsTileMap.HasTile(new Vector3Int((int)(firstTilePosition.x + x * cellSize), (int)(firstTilePosition.y + y * cellSize), 0));
                
                NodeArray[x, y] = new Node(x, y, isWall, new Vector3(firstTilePosition.x + x * cellSize, firstTilePosition.y + y * cellSize, 0));
            }
        }
    }

    /// <summary>
    /// Får tag på alla noder brevid noden som skickas in
    /// </summary>
    public List<Node> GetNeighboringNodes(Node node)
    {
        List<Node> neighborList = new List<Node>();

        int indexCheckX;
        int indexCheckY;

        #region Check if neighbor nodes are within range of the 2D array
        //right
        indexCheckX = node.IndexGridX + 1;
        indexCheckY = node.IndexGridY;

        if (indexCheckX >= 0 && indexCheckX < gridSizeX)
        {
            if (indexCheckY >= 0 && indexCheckY < gridSizeY)
            {
                if (NodeArray[indexCheckX, indexCheckY].IsWall == false)
                {
                    neighborList.Add(NodeArray[indexCheckX, indexCheckY]);
                }
            }
        }

        //left
        indexCheckX = node.IndexGridX - 1;
        indexCheckY = node.IndexGridY;

        if (indexCheckX >= 0 && indexCheckX < gridSizeX)
        {
            if (indexCheckY >= 0 && indexCheckY < gridSizeY)
            {
                if (NodeArray[indexCheckX, indexCheckY].IsWall == false)
                {
                    neighborList.Add(NodeArray[indexCheckX, indexCheckY]);
                }
            }
        }

        //top
        indexCheckX = node.IndexGridX;
        indexCheckY = node.IndexGridY - 1;

        if (indexCheckX >= 0 && indexCheckX < gridSizeX)
        {
            if (indexCheckY >= 0 && indexCheckY < gridSizeY)
            {
                if (NodeArray[indexCheckX, indexCheckY].IsWall == false)
                {
                    neighborList.Add(NodeArray[indexCheckX, indexCheckY]);
                }
            }
        }

        //bottom
        indexCheckX = node.IndexGridX;
        indexCheckY = node.IndexGridY + 1;

        if (indexCheckX >= 0 && indexCheckX < gridSizeX)
        {
            if (indexCheckY >= 0 && indexCheckY < gridSizeY)
            {
                if (NodeArray[indexCheckX, indexCheckY].IsWall == false)
                {
                    neighborList.Add(NodeArray[indexCheckX, indexCheckY]);
                }
            }
        }
        #endregion

        return neighborList;
    }

    /// <summary>
    /// Returnerar noden som finns närmast den inskickade positionen
    /// </summary>
    public Node GetNodeFromWorldPoint(Vector3 worldPoint)
    {
        int xPos = Mathf.FloorToInt(worldPoint.x);
        int yPos = Mathf.FloorToInt(worldPoint.y);

        xPos = Mathf.Clamp(xPos, -16, 15);  //hardcode
        yPos = Mathf.Clamp(yPos, -9, 8);    //hardcode

        int xIndex = xPos + 16; //hardcode
        int yIndex = yPos + 9;  //hardcode

        return NodeArray[xIndex, yIndex];
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundTileMap.transform.position, new Vector3(gridSizeX * cellSize, gridSizeY * cellSize, 1));

        if (NodeArray != null)
        {
            foreach (Node node in NodeArray)
            {
                if (node.IsWall == true)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(new Vector3(node.WorldPosition.x + cellSize / 2, node.WorldPosition.y + cellSize / 2, 0), 0.25f);
                }
            }
        }
    }

}