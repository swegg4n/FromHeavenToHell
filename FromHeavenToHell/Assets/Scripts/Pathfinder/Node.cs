using UnityEngine;

public class Node
{
    public int indexGridX;
    public int indexGridY;

    public bool isWall;

    public Vector3 worldPosition;

    public int hCost { get; set; }


    public Node(int indexGridX, int indexGridY, bool isWall, Vector3 worldPosition)
    {
        this.indexGridX = indexGridX;
        this.indexGridY = indexGridY;
        this.isWall = isWall;
        this.worldPosition = worldPosition;
    }
}
