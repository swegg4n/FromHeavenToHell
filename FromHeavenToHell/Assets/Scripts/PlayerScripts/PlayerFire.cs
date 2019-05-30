using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private Ability[] abilityList;     //Tillgängliga specialförmågor


    private void Update()
    {
        if (GameManager.instance.Paused == false)
        {
            CheckFireInput();
            foreach (Ability a in abilityList)
            {
                a.Update();
            }
        }
    }

    /// <summary>
    /// Ser om någon spelare trycker för att skjuta
    /// </summary>
    private void CheckFireInput()
    {
        try
        {
            if (gameObject.tag == GameManager.objectsTags[GameManager.Objects.PlayerDemon])
            {
                TriggerAbilityDemon();
            }
            else if (gameObject.tag == GameManager.objectsTags[GameManager.Objects.PlayerAngel])
            {
                TriggerAbilityAngel();
            }
        }
        catch {}
    }

    /// <summary>
    /// Triggrar en förmåga för demonen beroende på knapp tryckt
    /// </summary>
    private void TriggerAbilityDemon()
    {
        if (Input.GetAxisRaw(Inputs.PlayerDemonFire[0]) >= 0.01 && abilityList[0] != null)
        {
            abilityList[0].TriggerAbility(gameObject);
        }
        if ((Input.GetAxisRaw(Inputs.PlayerDemonFire[1]) >= 0.01 || Input.GetButton(Inputs.PlayerDemonFire[1]))
            && abilityList[1] != null)
        {
            abilityList[1].TriggerAbility(gameObject);
        }
        if ((Input.GetButton(Inputs.PlayerDemonFire[2]) || Input.GetButton(Inputs.PlayerDemonFire[2]))
            && abilityList[2] != null)
        {
            abilityList[2].TriggerAbility(gameObject);
        }
    }

    /// <summary>
    /// Triggrar en förmåga för ängeln beroende på knapp tryckt
    /// </summary>
    private void TriggerAbilityAngel()
    {
        if (Input.GetAxisRaw(Inputs.PlayerAngelFire[0]) >= 0.01 && abilityList[0] != null)
        {
            abilityList[0].TriggerAbility(gameObject);
        }
        if ((Input.GetAxisRaw(Inputs.PlayerAngelFire[1]) >= 0.01 || Input.GetButton(Inputs.PlayerAngelFire[1]))
            && abilityList[1] != null)
        {
            abilityList[1].TriggerAbility(gameObject);
        }
        if ((Input.GetButton(Inputs.PlayerAngelFire[2]) || Input.GetButton(Inputs.PlayerAngelFire[2]))
            && abilityList[2] != null)
        {
            abilityList[2].TriggerAbility(gameObject);
        }
    }
}