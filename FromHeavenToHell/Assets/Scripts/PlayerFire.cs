using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    private bool fireInput;

    private float projectileSpeed = 1f;

    private void Update()
    {
        if (gameObject.tag == "PlayerDemon")
        {
            if (PlayerManager.instance.playerDemonUsingMouseAndKeyboard == true)
            {
                KeyboardFireShot();
            }
            else
            {
                FireShot("R1P1");
            }
        }
        else if (gameObject.tag == "PlayerAngel")
        {
            if (PlayerManager.instance.playerAngelUsingMouseAndKeyboard == true)
            {
                KeyboardFireShot();
            }
            else
            {
                FireShot("R1P2");
            }
        }
    }

    private void KeyboardFireShot()
    {
        if (Input.GetButtonDown("MouseLeftClick"))
        {
            var bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = GetComponentInChildren<AimIndicator>().direction.normalized * projectileSpeed;
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