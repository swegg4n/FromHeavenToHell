using UnityEngine;

public class CooldownController : MonoBehaviour
{
    private float cooldownCountDownProjectile;    //tiden som måste väntas. Skrivs i sekunder
    private float cooldownCountDownAoe;    //tiden som måste väntas. Skrivs i sekunder
    private float cooldownCountDownDash;    //tiden som måste väntas. Skrivs i sekunder

    /// <summary>
    /// Kontrollerar om väntetiden har passerat eller inte
    /// </summary>
    public bool ProjectileCooldownPassed()
    {
        if (cooldownCountDownProjectile <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Kontrollerar om väntetiden har passerat eller inte
    /// </summary>
    public bool AoeCooldownPassed()
    {
        if (cooldownCountDownAoe <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Kontrollerar om väntetiden har passerat eller inte
    /// </summary>
    public bool DashCooldownPassed()
    {
        if (cooldownCountDownDash <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Räknar ner väntetiden efter hand som tiden går
    /// </summary>
    private void Update()
    {
        if (GameManager.instance.Paused == false)
        {
            if (cooldownCountDownProjectile > 0)
            {
                cooldownCountDownProjectile -= Time.deltaTime;
            }

            if (cooldownCountDownAoe > 0)
            {
                cooldownCountDownAoe -= Time.deltaTime;
            }

            if (cooldownCountDownDash > 0)
            {
                cooldownCountDownDash -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// Återställer väntetiden
    /// </summary>
    /// <param name="cooldown">Nya tiden som måste väntas</param>
    public void ResetProjectileCooldown(float cooldown)
    {
        cooldownCountDownProjectile = cooldown;
    }

    /// <summary>
    /// Återställer väntetiden
    /// </summary>
    /// <param name="cooldown">Nya tiden som måste väntas</param>
    public void ResetAoeCooldown(float cooldown)
    {
        cooldownCountDownAoe = cooldown;
    }

    /// <summary>
    /// Återställer väntetiden
    /// </summary>
    /// <param name="cooldown">Nya tiden som måste väntas</param>
    public void ResetDashCooldown(float cooldown)
    {
        cooldownCountDownDash = cooldown;
    }
}
