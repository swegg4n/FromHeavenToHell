using System.Collections;
using System.Collections.Generic;
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


    [SerializeField] private GameObject playerDemon;
    [SerializeField] private GameObject playerAngel;

    public bool playerDemonUsingMouseAndKeyboard { get; private set; }
    public bool playerAngelUsingMouseAndKeyboard { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(playerAngel);
        Instantiate(playerDemon);
        playerAngelUsingMouseAndKeyboard = true;
        playerDemonUsingMouseAndKeyboard = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
