using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private float velocityX;
    private float velocityY;

    [SerializeField] private float velocitySpeed;
    [SerializeField] private float addForceSpeed;

    [SerializeField] private bool useVelocity;
    [SerializeField] private bool useAddForce;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        velocitySpeed = 130f;
        addForceSpeed = 11f;

        rigidBody.mass = 0.1f;
        rigidBody.drag = 1000f;

        useVelocity = true;
    }


    void FixedUpdate()
    {
        if(gameObject.tag == "PlayerDemon")
        {
            if(PlayerManager.instance.playerDemonUsingMouseAndKeyboard == true)
            {
                GetKeyboardInput();
            }
            else
            {
                GetJoystickInput("P1");
            }  
        }
        else if(gameObject.tag == "PlayerAngel")
        {
            if (PlayerManager.instance.playerAngelUsingMouseAndKeyboard == true)
            {
                GetKeyboardInput();
            }
            else
            {
                GetJoystickInput("P2");
            }
              
        }
        MovePlayer();
    }

    private void GetKeyboardInput()
    {
        velocityX = Input.GetAxisRaw("HorizontalMouse");
        velocityY = Input.GetAxisRaw("VerticalMouse");
        
    }


    private void GetJoystickInput(string player)
    {
        velocityX = Input.GetAxisRaw("Horizontal" + player);
        velocityY = Input.GetAxisRaw("Vertical" + player);
    }
    private void MovePlayer()
    {
        if (useVelocity)
        {
            rigidBody.velocity = new Vector2(velocityX, velocityY).normalized * velocitySpeed;
        }

        if (useAddForce)
        {
            rigidBody.AddForce(new Vector2(velocityX, velocityY).normalized * addForceSpeed, ForceMode2D.Impulse);
        }
    }
}
