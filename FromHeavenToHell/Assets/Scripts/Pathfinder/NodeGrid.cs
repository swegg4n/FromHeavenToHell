using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    public Node[,] NodeArray { get; private set; }  //2D-array som håller alla banans noder

    private float cellSize;

    private int gridSizeX;  //Banans bredd (antal noder i bredd)
    private int gridSizeY;  //Banans höjd (antal noder i höjd)


    private void Awake()
    {
        CreateGrid();
    }

    /// <summary>
    /// Skapar noder för alla tiles i rummet, lägger de i en 2d array
    /// </summary>
    public void CreateGrid()
    {
        cellSize = 1;
        gridSizeX = GameManager.instance.CurrentRoom.GetComponent<Room>().GetRoomSize().x;
        gridSizeY = GameManager.instance.CurrentRoom.GetComponent<Room>().GetRoomSize().y;


        NodeArray = new Node[gridSizeX, gridSizeY];
        Vector3Int firstTilePosition = new Vector3Int(
            GameManager.instance.CurrentRoom.GetComponent<Room>().roomBounds.Item1.x,
            GameManager.instance.CurrentRoom.GetComponent<Room>().roomBounds.Item2.x, 0);

        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                bool isWall = GameManager.instance.CurrentRoom.GetComponent<Room>().CheckWallTileAtPosition(new Vector3(firstTilePosition.x + x * cellSize, firstTilePosition.y + y * cellSize, 0));

                NodeArray[x, y] = new Node(x, y, isWall, new Vector3(firstTilePosition.x + x * cellSize + GameManager.instance.CurrentRoom.transform.position.x
                    , firstTilePosition.y + y * cellSize + GameManager.instance.CurrentRoom.transform.position.y, 0));
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
        int xPos = Mathf.FloorToInt(worldPoint.x - (int)GameManager.instance.CurrentRoom.transform.position.x);
        int yPos = Mathf.FloorToInt(worldPoint.y - (int)GameManager.instance.CurrentRoom.transform.position.y);

        xPos = Mathf.Clamp(xPos, GameManager.instance.CurrentRoom.GetComponent<Room>().roomBounds.Item1.x,
            GameManager.instance.CurrentRoom.GetComponent<Room>().roomBounds.Item1.y - 1);

        yPos = Mathf.Clamp(yPos, GameManager.instance.CurrentRoom.GetComponent<Room>().roomBounds.Item2.x,
            GameManager.instance.CurrentRoom.GetComponent<Room>().roomBounds.Item2.y - 1);


        int xIndex = xPos + GameManager.instance.CurrentRoom.GetComponent<Room>().roomBounds.Item1.y;
        int yIndex = yPos + GameManager.instance.CurrentRoom.GetComponent<Room>().roomBounds.Item2.y;

        return NodeArray[xIndex, yIndex];
    }


    private void OnDrawGizmos() //THROW AWAY CODE
    {
        try
        {
            foreach (var node in NodeArray)
            {
                if (node.IsWall == true)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.green;

                }
                Gizmos.DrawWireCube(new Vector3(node.WorldPosition.x + 0.5f, node.WorldPosition.y + 0.5f, node.WorldPosition.z), new Vector3(0.5f, 0.5f, 0.5f));
            }
        }
        catch (System.Exception)
        {

        }
    }

}