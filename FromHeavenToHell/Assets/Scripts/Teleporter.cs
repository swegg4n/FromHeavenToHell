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

        if (collider.tag == GameManager.objectsTags[GameManager.Objects.PlayerDemon])
        {
            PlayerManager.instance.PlayerDemonCanTeleport = true;   //Sätter att demonen kan teleportera
        }
        else if (collider.tag == GameManager.objectsTags[GameManager.Objects.PlayerAngel])
        {
            PlayerManager.instance.PlayerAngelCanTeleport = true;   //Sätter att ängeln kan teleportera
        }

        Teleport(collider);
    }

    /// <summary>
    /// Sätter att spelaren som går ut ur denna collider INTE kan teleportera
    /// </summary>
    /// <param name="collider">Collidern på objektet som går ut ur teleport-tilen</param>
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == GameManager.objectsTags[GameManager.Objects.PlayerDemon])
        {
            PlayerManager.instance.PlayerDemonCanTeleport = false;   //Sätter att demonen kan teleportera
        }
        else if (collider.tag == GameManager.objectsTags[GameManager.Objects.PlayerAngel])
        {
            PlayerManager.instance.PlayerAngelCanTeleport = false;   //Sätter att ängeln kan teleportera
        }
    }

    /// <summary>
    /// Checks if players can teleport, calculates where players should be teleported and initiates a teleport
    /// </summary>
    public void Teleport(Collider2D collider)
    {
        ///Om båda spelarna kan teleportera och de befinner sig på samma teleport-tiles
        if (PlayerManager.instance.PlayerDemonCanTeleport == true && PlayerManager.instance.PlayerAngelCanTeleport == true &&
            Vector2.Distance(PlayerManager.instance.PlayerDemonInstance.transform.position, PlayerManager.instance.PlayerAngelInstance.transform.position) <= 3)
        {
            Vector2? positionTeleportTo = CheckTeleporter(collider);
            PlayerManager.instance.TeleportPlayers(positionTeleportTo, roomToTeleportTo);
        }
    }

    private Vector2? CheckTeleporter(Collider2D collider)
    {
        //Debug.Log("6" + collider.bounds.center + "Collider" + collider.bounds);

        Vector2 normalizedDirection = (collider.bounds.center - transform.parent.transform.localPosition).normalized;
        float koeficient = normalizedDirection.y / normalizedDirection.x;

        //Debug.Log("4" + koeficient);

        if ((koeficient < 1 && koeficient > -1) && normalizedDirection.x > 0)
        {
            // Debug.Log("A1");

            roomToTeleportTo = GetComponentInParent<Room>().RightRoom;

            if (GetComponentInParent<Room>().RightRoom != null)
            {
                return GetComponentInParent<Room>().RightRoom.GetComponent<Room>().CheckTeleportInDirecction(Vector2.right);
            }
        }
        else if ((koeficient < 1 && koeficient > -1) && normalizedDirection.x < 0)
        {
            // Debug.Log("A2");

            roomToTeleportTo = GetComponentInParent<Room>().LeftRoom;

            if (GetComponentInParent<Room>().LeftRoom != null)
            {
                return GetComponentInParent<Room>().LeftRoom.GetComponent<Room>().CheckTeleportInDirecction(Vector2.left);
            }
        }
        else if ((koeficient > 1 || koeficient < -1) && normalizedDirection.y > 0)
        {
            //Debug.Log("A3");

            roomToTeleportTo = GetComponentInParent<Room>().AboveRoom;

            if (GetComponentInParent<Room>().AboveRoom != null)
            {
                return GetComponentInParent<Room>().AboveRoom.GetComponent<Room>().CheckTeleportInDirecction(Vector2.up);
            }
        }
        else if ((koeficient > 1 || koeficient < -1) && normalizedDirection.y < 0)
        {
            // Debug.Log("A4");

            roomToTeleportTo = GetComponentInParent<Room>().BelowRoom;

            if (GetComponentInParent<Room>().BelowRoom != null)
            {
                return GetComponentInParent<Room>().BelowRoom.GetComponent<Room>().CheckTeleportInDirecction(Vector2.down);
            }
        }

        // Debug.Log("5");
        return null;
    }

}