using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private Ability selectedAbility;


    private void Update()
    {
        CheckFireInput();
        selectedAbility.Update();
    }


    private void CheckFireInput()
    {
        try
        {
            switch (gameObject.tag)
            {
                case "PlayerDemon":
                    {
                        if (Input.GetButton(PlayerManager.instance.PlayerDemonFire) && selectedAbility != null)
                        {
                            selectedAbility.TriggerAbility(gameObject);
                        }
                    }
                    break;

                case "PlayerAngel":
                    {
                        if (Input.GetButton(PlayerManager.instance.PlayerAngelFire) && selectedAbility != null)
                        {
                            selectedAbility.TriggerAbility(gameObject);
                        }
                    }
                    break;
            }
        }
        catch
        {

        }
    }

}