using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private AbilityProperty selectedAbility;

    private float nextFire;

    //[SerializeField] private GameObject projectile;

    //private bool fireInput;

    //private float projectileSpeed = 1f;



    private void Update()
    {
        if (gameObject.tag == "PlayerDemon")
        {
            if (selectedAbility.fireRate > 0)
            {
                if ((PlayerManager.instance.playerDemonUsingMouseAndKeyboard == true && Input.GetButton("MouseLeftClick") == true && Time.time > nextFire) ||
                    (PlayerManager.instance.playerDemonUsingMouseAndKeyboard == false && Input.GetButton("R1P1") == true && Time.time > nextFire))
                {
                    nextFire = Time.time + 1f / selectedAbility.fireRate;

                    UseAbility(transform.position, GetComponent<AimIndicator>().direction);
                }
            }
            else
            {
                if ((PlayerManager.instance.playerDemonUsingMouseAndKeyboard == true && Input.GetButtonDown("MouseLeftClick") == true) ||
                    (PlayerManager.instance.playerDemonUsingMouseAndKeyboard == false && Input.GetButtonDown("R1P1") == true))
                {
                    UseAbility(transform.position, GetComponent<AimIndicator>().direction);
                }
            }
        }
        else if (gameObject.tag == "PlayerAngel")
        {
            if (selectedAbility.fireRate > 0)
            {
                if ((PlayerManager.instance.playerAngelUsingMouseAndKeyboard == true && Input.GetButton("MouseLeftClick") == true && Time.time > nextFire) ||
                    (PlayerManager.instance.playerAngelUsingMouseAndKeyboard == false && Input.GetButton("R1P2") == true && Time.time > nextFire))
                {
                    nextFire = Time.time + 1f / selectedAbility.fireRate;

                    UseAbility(transform.position, GetComponent<AimIndicator>().direction);
                }
            }
            else
            {
                if ((PlayerManager.instance.playerAngelUsingMouseAndKeyboard == true && Input.GetButtonDown("MouseLeftClick") == true) ||
                    (PlayerManager.instance.playerAngelUsingMouseAndKeyboard == false && Input.GetButtonDown("R1P2") == true))
                {
                    UseAbility(transform.position, GetComponent<AimIndicator>().direction);
                }
            }
        }
    }

    /*
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
    */

    private void UseAbility(Vector3 position, Vector3 direction)
    {
        //Projectile
        if (selectedAbility.abilityType == AbilityProperty.AbilityType.Projectile)
        {
            var projectile = Instantiate(selectedAbility.projectilePrefab, position, Quaternion.identity);

            if (selectedAbility.icon != null)
            {
                projectile.GetComponent<SpriteRenderer>().sprite = selectedAbility.icon;
            }
            else
            {
                Debug.LogWarning("Ability has no assigned sprite!");
            }

            projectile.GetComponent<SpriteRenderer>().color = selectedAbility.color;

            projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * selectedAbility.speed;
        }

        //Static Beam
        if (selectedAbility.abilityType == AbilityProperty.AbilityType.StaticBeam)
        {
            Instantiate(selectedAbility.staticBeamPrefab, position, Quaternion.identity);
        }
    }

}