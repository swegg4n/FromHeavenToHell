using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    public Node[,] NodeArray { get; private set; }  //2D-array som håller alla banans noder

    private float cellSize;

    private int gridSizeX;  //Banans bredd (antal noder i bredd)
    private int gridSizeY;  //Banans höjd (antal noder i höjd)


    private void Start()
    {
        cellSize = 1;
        gridSizeX = 32;
        gridSizeY = 18;

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
                bool isWall = GameManager.instance.GetCurrentRoom().GetComponent<Room>().CheckWallTileAtPosition(new Vector3(firstTilePosition.x + x * cellSize, firstTilePosition.y + y * cellSize, 0));

                NodeArray[x, y] = new Node(x, y, isWall, new Vector3(firstTilePosition.x + x * cellSize, firstTilePosition.y + y * cellSize, 0));
            }
        }
    }

    /// <summary>
    /// Får tag på alla noder brevid noden som skickas in (upp, ner, höger, vänster)
    /// </summary>
    public List<Node> GetNeighboringNodes(Node node)
    {
        List<Node> neighborList = new List<Node>();

        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (Mathf.Abs(x) == Mathf.Abs(y))
                {
                    continue;
                }

                int indexCheckX = node.IndexGridX + x;
                int indexCheckY = node.IndexGridY + y;

                if (indexCheckX >= 0 && indexCheckX < gridSizeX && 
                    indexCheckY >= 0 && indexCheckY < gridSizeY)
                {
                    if (NodeArray[indexCheckX, indexCheckY].IsWall == false)
                    {
                        neighborList.Add(NodeArray[indexCheckX, indexCheckY]);
                    }
                }

            }
        }

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