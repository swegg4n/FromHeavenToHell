using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    [SerializeField] private bool usingController;

    private float lastFireInput;
    private float fireInput;

    private float projectileSpeed = 1f;

    private void Update()
    {
        if(usingController == true)
        {
            fireInput = Input.GetAxisRaw("Fire1");

            if (lastFireInput < 0.5 && fireInput > 0.5)
            {
                var bullet = Instantiate(projectile, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = GetComponentInChildren<AimIndicator>().direction.normalized * projectileSpeed;
            }

            lastFireInput = fireInput;
        }
        else
        {
            if (Input.GetButtonDown("Fire1Mouse"))
            {
                var bullet = Instantiate(projectile, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = GetComponentInChildren<AimIndicator>().direction.normalized * projectileSpeed;
            }
        }
    }

}
