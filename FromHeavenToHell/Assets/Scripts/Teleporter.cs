using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private List<Vector2> currentRoomTeleportPosList;
    private GameObject roomToTeleportTo;


    /// <summary>
    /// Sätter att spelaren som går in i denna collider kan teleportera
    /// </summary>
    /// <param name="collider">Collidern på objektet som går in i teleport-tilen</param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        currentRoomTeleportPosList = GetComponentInParent<Room>().GetTeleportPos();

        switch (collider.transform.tag)
        {
            case "PlayerDemon":     //Om spelaren är demonen
                {
                    PlayerManager.instance.PlayerDemonCanTeleport = true;   //Sätter att demonen kan teleportera
                    Debug.Log("Demon In");
                }
                break;

            case "PlayerAngel":     //Om spelaren är ängeln
                {
                    PlayerManager.instance.PlayerAngelCanTeleport = true;   //Sätter att ängeln kan teleportera
                    Debug.Log("Angel In");
                }
                break;
        }

        //Om båda spelarna kan teleportera och de befinner sig på samma teleport-tiles
        if (PlayerManager.instance.PlayerDemonCanTeleport == true && PlayerManager.instance.PlayerAngelCanTeleport == true &&
            Vector2.Distance(PlayerManager.instance.PlayerDemonInstance.transform.position, PlayerManager.instance.PlayerAngelInstance.transform.position) <= 3)
        {
            Vector2? positionTeleportTo = CheckTeleporter(collider);
            PlayerManager.instance.TeleportPlayers(positionTeleportTo, roomToTeleportTo);
        }
    }

    /// <summary>
    /// Sätter att spelaren som går ut ur denna collider INTE kan teleportera
    /// </summary>
    /// <param name="collider">Collidern på objektet som går ut ur teleport-tilen</param>
    private void OnTriggerExit2D(Collider2D collider)
    {
        switch (collider.transform.tag)
        {
            case "PlayerDemon":     //Om spelaren är demonen
                {
                    PlayerManager.instance.PlayerDemonCanTeleport = false;      //Sätter att demonen INTE kan teleportera
                    Debug.Log("Demon Out");
                }
                break;

            case "PlayerAngel":     //Om spelaren är ängeln
                {
                    PlayerManager.instance.PlayerAngelCanTeleport = false;      //Sätter att ängeln INTE kan teleportera
                    Debug.Log("Angel Out");
                }
                break;
        }
    }

    private Vector2? CheckTeleporter(Collider2D collider)
    {
        Debug.Log("6" + collider.bounds.center + "Collider" + collider.bounds);

        Vector2 normalizedDirection = (collider.bounds.center - transform.parent.transform.position).normalized;
        float koeficient = normalizedDirection.y / normalizedDirection.x;

        Debug.Log("4" + koeficient);

        if ((koeficient < 1 && koeficient > -1) && normalizedDirection.x > 0)
        {
            Debug.Log("A1");

            roomToTeleportTo = GetComponentInParent<Room>().rightRoom;

            if (GetComponentInParent<Room>().rightRoom != null)
            {
                return GetComponentInParent<Room>().rightRoom.GetComponent<Room>().CheckTeleportInDirecction(Vector2.right);
            }
        }
        else if ((koeficient < 1 && koeficient > -1) && normalizedDirection.x < 0)
        {
            Debug.Log("A2");

            roomToTeleportTo = GetComponentInParent<Room>().leftRoom;

            if (GetComponentInParent<Room>().leftRoom != null)
            {
                return GetComponentInParent<Room>().leftRoom.GetComponent<Room>().CheckTeleportInDirecction(Vector2.left);
            }
        }
        else if ((koeficient > 1 || koeficient < -1) && normalizedDirection.y > 0)
        {
            Debug.Log("A3");

            roomToTeleportTo = GetComponentInParent<Room>().aboveRoom;

            if (GetComponentInParent<Room>().aboveRoom != null)
            {
                return GetComponentInParent<Room>().aboveRoom.GetComponent<Room>().CheckTeleportInDirecction(Vector2.up);
            }
        }
        else if ((koeficient > 1 || koeficient < -1) && normalizedDirection.y < 0)
        {
            Debug.Log("A4");

            roomToTeleportTo = GetComponentInParent<Room>().belowRoom;

            if (GetComponentInParent<Room>().belowRoom != null)
            {
                return GetComponentInParent<Room>().belowRoom.GetComponent<Room>().CheckTeleportInDirecction(Vector2.down);
            }
        }

        Debug.Log("5");
        return null;
    }

}