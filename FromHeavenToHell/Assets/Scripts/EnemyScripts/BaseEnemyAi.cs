using System;
using UnityEngine;

public class BaseEnemyAi : MonoBehaviour
{
    private Vector3 direction;      //Riktningen fienden går
    private Vector3 aimDirection;       //Riktningen fienden siktar

    private GameObject player1;
    private GameObject player2;

    [SerializeField] private LayerMask enemyIgnoreLayerMask;    //Det fienden ska ignorera när den ser om den ska skjuta
    [SerializeField] private bool stationary;       //Om fienden ska röra sig eller inte

    private const float enemyFireSpace = 0.25f;     //Radie fienden ska kompesera för vid skjutning


    void Start()
    {
        player1 = PlayerManager.instance.PlayerAngelInstance;
        player2 = PlayerManager.instance.PlayerDemonInstance;

        if (stationary == false)
        {
            //Kallar på att ta ut en ny väg för fienden att går
            GetComponent<Pathfinder>().FindPath(transform.position, GetClosestTargetPosition());
        }
    }

    void Update()
    {
        if (GameManager.instance.Paused == false)
        {
            CheckIfHitAndFire();

            if (stationary == false)
            {

                if (GetComponent<Pathfinder>().FinalPath != null)
                {
                    if (GetComponent<Pathfinder>().FinalPath.Count > 0)
                    {
                        //Om fienden har kommit fram till nästa position i dens väg
                        if (GameManager.instance.gameObject.GetComponent<NodeGrid>().GetNodeFromWorldPoint(transform.position) ==
                            GameManager.instance.gameObject.GetComponent<NodeGrid>().GetNodeFromWorldPoint(GetComponent<Pathfinder>().FinalPath[0].WorldPosition))
                        {
                            //Kallar på att ta ut en ny väg för fienden att går
                            GetComponent<Pathfinder>().FindPath(transform.position, GetClosestTargetPosition());
                        }
                    }
                    //Om fienden står still
                    else if (GetComponent<Rigidbody2D>().velocity == Vector2.zero)
                    {
                        //Kallar på att ta ut en ny väg för fienden att går
                        GetComponent<Pathfinder>().FindPath(transform.position, GetClosestTargetPosition());
                    }

                    MoveToNextTile();
                }
            }
        }

    }

    /// <summary>
    /// Ser om fienden kan skjuta närmsta spelare (ser så ingen vägg är i vägen)
    /// </summary>
    protected virtual void CheckIfHitAndFire()
    {
        try
        {
            aimDirection = GetClosestTargetPosition() - transform.position;
            float range = GetComponent<EnemyBaseClass>().Ability.OptimalRange / GameManager.instance.TileSize;
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, enemyFireSpace, aimDirection, range, enemyIgnoreLayerMask);
            if (hit == true && (hit.transform.tag == GameManager.objectsTags[GameManager.Objects.PlayerDemon] || hit.transform.tag == GameManager.objectsTags[GameManager.Objects.PlayerAngel]))
            {
                GetComponent<EnemyBaseClass>().Ability.TriggerAbility(gameObject);
            }
        }
        catch (Exception e)
        {
        }
    }

    /// <summary>
    /// Rör fienden till nästa position i fiendens väg
    /// </summary>
    private void MoveToNextTile()
    {
        if (GetComponent<Pathfinder>().FinalPath.Count > 0)
        {
            direction = GetComponent<Pathfinder>().FinalPath[0].WorldPosition - transform.position;
            direction = new Vector3(direction.x + 0.5f, direction.y + 0.5f, 0);

            GetComponent<Rigidbody2D>().velocity = direction.normalized * GetComponent<EnemyBaseClass>().SpeedFactor;
        }
    }

    /// <summary>
    /// Räknar ut vilken spelare som är närmst fienden
    /// </summary>
    /// <returns>Returnerar positionen för denna spelare som en Vector3</returns>
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