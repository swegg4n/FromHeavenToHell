using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class Teleporter : MonoBehaviour
{
    private List<Vector2> currentRoomTeleportPosList;

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
                Debug.Log("angel ready");

                PlayerManager.instance.PlayerAngelTeleport = !PlayerManager.instance.PlayerAngelTeleport;
            }

            else if (collider.gameObject.tag == "PlayerDemon")
            {
                Debug.Log("demon ready");

                PlayerManager.instance.PlayerDemonTeleport = !PlayerManager.instance.PlayerDemonTeleport;
            }

            Vector2 playerAngelPosition = PlayerManager.instance.playerAngelInstance.transform.position;
            Vector2 playerDemonPosition = PlayerManager.instance.playerDemonInstance.transform.position;

            Vector2 positionTeleportTo = CheckTeleporter(collider);

            PlayerManager.instance.TeleportPlayers(positionTeleportTo);

            //Debug.Log(GetComponentInParent<Room>().rightRoom);
            //Debug.Log(GetComponentInParent<Room>().rightRoom.gameObject);
            //Debug.Log(GetComponentInParent<Room>().rightRoom.gameObject.transform);
            //Debug.Log(GetComponentInParent<Room>().rightRoom.gameObject.transform.position);



            //Debug.Log("test");

            //if (GetComponent<TilemapCollider2D>().IsTouching(PlayerManager.instance.playerAngelInstance.GetComponent<Collider2D>()))
            //{
            //    Debug.Log("tp");
            //    Vector2 positionTeleportTo = CheckTeleporter(collider);

            //    PlayerManager.instance.TeleportPlayers(positionTeleportTo);
            //}
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
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

    private Vector2 CheckTeleporter(Collider2D collider)
    {
        foreach(Vector2 tpPos in currentRoomTeleportPosList)
        {
            Debug.Log("6");

            //IF SATSEN HÄNDER INTE!!!
            if (collider.OverlapPoint(tpPos))
            {
                Vector2 normalizedDirection = (tpPos - (Vector2)transform.parent.transform.position).normalized;
                float koeficient = normalizedDirection.y / normalizedDirection.x;

                Debug.Log("4");
                    
                if ((koeficient < 1 && koeficient > -1) && normalizedDirection.x > 0)
                {
                    Debug.Log("1");
                    return GetComponentInParent<Room>().rightRoom.GetComponent<Room>().CheckTeleportInDirecction(Vector2.right);
                }
                else if ((koeficient < 1 && koeficient > -1) && normalizedDirection.x < 0)
                {
                    Debug.Log("1");
                    return GetComponentInParent<Room>().leftRoom.GetComponent<Room>().CheckTeleportInDirecction(Vector2.left);
                }
                else if ((koeficient > 1 || koeficient < -1) && normalizedDirection.y > 0)
                {
                    Debug.Log("1");
                    return GetComponentInParent<Room>().aboveRoom.GetComponent<Room>().CheckTeleportInDirecction(Vector2.up);
                }
                else if ((koeficient > 1 || koeficient < -1) && normalizedDirection.y < 0)
                {
                    Debug.Log("1");
                    return GetComponentInParent<Room>().belowRoom.GetComponent<Room>().CheckTeleportInDirecction(Vector2.down);
                }
            }
        }
        Debug.Log("5");
        return Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
