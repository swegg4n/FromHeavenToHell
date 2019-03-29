using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private AbilityProperty selectedAbility;
    
    [SerializeField] private bool isPlayer1;
    /*
    private bool fireInput;

    private float projectileSpeed = 1f;
    */

    private void Update()
    {
        if (Input.GetButton("R1P1") == true && isPlayer1 == true)
        {
            if (selectedAbility != null)
                selectedAbility.UseAbility(transform.position, GetComponentInChildren<AimIndicator>().direction);
        }

        /*
        if (player1 == true)
        {
            FireShot("R1P1");
        }
        else if (player1 == false)
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
        }*/
    }

    //private void Fire()
    //{
        /*
        if (fireInput == true)
        {
            var bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = GetComponentInChildren<AimIndicator>().direction.normalized * projectileSpeed;
        }*/
    //}
}
