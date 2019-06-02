using UnityEngine;

public class Node
{
    public int IndexGridX { get; }  //x-index för noden i arrayen som håller alla banans noder
    public int IndexGridY { get; }  //y-index för noden i arrayen som håller alla banans noder

    public bool IsWall { get; }     //Ifall noden är vid en vägg eller inte (om det går att gå till noden)

    public Vector3 WorldPosition { get; }   //Positionen i världen för noden

    public Node ParentNode { get; set; }    //Noden .this kommer i från ()

    public int GCost { get; set; }  //Kostnad för att röra sig, beror på avstånd från start
    public int HCost { get; set; }  //Kostnad för att röra sig, beror på avstånd till mål

    public int FCost { get { return GCost + HCost; } }      //Totala kostnaden för att röra sig till noden (läge = bättre)


    public Node(int indexGridX, int indexGridY, bool isWall, Vector3 worldPosition)
    {
        IndexGridX = indexGridX;
        IndexGridY = indexGridY;
        IsWall = isWall;
        WorldPosition = worldPosition;
    }
}
