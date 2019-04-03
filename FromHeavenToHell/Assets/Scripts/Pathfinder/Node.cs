using UnityEngine;

public class Node : MonoBehaviour
{
    public int indexGridX;
    public int indexGridY;

    public bool isWall;

    public Vector3 worldPosition;

    public Node derivationNode;

    public int gCost;
    public int hCost;

    public int fCost { get { return gCost + hCost; } }



    public Node (int indexGridX, int indexGridY, bool isWall, Vector3 worldPosition)
    {
        this.indexGridX = indexGridX;
        this.indexGridY = indexGridY;
        this.isWall = isWall;
        this.worldPosition = worldPosition;
    }
}
