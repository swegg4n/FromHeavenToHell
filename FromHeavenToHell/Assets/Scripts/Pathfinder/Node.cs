using UnityEngine;

public class Node
{
    public int IndexGridX { get; }
    public int IndexGridY { get; }

    public bool IsWall { get; }

    public Vector3 WorldPosition { get; }

    public Node ParentNode { get; set; }

    public int GCost { get; set; }
    public int HCost { get; set; }

    public int FCost { get { return GCost + HCost; } }      //Totala kostnaden för att röra sig till noden (läge = bättre)


    public Node(int indexGridX, int indexGridY, bool isWall, Vector3 worldPosition)
    {
        IndexGridX = indexGridX;
        IndexGridY = indexGridY;
        IsWall = isWall;
        WorldPosition = worldPosition;
    }
}
