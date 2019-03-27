using UnityEngine;

public class AimIndicator : MonoBehaviour
{
    private Vector3 mousePos;

    private float aimX;
    private float aimY;

    private Vector2 lastDirection;
    public Vector2 direction { get; private set; }

    [SerializeField] private bool player1;

    void Update()
    {
        if (player1 == true)
        {
            MoveSight("P1");
        }
        else if(player1 == false)
        {
            MoveSight("P2");
        }
        else
        {
            //mousePos = Input.mousePosition;
            //mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            //direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

            //transform.up = direction;
        }
    }

    private void MoveSight(string player)
    {
        aimX = Input.GetAxisRaw("HorizontalRightStick" + player);
        aimY = Input.GetAxisRaw("VerticalRightStick" + player);

        direction = new Vector2(aimX, aimY);

        if (direction == Vector2.zero)
        {
            direction = lastDirection;
        }

        transform.up = direction;

        lastDirection = direction;
    }
}
