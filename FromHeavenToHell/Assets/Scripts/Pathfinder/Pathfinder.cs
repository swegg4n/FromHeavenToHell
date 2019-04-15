using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private NodeGrid nodeGrid;  //Klass som håller information om alla noder på banan

    private Node startNode;     //Närmsta noden till starten (objektet som har pathfindern)
    private Node targetNode;    //Närmsta noden till målet

    public List<Node> FinalPath { get; private set; }       //Den närmsta vägen från start till mål


    private void Awake()
    {
        nodeGrid = GameManager.instance.gameObject.GetComponent<NodeGrid>();
    }

    /// <summary>
    /// Hittar nästa, bästa nod att röra sig till för alla noder som leder till målet
    /// </summary>
    public void FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        startNode = nodeGrid.GetNodeFromWorldPoint(startPosition);
        targetNode = nodeGrid.GetNodeFromWorldPoint(targetPosition);

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];

            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].FCost < currentNode.FCost || openList[i].FCost == currentNode.FCost && openList[i].HCost < currentNode.HCost)
                {
                    currentNode = openList[i];
                }
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode)
            {
                GetFinalPath(startNode, targetNode);
            }

            foreach (Node neighborNode in nodeGrid.GetNeighboringNodes(currentNode))
            {
                if (neighborNode.IsWall == true || closedList.Contains(neighborNode) == true)
                {
                    continue;
                }

                int moveCost = currentNode.GCost + GetManhattenDistance(currentNode, neighborNode);     //FCost för granne

                if (moveCost < neighborNode.GCost || openList.Contains(neighborNode) == false)
                {
                    neighborNode.GCost = moveCost;
                    neighborNode.HCost = GetManhattenDistance(neighborNode, targetNode);
                    neighborNode.ParentNode = currentNode;

                    if (openList.Contains(neighborNode) == false)
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }

    }

    /// <summary>
    /// Går igenom alla bästa noder och lägger de i en lista som håller hela vägens noder
    /// </summary>
    private void GetFinalPath(Node startNode, Node endNode)
    {
        FinalPath = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            FinalPath.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }

        FinalPath.Reverse();
    }

    /// <summary>
    /// Avståndet i ett rutmönster där ingen ruta får mätas rakt över (inte fågelvägen)
    /// </summary>
    private int GetManhattenDistance(Node nodeA, Node nodeB)
    {
        int dX = Mathf.Abs(nodeA.IndexGridX - nodeB.IndexGridX);
        int dY = Mathf.Abs(nodeA.IndexGridY - nodeB.IndexGridY);

        return dX + dY;
    }

}