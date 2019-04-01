using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyAi : MonoBehaviour
{
    // Start is called before the first frame update

    float velocityX;
    float velocityY;

    Vector2 direction;

    GameObject playerOne;
    GameObject playerTwo;


    void Start()
    {
        playerOne = PlayerManager.instance.player1;
        playerTwo = PlayerManager.instance.player2;

        GetComponent<Rigidbody2D>().mass = 0.1f;
        GetComponent<Rigidbody2D>().drag = 1000f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Vector2.Distance(playerOne.GetComponent<Transform>().position, GetComponent<Transform>().position) <=
            Vector2.Distance(playerTwo.GetComponent<Transform>().position, GetComponent<Transform>().position))
        {
            velocityX = playerOne.GetComponent<Transform>().position.x - GetComponent<Transform>().position.x;
            velocityY = playerOne.GetComponent<Transform>().position.y - GetComponent<Transform>().position.y;
        }
        else
        {
            velocityX = playerTwo.GetComponent<Transform>().position.x - GetComponent<Transform>().position.x;
            velocityY = playerTwo.GetComponent<Transform>().position.y - GetComponent<Transform>().position.y;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(velocityX,velocityY).normalized * GetComponent<EnemyBaseClass>().SpeedFactor;
    }
}
