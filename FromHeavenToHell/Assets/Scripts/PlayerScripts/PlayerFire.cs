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
            if ((PlayerManager.instance.GetDemonUsingMouse() == true && Input.GetButton("MouseLeftClick") == true) ||
                (PlayerManager.instance.GetDemonUsingMouse() == false && Input.GetButton("R1P1") == true))
            {
                selectedAbility.TriggerAbility(gameObject);
            }
        }
        else if (gameObject.tag == "PlayerAngel")
        {
            if ((PlayerManager.instance.GetAngelUsingMouse() == true && Input.GetButton("MouseLeftClick") == true) ||
                (PlayerManager.instance.GetAngelUsingMouse() == false && Input.GetButton("R1P2") == true))
            {
                selectedAbility.TriggerAbility(gameObject);
            }
        }
    }
}