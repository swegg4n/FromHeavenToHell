using UnityEngine;

public class BaseEnemyAi : MonoBehaviour
{
    private Vector3 direction;

    private GameObject playerOne;
    private GameObject playerTwo;

    //private float raycastRange = 100f;
    //[SerializeField] private LayerMask layerMask;

    
    float counter;



    void Start()
    {
        playerOne = PlayerManager.instance.player1;
        playerTwo = PlayerManager.instance.player2;

        GetComponent<Rigidbody2D>().mass = 0.1f;
        GetComponent<Rigidbody2D>().drag = 1000f;

        GetComponent<Pathfinder>().FindNextNode(transform.position, GetClosestPlayer().transform.position);
    }


    private void Update()
    {
        GetComponent<Pathfinder>().targetNode = PlayerManager.instance.gameObject.GetComponent<Grid>().GetNodeFromWorldPoint(GetClosestPlayer().transform.position);

        //More specific (check if enemy is close to centre of chosen node)
        //if (PlayerManager.instance.gameObject.GetComponent<Grid>().GetNodeFromWorldPoint(transform.position) == GetComponent<Pathfinder>().chosenNode)
        if (Vector3.Distance(transform.position, GetComponent<Pathfinder>().chosenNode.worldPosition) <= 0.5f)
        {
            GetComponent<Pathfinder>().FindNextNode(transform.position, GetClosestPlayer().transform.position);
        }

        
        direction = new Vector3((GetComponent<Pathfinder>().chosenNode.worldPosition.x + 0.5f) - (transform.position.x + 0.5f),
            (GetComponent<Pathfinder>().chosenNode.worldPosition.y + 0.5f) - (transform.position.y + 0.5f), 0);
        
        /*
        direction = new Vector3((GetComponent<Pathfinder>().chosenNode.worldPosition.x + 0.5f) - (PlayerManager.instance.gameObject.GetComponent<Grid>().GetNodeFromWorldPoint(transform.position).worldPosition.x + 0.5f),
            (GetComponent<Pathfinder>().chosenNode.worldPosition.y + 0.5f) - (PlayerManager.instance.gameObject.GetComponent<Grid>().GetNodeFromWorldPoint(transform.position).worldPosition.y + 0.5f), 0);
        */

        GetComponent<Rigidbody2D>().velocity = direction.normalized * GetComponent<EnemyBaseClass>().SpeedFactor;
    }




    void FixedUpdate()
    {
        /*
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
        */
    }

    /*
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
    }*/


    private GameObject GetClosestPlayer()
    {
        if (Vector2.Distance(playerOne.GetComponent<Transform>().position, transform.position) <=
                    Vector2.Distance(playerTwo.GetComponent<Transform>().position, transform.position))
        {
            return playerOne;
        }
        else
        {
            return playerTwo;
        }
    }

}