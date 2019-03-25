using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private float velocityX;
    private float velocityY;

    [SerializeField] private bool usingController;

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

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(usingController == true)
        {
            velocityX = Input.GetAxisRaw("Horizontal");
            velocityY = Input.GetAxisRaw("Vertical");

            if (useVelocity)
            {
                rigidBody.velocity = new Vector2(velocityX, velocityY).normalized * velocitySpeed;
            }

            if (useAddForce)
            {
                rigidBody.AddForce(new Vector2(velocityX, velocityY).normalized * addForceSpeed, ForceMode2D.Impulse);
            }
        }
        else
        {
            velocityX = Input.GetAxisRaw("HorizontalMouse");
            velocityY = Input.GetAxisRaw("VerticalMouse");

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
}
