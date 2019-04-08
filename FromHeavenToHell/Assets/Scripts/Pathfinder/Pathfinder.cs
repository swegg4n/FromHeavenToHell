using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private Grid gridRef;

    private Node startNode;
    private Node targetNode;

    public List<Node> FinalPath { get; private set; }


    private void Start()
    {
        gridRef = PlayerManager.instance.gameObject.GetComponent<Grid>();
    }

    public void FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        startNode = gridRef.GetNodeFromWorldPoint(startPosition);
        targetNode = gridRef.GetNodeFromWorldPoint(targetPosition);

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

            foreach (Node neighborNode in gridRef.GetNeighboringNodes(currentNode))
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

    private int GetManhattenDistance(Node nodeA, Node nodeB)
    {
        int dX = Mathf.Abs(nodeA.IndexGridX - nodeB.IndexGridX);
        int dY = Mathf.Abs(nodeA.IndexGridY - nodeB.IndexGridY);

        return dX + dY;
    }



    private void OnDrawGizmos()
    {
        if (startNode != null && targetNode != null && FinalPath != null) 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(new Vector3(startNode.WorldPosition.x + 0.5f, startNode.WorldPosition.y + 0.5f, 0), Vector3.one / 3);
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(targetNode.WorldPosition.x + 0.5f, targetNode.WorldPosition.y + 0.5f, 0), Vector3.one / 3);


            foreach (Node node in gridRef.NodeArray)
            {
                if (FinalPath.Contains(node) == true)
                {
                    if (node == startNode || node == targetNode)
                    {
                        continue;
                    }
                    Gizmos.color = Color.magenta;
                    Gizmos.DrawCube(new Vector3(node.WorldPosition.x + 0.5f, node.WorldPosition.y + 0.5f, 0), Vector3.one / 3);
                }
            }
        }
    }

}