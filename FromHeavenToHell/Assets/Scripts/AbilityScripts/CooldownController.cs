using UnityEngine;

public class CooldownController : MonoBehaviour
{
    private float cooldownCountDown;



    public bool CooldownPassed()
    {
        if (cooldownCountDown <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void Update()
    {
        if (cooldownCountDown > 0)
        {
            cooldownCountDown -= Time.deltaTime;
        }
    }


    public void ResetCooldown(float cooldown)
    {
        cooldownCountDown = cooldown;
    }
}
