using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private Ability selectedAbility;


    private void Update()
    {
        UseAbility();
        selectedAbility.Update();
    }


    private void UseAbility()
    {
        if (gameObject.tag == "PlayerDemon")
        {
            if ((PlayerManager.instance.PlayerDemonUsingMouse == true && Input.GetButton("MouseLeftClick") == true)
                || (PlayerManager.instance.PlayerDemonUsingMouse == false && Input.GetButton("R1P1") == true))
            {
                selectedAbility.TriggerAbility(gameObject);
            }
        }
        else if (gameObject.tag == "PlayerAngel")
        {
            if ((PlayerManager.instance.PlayerAngelUsingMouse == true && Input.GetButton("MouseLeftClick") == true)
                || (PlayerManager.instance.PlayerAngelUsingMouse == false && Input.GetButton("R1P2") == true))
            {
                selectedAbility.TriggerAbility(gameObject);
            }
        }
    }

}