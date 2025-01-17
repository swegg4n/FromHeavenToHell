﻿using Assets.Classes;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region singleton
    public static PlayerManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] private Camera playersCamera;      //Kameran som visar rummet spelarna är i

    [SerializeField] private GameObject playerDemonPrefab;      //Prefab som ska användas som demon-spelare
    [SerializeField] private GameObject playerAngelPrefab;      //Prefab som ska användas som ängel-spelare

    public GameObject PlayerDemonInstance { get; private set; }     //Propery för att läsa och skriva till instansen av demon-spelaren
    public GameObject PlayerAngelInstance { get; private set; }     //Propery för att läsa och skriva till instansen av ängel-spelaren

    [SerializeField] private int health;    //Gemensamt liv för spelarna
    public int Health { get { return health; } set { health = value; } }
    public int MaxHealth { get; private set; }

    public bool PlayerDemonCanTeleport { get; set; }    //Håller reda på om demonen står på en teleport-tile eller inte
    public bool PlayerAngelCanTeleport { get; set; }    //Håller reda på om ängeln står på en teleport-tile eller inte


    /// <summary>
    /// Kallas innan första uppdateringen
    /// </summary>
    private void Start()
    {
        MaxHealth = health;

        PlayerDemonInstance = Instantiate(playerDemonPrefab);       //Skapar ett objekt prefaben som används för demonspelaren
        PlayerAngelInstance = Instantiate(playerAngelPrefab);       //Skapar ett objekt prefaben som används för ängelspelaren

        StartCoroutine(GameSetup());
    }


    private IEnumerator GameSetup()
    {
        yield return new WaitForEndOfFrame();

        PlayerDemonInstance.transform.position = GameManager.instance.CurrentRoom.transform.localPosition;     //Flyttar demonen till nya positionen
        PlayerAngelInstance.transform.position = GameManager.instance.CurrentRoom.transform.localPosition;     //Flyttar ängeln till nya positionen
        //Flyttar kameran till nya rummet
        playersCamera.transform.position = new Vector3(GameManager.instance.CurrentRoom.transform.localPosition.x,
            GameManager.instance.CurrentRoom.transform.localPosition.y, playersCamera.transform.localPosition.z);

        EnemyManager.instance.ResetRoom();  //Tar bort alla fienders spawn-positioner och räknar ut de nya spawn-positionerna för rummet

        GameManager.instance.CurrentRoom.GetComponent<Room>().CalculateBoundsXY();  //Räknar ut hur stort rummet är
        GetComponent<NodeGrid>().CreateGrid();  //Skapar ett nytt rutnät av noder som används av A* (pathfinder)

        GetComponent<ObjectiveController>().StartObjective();
    }

    /// <summary>
    /// Teleporterar spelarna till ett rum
    /// </summary>
    /// <param name="position">Den uträknade positionen spelarna ska teleporteras till</param>
    /// <param name="roomToTeleportTo">Rummet som spelarna ska befinna sig i</param>
    public void TeleportPlayers(Vector3? position, GameObject roomToTeleportTo)
    {
        ///Kontrollerar så att objektivet är avklarat och spelarna har en position att teleportera till
        if (GameManager.instance.GetComponent<ObjectiveController>().ObjectiveCompleted == true && position != null)
        {
            GameManager.instance.CurrentRoom = roomToTeleportTo;    //Sätter det aktiva rummet till det nya rummet

            EnemyManager.instance.ResetRoom();  //Tar bort alla fienders spawn-positioner och räknar ut de nya spawn-positionerna för rummet

            GameManager.instance.CurrentRoom.GetComponent<Room>().CalculateBoundsXY();  //Räknar ut hur stort rummet är
            GetComponent<NodeGrid>().CreateGrid();   //Skapar ett nytt rutnät av noder som används av A* (pathfinder)

            PlayerDemonInstance.transform.position = (Vector3)position;     //Flyttar demonen till nya positionen
            PlayerAngelInstance.transform.position = (Vector3)position;     //Flyttar ängeln till nya positionen

            //Flyttar kameran till nya rummet
            playersCamera.transform.position = new Vector3(GameManager.instance.CurrentRoom.transform.position.x,
                GameManager.instance.CurrentRoom.transform.position.y, playersCamera.transform.position.z);

            GetComponent<ObjectiveController>().StartObjective();   //Startar objektivet
        }
    }

    public void Heal(int heal)
    {
        health += heal;
    }


    /// <summary>
    /// Kontrollerar om spelarnas liv är mindre eller lika med 0.
    /// </summary>
    private void DeathCheck()
    {
        if (health <= 0)
        {
            Destroy(PlayerDemonInstance);
            Destroy(PlayerAngelInstance);

            GameManager.instance.GameLost = true;
        }
    }

    /// <summary>
    /// Minskar spelarnas liv med så mycket skada någon spelare tog
    /// </summary>
    /// <param name="damage">Så mycket skada någon spelare tog</param>
    public void TakeDamage(int damage, GameObject player, GameObject caster)
    {
        health -= damage;

        if (PlayerAngelInstance.CompareTag(player.tag) == true)
        {
            if (caster != null)
            {
                if (PlayerAngelInstance.CompareTag(caster.tag) == true)
                {
                    StatTracker.AngelSelfDamage += damage;
                }
                else if (PlayerDemonInstance.CompareTag(caster.tag) == true)
                {
                    StatTracker.DemonDamageDealtToAngel += damage;
                }
            }

            StatTracker.AngelDamageTaken += damage;
        }
        else if (PlayerDemonInstance.CompareTag(player.tag) == true)
        {
            if (caster != null)
            {
                if (PlayerDemonInstance.CompareTag(caster.tag) == true)
                {
                    StatTracker.DemonSelfDamage += damage;
                }
                else if (PlayerAngelInstance.CompareTag(caster.tag) == true)
                {
                    StatTracker.AngelDamageDealtToDemon += damage;
                }
            }
            StatTracker.DemonDamageTaken += damage;
        }

        DeathCheck();
    }

}