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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentRoomTeleportPosList = GetComponentInParent<Room>().GetTeleportPos();

        if (collision.gameObject.tag == "PlayerAngel" || collision.gameObject.tag == "PlayerDemon")
        {
            if(collision.gameObject.tag == "PlayerAngel")
            {
                PlayerManager.instance.PlayerAngelTeleport = true;
            }
            else if(collision.gameObject.tag == "PlayerDemon")
            {
                PlayerManager.instance.PlayerDemonTeleport = true;
            }

            Vector2 playerAngelPosition = PlayerManager.instance.playerAngelInstance.transform.position;
            Vector2 playerDemonPosition = PlayerManager.instance.playerDemonInstance.transform.position;

            //Vector2 positionTeleportTo = currentRoomTeleportPosList[0];

            //foreach (Vector2 telePos in currentRoomTeleportPosList)
            //{
            //    if (Vector2.Distance(playerAngelPosition, telePos) > GameManager.instance.tileSize
            //        && Vector2.Distance(playerAngelPosition, positionTeleportTo) > Vector2.Distance(playerAngelPosition, telePos))
            //    {
            //        positionTeleportTo = telePos;
            //    }
            //}


            PlayerManager.instance.TeleportPlayers(positionTeleportTo);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAngel")
        {
            PlayerManager.instance.PlayerAngelTeleport = false;
        }
        else if(collision.gameObject.tag == "PlayerDemon")
        {
            PlayerManager.instance.PlayerDemonTeleport = false;
        }
    }


    private void CheckTeleporter(Collision2D collision)
    {
        foreach(Vector2 tpPos in currentRoomTeleportPosList)
        {
            if (collision.otherCollider.OverlapPoint(tpPos))
            {
                Vector2 normalizedDirection = ((Vector2)transform.parent.transform.position - tpPos).normalized;
                float koeficient = normalizedDirection.y / normalizedDirection.x;

                if ((koeficient < 1 && koeficient > -1) && normalizedDirection.x > 0)
                {
                    //Teleporta till höger
                }
                else if ((koeficient < 1 && koeficient > -1) && normalizedDirection.x < 0)
                {
                    //Teleporta till vänster
                }
                else if ((koeficient > 1 || koeficient < -1) && normalizedDirection.y > 0)
                {
                    //Teleporta upp
                }
                else if ((koeficient > 1 || koeficient < -1) && normalizedDirection.y < 0)
                {
                    //Teleporta ner
                }

                //Vector2 rightTeleporter = currentRoomTeleportPosList.OrderBy(pos => pos.x).ToArray()[0];
                //Vector2 leftTeleporter = currentRoomTeleportPosList.OrderByDescending(pos => pos.x).ToArray()[0];

                //Vector2 topTelleporter = currentRoomTeleportPosList.OrderBy(pos => pos.y).ToArray()[0];
                //Vector2 bottomTeleporter = currentRoomTeleportPosList.OrderByDescending(pos => pos.y).ToArray()[0];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
