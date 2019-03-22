using UnityEngine;

public class AimIndicator : MonoBehaviour
{
    private Vector3 mousePos;
    [HideInInspector] public Vector2 direction;


    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);


        direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);


        transform.up = direction;

    }
}
