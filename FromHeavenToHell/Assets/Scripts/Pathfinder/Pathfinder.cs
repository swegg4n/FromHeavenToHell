using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private Grid gridRef;

    public Transform startTransform;
    public Transform targetTransform;

    Node startNode;
    public Node targetNode;

    public Node chosenNode;


    private void Start()
    {
        gridRef = PlayerManager.instance.gameObject.GetComponent<Grid>();
    }


    public void FindNextNode(Vector3 startPosition, Vector3 targetPosition)
    {
        startNode = gridRef.GetNodeFromWorldPoint(startPosition);
        targetNode = gridRef.GetNodeFromWorldPoint(targetPosition);

        foreach (Node neighborNode in gridRef.GetNeighboringNodes(startNode))
        {
            if (neighborNode.isWall == true)
            {
                Debug.Log("wall");
                continue;
            }

            neighborNode.hCost = GetManhattenDistance(neighborNode, targetNode);
            Debug.Log(neighborNode.hCost);
        }

        int minHCost = gridRef.GetNeighboringNodes(startNode).Min(node => node.hCost);
        chosenNode = gridRef.GetNeighboringNodes(startNode).Find(node => node.hCost == minHCost);
        Debug.Log("min hCost " + minHCost);
    }


    private int GetManhattenDistance(Node nodeA, Node nodeB)
    {
        int dX = Mathf.Abs(nodeA.indexGridX - nodeB.indexGridX);
        int dY = Mathf.Abs(nodeA.indexGridY - nodeB.indexGridY);

        return dX + dY;
    }

    private void OnDrawGizmos()
    {
        if (startNode != null && targetNode != null && chosenNode != null) 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(new Vector3(startNode.worldPosition.x + 0.5f, startNode.worldPosition.y + 0.5f, 0), Vector3.one / 3);
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(targetNode.worldPosition.x + 0.5f, targetNode.worldPosition.y + 0.5f, 0), Vector3.one / 3);
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(new Vector3(chosenNode.worldPosition.x + 0.5f, chosenNode.worldPosition.y + 0.5f, 0), Vector3.one / 3);
        }
    }

}