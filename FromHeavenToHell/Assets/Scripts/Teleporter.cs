using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class Teleporter : MonoBehaviour
{
    private List<Vector2> currentRoomTeleportPosList;
    private GameObject roomToTeleportTo;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        currentRoomTeleportPosList = GetComponentInParent<Room>().GetTeleportPos();

        if (collider.gameObject.tag == "PlayerAngel" || collider.gameObject.tag == "PlayerDemon")
        {
            if (collider.gameObject.tag == "PlayerAngel")
            {
                Debug.Log("angel in");

                PlayerManager.instance.PlayerAngelTeleport = !PlayerManager.instance.PlayerAngelTeleport;
            }

            else if (collider.gameObject.tag == "PlayerDemon")
            {
                Debug.Log("demon in");

                PlayerManager.instance.PlayerDemonTeleport = !PlayerManager.instance.PlayerDemonTeleport;
            }

            Vector2 playerAngelPosition = PlayerManager.instance.PlayerAngelInstance.transform.position;
            Vector2 playerDemonPosition = PlayerManager.instance.PlayerDemonInstance.transform.position;

            Vector2? positionTeleportTo = CheckTeleporter(collider);

            Debug.Log(positionTeleportTo);

            PlayerManager.instance.TeleportPlayers(positionTeleportTo, roomToTeleportTo);
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("exit");

    //    if (collision.gameObject.tag == "PlayerAngel")
    //    {
    //        Debug.Log("angel exit");

    //        PlayerManager.instance.PlayerAngelTeleport = false;
    //    }
    //    else if(collision.gameObject.tag == "PlayerDemon")
    //    {
    //        Debug.Log("demon exit");

    //        PlayerManager.instance.PlayerDemonTeleport = false;
    //    }
    //}

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

            if(GetComponentInParent<Room>().rightRoom != null)
            {
                return GetComponentInParent<Room>().rightRoom.GetComponent<Room>().CheckTeleportInDirecction(Vector2.right);
            }
        }
        else if ((koeficient < 1 && koeficient > -1) && normalizedDirection.x < 0)
        {
            Debug.Log("A2");

            roomToTeleportTo = GetComponentInParent<Room>().leftRoom;

            if(GetComponentInParent<Room>().leftRoom != null)
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

    // Update is called once per frame
    void Update()
    {
       
    }
}
