using System.Collections;
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

    [SerializeField] private GameObject playerDemonPrefab;
    [SerializeField] private GameObject playerAngelPrefab;

    public GameObject playerDemonInstance { get; private set; }
    public GameObject playerAngelInstance { get; private set; }

    [SerializeField] private int health;

    [SerializeField] private bool playerDemonUsingMouse;
    [SerializeField] private bool playerAngelUsingMouse;

    // Start is called before the first frame update
    void Start()
    {
        playerAngelInstance = Instantiate(playerAngelPrefab);
        playerDemonInstance = Instantiate(playerDemonPrefab);

        playerAngelUsingMouseAndKeyboard = true;
        playerDemonUsingMouseAndKeyboard = false;
    }

    // Update is called once per frame
    void Update()
    {
        DeathCheck();
    }

    void DeathCheck()
    {
        if (health <= 0)
        {
            Destroy(currentPlayerDemon);
            Destroy(currentPlayerAngel);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public bool GetAngelUsingMouse()
    {
        return playerAngelUsingMouse;
    }

    public bool GetDemonUsingMouse()
    {
        return playerDemonUsingMouse;
    }
}
