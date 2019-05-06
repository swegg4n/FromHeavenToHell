using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager instance;


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

    //[SerializeField] private Tilemap groundTileMap;
    //[SerializeField] private Tilemap wallTileMap;
    //[SerializeField] private Tilemap topTileMap;

    [SerializeField] private GameObject currentRoom;
    public GameObject CurrentRoom { set { currentRoom = value; } get { return currentRoom; } } 

    public bool gameLost { set; get; }
    public bool gameWon { set; get; }

    public int tileSize { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        tileSize = 32; //Vem hårdkodade detta? fyi inte Jonathan eller Oscar
    }

    // Update is called once per frame
    void Update()
    {
        if(gameLost == true)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
        else if(gameWon == true)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
    }
}
