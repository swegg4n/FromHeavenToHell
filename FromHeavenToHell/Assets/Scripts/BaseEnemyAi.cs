using UnityEngine;

public class BaseEnemyAi : MonoBehaviour
{
    private Vector2 direction;
    private Vector2 closestPlayerDirection;

    private GameObject playerOne;
    private GameObject playerTwo;

    private float raycastRange = 100f;
    [SerializeField] private LayerMask layerMask;



    void Start()
    {
        playerOne = PlayerManager.instance.player1;
        playerTwo = PlayerManager.instance.player2;

        GetComponent<Rigidbody2D>().mass = 0.1f;
        GetComponent<Rigidbody2D>().drag = 1000f;
    }


    void FixedUpdate()
    {
        SetClosestPlayerDirection();


        RaycastHit2D hit = Physics2D.Raycast(transform.position, closestPlayerDirection, raycastRange, layerMask);

        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            Debug.Log("path blocked");
        }
        else
        {
            direction = closestPlayerDirection;
        }


        GetComponent<Rigidbody2D>().velocity = direction.normalized * GetComponent<EnemyBaseClass>().SpeedFactor;
    }


    private void SetClosestPlayerDirection()
    {
        if (Vector2.Distance(playerOne.GetComponent<Transform>().position, GetComponent<Transform>().position) <=
            Vector2.Distance(playerTwo.GetComponent<Transform>().position, GetComponent<Transform>().position))
        {
            closestPlayerDirection = new Vector2(playerOne.GetComponent<Transform>().position.x - transform.position.x,
                playerOne.GetComponent<Transform>().position.y - transform.position.y);

        }
        else
        {
            closestPlayerDirection = new Vector2(playerTwo.GetComponent<Transform>().position.x - transform.position.x,
                playerTwo.GetComponent<Transform>().position.y - transform.position.y);
        }
    }

}