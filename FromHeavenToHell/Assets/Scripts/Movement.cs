using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private float velocityX;
    private float velocityY;

    [SerializeField] private bool player1;

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
        if(player1 == true)
        {
            MovePlayer("P1");
        }
        else if(player1 == false)
        {
            MovePlayer("P2");   
        }
        else
        {
            //velocityX = Input.GetAxisRaw("HorizontalMouse");
            //velocityY = Input.GetAxisRaw("VerticalMouse");

            //if (useVelocity)
            //{
            //    rigidBody.velocity = new Vector2(velocityX, velocityY).normalized * velocitySpeed;
            //}

            //if (useAddForce)
            //{
            //    rigidBody.AddForce(new Vector2(velocityX, velocityY).normalized * addForceSpeed, ForceMode2D.Impulse);
            //}
        }
    }

    private void MovePlayer(string player)
    {
        velocityX = Input.GetAxisRaw("Horizontal" + player);
        velocityY = Input.GetAxisRaw("Vertical" + player);

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
