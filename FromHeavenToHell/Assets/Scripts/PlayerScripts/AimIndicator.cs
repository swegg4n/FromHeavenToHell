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
                if (Inputs.PlayerDemonUsingMouse == true)
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
                if (Inputs.PlayerAngelUsingMouse == true)
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
                    if (Inputs.PlayerDemonHorizontalAimAxis != null || Inputs.PlayerDemonVerticalAimAxis != null)
                    {
                        MoveSightJoystick(Inputs.PlayerDemonHorizontalAimAxis, Inputs.PlayerDemonVerticalAimAxis);
                    }
                    else
                    {
                        MoveSightKeyboard();
                    }
                }
                break;

            case "PlayerAngel":
                {
                    if (Inputs.PlayerAngelHorizontalAimAxis != null || Inputs.PlayerAngelVerticalAimAxis != null)
                    {
                        MoveSightJoystick(Inputs.PlayerAngelHorizontalAimAxis, Inputs.PlayerAngelVerticalAimAxis);
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
