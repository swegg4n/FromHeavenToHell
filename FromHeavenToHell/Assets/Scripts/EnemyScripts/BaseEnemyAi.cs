﻿using UnityEngine;

public class BaseEnemyAi : MonoBehaviour
{
    private Vector3 direction;
    private Vector3 aimDirection;

    private GameObject player1;
    private GameObject player2;

    [SerializeField] private LayerMask enemyIgnoreLayerMask;


    void Start()
    {
        player1 = PlayerManager.instance.playerAngelInstance;
        player2 = PlayerManager.instance.playerDemonInstance;

        GetComponent<Pathfinder>().FindPath(transform.position, GetClosestTargetPosition());
    }

    private void Update()
    {
        float range = GetComponent<EnemyBaseClass>().Ability.optimalRange / 32f;
        /*Vector3*/ aimDirection = GetClosestTargetPosition() - transform.position;

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.25f, aimDirection, range, enemyIgnoreLayerMask);

        if (hit == true && (hit.transform.tag == "PlayerDemon" || hit.transform.tag == "PlayerAngel"))
        {
            GetComponent<EnemyBaseClass>().Ability.TriggerAbility(gameObject);
            return;
        }
        


        if (GetComponent<Pathfinder>().FinalPath != null)
        {
            if (GetComponent<Rigidbody2D>().velocity != Vector2.zero && GetComponent<Pathfinder>().FinalPath.Count > 0)
            {
                if (GameManager.instance.gameObject.GetComponent<NodeGrid>().GetNodeFromWorldPoint(transform.position) ==
                    GameManager.instance.gameObject.GetComponent<NodeGrid>().GetNodeFromWorldPoint(GetComponent<Pathfinder>().FinalPath[0].WorldPosition))
                {
                    GetComponent<Pathfinder>().FindPath(transform.position, GetClosestTargetPosition());
                }
            }
            else if (GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            {
                GetComponent<Pathfinder>().FindPath(transform.position, GetClosestTargetPosition());
            }

            MoveToNextTile();
        }

    }

    private void MoveToNextTile()
    {
        if (GetComponent<Pathfinder>().FinalPath.Count > 0)
        {
            direction = GetComponent<Pathfinder>().FinalPath[0].WorldPosition - transform.position;
            direction = new Vector3(direction.x + 0.5f, direction.y + 0.5f, 0);

            GetComponent<Rigidbody2D>().velocity = direction.normalized * GetComponent<EnemyBaseClass>().SpeedFactor;
        }
    }

    public Vector3 GetClosestTargetPosition()
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


    private void OnDrawGizmos()
    {
        try
        {
            Gizmos.DrawRay(transform.position, aimDirection);
            Gizmos.DrawWireSphere(transform.position, 0.25f);
        }
        catch (System.Exception)
        {
        }
    }

}