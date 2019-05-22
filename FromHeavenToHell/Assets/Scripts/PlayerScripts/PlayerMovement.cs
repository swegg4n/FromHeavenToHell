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
        /*>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>*/
        playerSpeed = 130f;

        rigidBody.mass = 0.1f;
        rigidBody.drag = 1000f;
        /*<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*/
    }

    void FixedUpdate()
    {
        if(GameManager.instance.Paused == false)
        {
            //GetInput();
            MovePlayer();
        }
    }

    #region old
    /*
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
    */
    #endregion

    private void MovePlayer()
    {
        if (Dashing == false)
        {
            if (PlayerManager.instance.PlayerDemonHorizontalAxis == null || PlayerManager.instance.PlayerAngelHorizontalAxis == null ||
                PlayerManager.instance.PlayerDemonVerticalAxis == null || PlayerManager.instance.PlayerAngelVerticalAxis == null)
            {
                return;
            }

            bool isDemon = (tag == "PlayerDemon");

            velocityX = Input.GetAxisRaw(isDemon ? PlayerManager.instance.PlayerDemonHorizontalAxis : PlayerManager.instance.PlayerAngelHorizontalAxis);
            velocityY = Input.GetAxisRaw(isDemon ? PlayerManager.instance.PlayerDemonVerticalAxis : PlayerManager.instance.PlayerAngelVerticalAxis);

            rigidBody.velocity = new Vector2(velocityX, velocityY).normalized * playerSpeed;
        }
    }

}