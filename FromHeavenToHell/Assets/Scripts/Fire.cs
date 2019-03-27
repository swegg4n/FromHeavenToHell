using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    [SerializeField] private bool player1;

    private bool fireInput;

    private float projectileSpeed = 1f;

    private void Update()
    {
        if(player1 == true)
        {
            FireShot("R1P1");
        }
        else if(player1 == false)
        {
            FireShot("R1P2");
        }
        else
        {
            //if (Input.GetButtonDown("Fire1Mouse"))
            //{
            //    var bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            //    bullet.GetComponent<Rigidbody2D>().velocity = GetComponentInChildren<AimIndicator>().direction.normalized * projectileSpeed;
            //}
        }
    }

    private void FireShot(string input)
    {
        fireInput = Input.GetButtonDown(input);

        if (fireInput == true)
        {
            var bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = GetComponentInChildren<AimIndicator>().direction.normalized * projectileSpeed;
        }
    }
}
