using UnityEngine;

public class AimIndicator : MonoBehaviour
{
    private Vector3 mousePos;

    private float aimX;
    private float aimY;

    private Vector2 lastDirection;
    public Vector2 direction;

    [SerializeField] private bool usingController;

    void Update()
    {
        if (usingController == true)
        {
            aimX = Input.GetAxisRaw("HorizontalRightStick");
            aimY = Input.GetAxisRaw("VerticalRightStick");

            direction = new Vector2(aimX, aimY);

            if(direction == Vector2.zero)
            {
                direction = lastDirection;
            }

            transform.up = direction;

            lastDirection = direction;
        }
        else
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

            transform.up = direction;
        }
    }
}
