using UnityEngine;

public class Movement : MonoBehaviour
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
        if (Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Horizontal") > 0)
            velocityX = Input.GetAxisRaw("Horizontal");
        else
            velocityX = 0;

        if (Input.GetAxisRaw("Vertical") < 0 || Input.GetAxisRaw("Vertical") > 0)
            velocityY = Input.GetAxisRaw("Vertical");
        else
            velocityY = 0;


        if (useVelocity)
            rigidBody.velocity = new Vector2(velocityX, velocityY).normalized * velocitySpeed;

        if (useAddForce)
            rigidBody.AddForce(new Vector2(velocityX, velocityY).normalized * addForceSpeed, ForceMode2D.Impulse);
            
    }
}
