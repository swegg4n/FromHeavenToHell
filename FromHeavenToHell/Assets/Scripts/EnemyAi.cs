using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    Vector2 EnemyPos;

    Vector2 playerOnePos;
    Vector2 playerTwoPos;

    Vector2 enemyDirection;
    Vector2 playerOneDirection;
    Vector2 playerTwoDirection;

    float playerOneDistance;
    float playerTwoDistance;

    Rigidbody2D rigidBody;

    GameObject playerOne;
    GameObject playerTwo;
    //GameObject enemy;
    GameObject[] enemiesArray;

    private void Awake()
    {
        //rigidBody = GetComponent<Rigidbody2D>();

        //rigidBody.mass = 0.1f;
        //rigidBody.drag = 1000f;

        //enemy = GameObject.Find("CorruptedBunnyDog");
        //EnemyPos = new Vector2(enemy.transform.position.x, enemy.transform.position.y);

        //playerOne = PlayerManager.instance.player1;
        //playerTwo = PlayerManager.instance.player2;
        //Debug.Log("error is" + playerOne);


        //enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");

            playerOne = PlayerManager.instance.player1;
            playerTwo = PlayerManager.instance.player2;

            rigidBody = GetComponent<Rigidbody2D>();

            rigidBody.mass = 0.1f;
            rigidBody.drag = 1000f;
        
        //foreach( )
        //{
        //    enemyList = GameObject.Find()
        //    enemyList.Add(enemy)
        //}

    }
    

    void FixedUpdate()
    {


        //playerOnePos = new Vector2(playerOne.transform.position.x, playerOne.transform.position.y);
        //playerTwoPos = new Vector2(playerTwo.transform.position.x, playerTwo.transform.position.y);
        ////EnemyPos = new Vector2(transform.position.x, transform.position.y);


        //playerOneDirection = playerOnePos - EnemyPos;
        //playerTwoDirection = playerTwoPos - EnemyPos;

        //playerOneDistance = playerOneDirection.magnitude;
        //playerTwoDistance = playerTwoDirection.magnitude;

            EnemyPos = new Vector2(transform.position.x, transform.position.y);

            playerOnePos = new Vector2(playerOne.transform.position.x, playerOne.transform.position.y);
            playerTwoPos = new Vector2(playerTwo.transform.position.x, playerTwo.transform.position.y);

            playerOneDirection = playerOnePos - EnemyPos;
            playerTwoDirection = playerTwoPos - EnemyPos;

            playerOneDistance = playerOneDirection.magnitude;
            playerTwoDistance = playerTwoDirection.magnitude;


            if (playerOneDistance <= playerTwoDistance)
            {
                enemyDirection = playerOneDirection;
            }
            else
            {
                enemyDirection = playerTwoDirection;
            }

            rigidBody.velocity = enemyDirection * 7f;
        

        //if (playerOneDistance <= playerTwoDistance)
        //{
        //    enemyDirection = playerOneDirection;
        //}
        //else
        //{
        //    enemyDirection = playerTwoDirection;
        //}

        //rigidBody.velocity = enemyDirection * 7f;

        //foreach( GameObject in enemyList)


    }
    //void Update()
    //{
    //    Debug.Log("direction is" + enemyDirection);
    //}
}
