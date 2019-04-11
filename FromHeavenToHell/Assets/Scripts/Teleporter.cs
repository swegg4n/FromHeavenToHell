using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Teleporter : MonoBehaviour
{
    List<Vector3> currentRoomTeleportPosList;

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
                
                
                    Debug.Log("Angel enter");
                    PlayerManager.instance.PlayerAngelTeleport = true;
                
            }
            else if (collision.gameObject.tag == "PlayerDemon")
            {
                Debug.Log("Demon enter");
                PlayerManager.instance.PlayerDemonTeleport = true;
            }
            Vector3 playerPosition = PlayerManager.instance.playerAngelInstance.transform.position;
            Vector3 positionTeleportTo = currentRoomTeleportPosList[0];

            foreach (Vector3 telePos in currentRoomTeleportPosList)
            {
                if (Vector3.Distance(playerPosition, telePos) > GameManager.instance.tileSize == true
                    && Vector3.Distance(playerPosition, positionTeleportTo) > Vector3.Distance(playerPosition, telePos) == true)
                {
                    positionTeleportTo = telePos;
                }
            }
            PlayerManager.instance.TeleportPlayers(positionTeleportTo);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAngel")
        {

            Debug.Log("Angel exit");
            PlayerManager.instance.PlayerAngelTeleport = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
       
    }
}
