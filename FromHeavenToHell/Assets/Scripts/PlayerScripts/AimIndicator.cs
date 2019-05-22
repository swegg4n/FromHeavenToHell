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
        if (GameManager.instance.Paused == false)
        {

            CheckAimInput();

            #region old
            /*
            if (gameObject.tag == "PlayerDemon")
            {
                if (PlayerManager.instance.PlayerDemonUsingMouse == true)
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
                if (PlayerManager.instance.PlayerAngelUsingMouse == true)
                {
                    MoveSightKeyboard();
                }
                else
                {
                    MoveSightJoystick("P2");
                }
            }*/
            #endregion
        }
    }

    private void CheckAimInput()
    {
        switch (gameObject.tag)
        {
            case "PlayerDemon":
                {
                    if (PlayerManager.instance.PlayerDemonHorizontalAimAxis != null || PlayerManager.instance.PlayerDemonVerticalAimAxis != null)
                    {
                        MoveSightJoystick(PlayerManager.instance.PlayerDemonHorizontalAimAxis, PlayerManager.instance.PlayerDemonVerticalAimAxis);
                    }
                    else
                    {
                        MoveSightKeyboard();
                    }
                }
                break;

            case "PlayerAngel":
                {
                    if (PlayerManager.instance.PlayerAngelHorizontalAimAxis != null || PlayerManager.instance.PlayerAngelVerticalAimAxis != null)
                    {
                        MoveSightJoystick(PlayerManager.instance.PlayerAngelHorizontalAimAxis, PlayerManager.instance.PlayerAngelVerticalAimAxis);
                    }
                    else
                    {
                        MoveSightKeyboard();
                    }
                }
                break;
        }
    }

    private void MoveSightJoystick(string horizontalAimInput, string verticalAimInput)
    {
        aimX = Input.GetAxisRaw(horizontalAimInput);
        aimY = Input.GetAxisRaw(verticalAimInput);

        direction = new Vector2(aimX, aimY);

        if (direction == Vector2.zero)
        {
            direction = lastDirection;
        }

        aimIndicator.transform.up = direction;

        lastDirection = direction;
    }

    private void MoveSightKeyboard()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        aimIndicator.transform.up = direction;
    }

}
