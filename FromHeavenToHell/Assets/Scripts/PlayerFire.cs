using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private Ability selectedAbility;



    private void Update()
    {
        selectedAbility.UpdateCooldown(gameObject);

        if (gameObject.tag == "PlayerDemon")
        {
            if ((PlayerManager.instance.playerDemonUsingMouseAndKeyboard == true && Input.GetButton("MouseLeftClick") == true) ||
                (PlayerManager.instance.playerDemonUsingMouseAndKeyboard == false && Input.GetButton("R1P1") == true))
            {
                selectedAbility.TriggerAbility(gameObject);
            }
        }
        else if (gameObject.tag == "PlayerAngel")
        {
            if ((PlayerManager.instance.playerAngelUsingMouseAndKeyboard == true && Input.GetButton("MouseLeftClick") == true) ||
                (PlayerManager.instance.playerAngelUsingMouseAndKeyboard == false && Input.GetButton("R1P2") == true))
            {
                selectedAbility.TriggerAbility(gameObject);
            }
        }
    }

}