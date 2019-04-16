using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public float velocityX { get; private set; }    //Hastigheten i x-led
    public float velocityY { get; private set; }    //Hastigheten i y-led

    [SerializeField] private float playerSpeed;     //Spelarens fart

    public bool Dashing { get; set; }       //Håller reda på om spelaren håller på att göra en dash


    /// <summary>
    /// Kallas före Start
    /// </summary>
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        /*TA BORT EFTER VÄRDEN FÖR SPELAR-PREFAB ÄR IFYLLDA*/
        playerSpeed = 130f;

        rigidBody.mass = 0.1f;
        rigidBody.drag = 1000f;
        /*<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*/
    }

    void FixedUpdate()
    {
        GetInput();
        MovePlayer();
    }

    /// <summary>
    /// Låter spelarna styras på olika sätt beroende på valda styrsätt
    /// </summary>
    private void GetInput()
    {
        if (gameObject.tag == "PlayerDemon")
        {
            if (PlayerManager.instance.PlayerDemonUsingMouse == true)
            {
                GetKeyboardInput();
            }
            else
            {
                GetJoystickInput("P1");
            }
        }
        else if (gameObject.tag == "PlayerAngel")
        {
            if (PlayerManager.instance.PlayerAngelUsingMouse == true)
            {
                GetKeyboardInput();
            }
            else
            {
                GetJoystickInput("P2");
            }
        }
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
        if (Dashing == false)
        {
            rigidBody.velocity = new Vector2(velocityX, velocityY).normalized * playerSpeed;
        }
    }

}