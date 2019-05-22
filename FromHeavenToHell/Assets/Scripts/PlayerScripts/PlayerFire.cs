using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private Ability[] abilityList;


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


    private void CheckFireInput()
    {
        try
        {
            switch (gameObject.tag)
            {
                case "PlayerDemon":
                    {
                        TriggerAbilityDemon();
                    }
                    break;

                case "PlayerAngel":
                    {
                        TriggerAbilityAngel();
                    }
                    break;
            }
        }
        catch
        {

        }
    }

    private void TriggerAbilityDemon()
    {
        if (Input.GetButton(PlayerManager.instance.PlayerDemonFire[0]) && abilityList[0] != null)
        {
            abilityList[0].TriggerAbility(gameObject);
        }
        if ((Input.GetAxisRaw(PlayerManager.instance.PlayerDemonFire[1]) == 1 || Input.GetButton(PlayerManager.instance.PlayerDemonFire[1]))
            && abilityList[1] != null)
        {
            abilityList[1].TriggerAbility(gameObject);
        }
        if ((Input.GetAxisRaw(PlayerManager.instance.PlayerDemonFire[2]) == 1 || Input.GetButton(PlayerManager.instance.PlayerDemonFire[2])) 
            && abilityList[2] != null)
        {
            abilityList[2].TriggerAbility(gameObject);
        }
    }

    private void TriggerAbilityAngel()
    {
        if (Input.GetButton(PlayerManager.instance.PlayerAngelFire[0]) && abilityList[0] != null)
        {
            abilityList[0].TriggerAbility(gameObject);
        }
        if ((Input.GetAxisRaw(PlayerManager.instance.PlayerAngelFire[1]) == 1 || Input.GetButton(PlayerManager.instance.PlayerAngelFire[1]))
            && abilityList[1] != null)
        {
            abilityList[1].TriggerAbility(gameObject);
        }
        if ((Input.GetAxisRaw(PlayerManager.instance.PlayerAngelFire[2]) == 1 || Input.GetButton(PlayerManager.instance.PlayerAngelFire[2]))
            && abilityList[2] != null)
        {
            abilityList[2].TriggerAbility(gameObject);
        }
    }
}