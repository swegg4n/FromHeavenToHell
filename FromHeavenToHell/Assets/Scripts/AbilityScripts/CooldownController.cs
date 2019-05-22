using UnityEngine;

public class CooldownController : MonoBehaviour
{
    private float cooldownCountDown;    //tiden som måste väntas. Skrivs i sekunder

    /// <summary>
    /// Kontrollerar om väntetiden har passerat eller inte
    /// </summary>
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

    /// <summary>
    /// Räknar ner väntetiden efter hand som tiden går
    /// </summary>
    private void Update()
    {
        if (GameManager.instance.Paused == false)
        {
            if (cooldownCountDown > 0)
            {
                cooldownCountDown -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// Återställer väntetiden
    /// </summary>
    /// <param name="cooldown">Nya tiden som måste väntas</param>
    public void ResetCooldown(float cooldown)
    {
        cooldownCountDown = cooldown;
    }
}
