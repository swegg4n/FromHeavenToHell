using UnityEngine;

public class AimIndicator : MonoBehaviour
{
    [SerializeField] private GameObject aimIndicator;

    private Vector3 mousePos;

    private float aimX;
    private float aimY;

    private Vector2 lastDirection;
    public Vector2 Direction { get; private set; }


    void Update()
    {
        if (GameManager.instance.Paused == false)
        {
            CheckAimInput();
        }
    }

    private void CheckAimInput()
    {
        if (gameObject.tag == GameManager.objectsTags[GameManager.Objects.PlayerDemon])
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
        else if (gameObject.tag == GameManager.objectsTags[GameManager.Objects.PlayerAngel])
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
    }

    private void MoveSightJoystick(string horizontalAimInput, string verticalAimInput)
    {
        aimX = Input.GetAxisRaw(horizontalAimInput);
        aimY = Input.GetAxisRaw(verticalAimInput);

        Direction = new Vector2(aimX, aimY);

        if (Direction == Vector2.zero)
        {
            Direction = lastDirection;
        }

        aimIndicator.transform.up = Direction;

        lastDirection = Direction;
    }

    private void MoveSightKeyboard()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        aimIndicator.transform.up = Direction;
    }

}
