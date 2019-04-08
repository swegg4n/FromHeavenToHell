using UnityEngine;

public class AimIndicator : MonoBehaviour
{
    [SerializeField] private GameObject aimIndicator;

    private Vector3 mousePos;

    private float aimX;
    private float aimY;

    private Vector2 lastDirection;
    public Vector2 direction { get; private set; }

    void Update()
    {
        if (gameObject.tag == "PlayerDemon")
        {
            if (PlayerManager.instance.GetDemonUsingMouse() == true)
            {
                MoveSightKeyboard();
            }
            else
            {
                MoveSightJoystick("P1");
            }
        }
        else if(gameObject.tag == "PlayerAngel")
        {
            if (PlayerManager.instance.GetAngelUsingMouse() == true)
            {
                MoveSightKeyboard();
            }
            else
            {
                MoveSightJoystick("P2");
            }
        }
    }

    private void MoveSightKeyboard()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        aimIndicator.transform.up = direction;
    }

    private void MoveSightJoystick(string player)
    {
        aimX = Input.GetAxisRaw("HorizontalRightStick" + player);
        aimY = Input.GetAxisRaw("VerticalRightStick" + player);

        direction = new Vector2(aimX, aimY);

        if (direction == Vector2.zero)
        {
            direction = lastDirection;
        }

        aimIndicator.transform.up = direction;

        lastDirection = direction;
    }
}
