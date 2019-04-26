﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

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

    [SerializeField] private GameObject playerDemonPrefab;      //Prefab som ska användas som demon-spelare
    [SerializeField] private GameObject playerAngelPrefab;      //Prefab som ska användas som ängel-spelare

    public GameObject PlayerDemonInstance { get; private set; }     //Propery för att läsa och skriva till instansen av demon-spelaren
    public GameObject PlayerAngelInstance { get; private set; }     //Propery för att läsa och skriva till instansen av ängel-spelaren

    [SerializeField] private int health;    //Gemensamt liv för spelarna

    [SerializeField] private bool playerDemonUsingMouse;        //Indikerar om demon-spelaren ska styras med mus eller inte (kontroll)
    public bool PlayerDemonUsingMouse { get { return playerDemonUsingMouse; } }     //Property för att från andra klasser få vilket styrsätt som används för demonen
    [SerializeField] private bool playerAngelUsingMouse;    //Indikerar om ängel-spelaren ska styras med mus eller inte (kontroll)
    public bool PlayerAngelUsingMouse { get { return playerAngelUsingMouse; } }     //Property för att från andra klasser få vilket styrsätt som används för ängeln

    [SerializeField] private float teleportCooldown;
    private bool teleportCooldownReady;
    private float timeSinceLastTeleport;
    public bool PlayerDemonTeleport { get; set; }
    public bool PlayerAngelTeleport { get; set; }


    /// <summary>
    /// Kallas innan första uppdateringen
    /// </summary>
    private void Start()
    {
        PlayerDemonInstance = Instantiate(playerDemonPrefab);       //Skapar ett objekt prefaben som används för demonspelaren
        PlayerAngelInstance = Instantiate(playerAngelPrefab);       //Skapar ett objekt prefaben som används för ängelspelaren
        teleportCooldownReady = true;
    }

    public void TeleportPlayers(Vector3? position, GameObject roomToTeleportTo)
    {
        if(PlayerDemonTeleport == true && PlayerAngelTeleport == true && teleportCooldownReady == true
            && position != null)
        {
            PlayerAngelInstance.transform.position = (Vector3)position;
            PlayerDemonInstance.transform.position = (Vector3)position;

            GameManager.instance.CurrentRoom = roomToTeleportTo;

            EnemyManager.instance.ResetRoom();

            PlayerDemonTeleport = false;
            PlayerAngelTeleport = false;

            teleportCooldownReady = false;
            timeSinceLastTeleport = 0;
        }
    }

    /// <summary>
    /// Kallas varje frame
    /// </summary>
    private void Update()
    {
        DeathCheck();

        timeSinceLastTeleport += Time.deltaTime;

        if(timeSinceLastTeleport > teleportCooldown)
        {
            teleportCooldownReady = true;
        }
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
        }
    }

    /// <summary>
    /// Minskar spelarnas liv med så mycket skada någon spelare tog
    /// </summary>
    /// <param name="damage">Så mycket skada någon spelare tog</param>
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

}