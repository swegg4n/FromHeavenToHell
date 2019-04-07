using UnityEngine;

public class BaseEnemyAi : MonoBehaviour
{
    private Vector3 direction;

    private GameObject player1;
    private GameObject player2;


    void Start()
    {
        player1 = PlayerManager.instance.player1;
        player2 = PlayerManager.instance.player2;
    }

    private void Update()
    {
        GetComponent<Pathfinder>().FindPath(transform.position, GetClosestTargetPosition());

        if (GetComponent<Pathfinder>().FinalPath.Count > 0)
        {
            direction = (GetComponent<Pathfinder>().FinalPath[0].WorldPosition - transform.position);
            direction = new Vector3(direction.x + 0.5f, direction.y + 0.5f, 0);

            GetComponent<Rigidbody2D>().velocity = direction.normalized * GetComponent<EnemyBaseClass>().SpeedFactor;
        }
    }

    private Vector3 GetClosestTargetPosition()
    {
        if (Vector2.Distance(player1.GetComponent<Transform>().position, transform.position) <=
                    Vector2.Distance(player2.GetComponent<Transform>().position, transform.position))
        {
            return player1.transform.position;
        }
        else
        {
            return player2.transform.position;
        }
    }

}