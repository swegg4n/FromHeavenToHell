using System.Collections.Generic;
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

        List<Node> openList = new List<Node>();     //Lista med alla noder som inte sökts igenom
        HashSet<Node> closedList = new HashSet<Node>();     //HashSet med alla noder som redan har sökts igenom. 
                                                            //Används för att inte söka igenom samma nod flera gånger
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

            if (currentNode == targetNode)      //om målet är nått
            {
                GetFinalPath(startNode, targetNode);
            }

            //går igenom alla grann-noder för att se vad det kostar att röra sig till de
            foreach (Node neighborNode in nodeGrid.GetNeighboringNodes(currentNode))
            {
                //om noden är en vägg eller om den redan sökts igenom, hoppa över den (=går inte att gå till den)
                if (neighborNode.IsWall == true || closedList.Contains(neighborNode) == true)
                {
                    continue;
                }

                //räknar ut fCost för noden med (gcost + hCost)
                int moveCost = currentNode.GCost + GetManhattenDistance(currentNode, neighborNode);

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

        //går igenom den slutgiltiga vägen baklänges och lägger till de i en lista (target -> start)
        while (currentNode != startNode)
        {
            FinalPath.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }

        FinalPath.Reverse();    //Vänder på listan för att få noderna i rätt ordning (start -> target)
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


    private void OnDrawGizmos()
    {
        try
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(new Vector3(startNode.WorldPosition.x + 0.5f, startNode.WorldPosition.y + 0.5f, startNode.WorldPosition.z), new Vector3(0.5f, 0.5f, 0.5f));

            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(targetNode.WorldPosition.x + 0.5f, targetNode.WorldPosition.y + 0.5f, targetNode.WorldPosition.z), new Vector3(0.5f, 0.5f, 0.5f));
        }
        catch (System.Exception)
        {
        }
    }

}