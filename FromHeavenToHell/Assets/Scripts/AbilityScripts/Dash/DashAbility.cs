using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Dash Ability")]
public class DashAbility : Ability
{
    private GameObject caster;

    [SerializeField] private float dashDistance;    //Distansen användaren ska förflyttas
    [SerializeField] private float dashTime;    //Tiden under vilken användaren ska förflyttas
    [SerializeField] private float cooldown;    //Tiden användaren måste vänta innan abilityn kan användas
    private float dashSpeed;    //Farten användaren ska förflyttas med
    private Vector2 dashDirection;  //Riktningen för förflyttning
    private float timeCounter;
    private bool instantDash;   //Ifall användaren ska teleporteras direkt till mål eller förflyttas över tid

    /// <summary>
    /// Metod som används för att aktivera abilities
    /// </summary>
    /// <param name="caster">Objektet som använder abilityn</param>
    public override void TriggerAbility(GameObject caster)
    {
        CooldownController cdController = caster.GetComponent<CooldownController>();

        if (cdController.DashCooldownPassed() == true)
        {
            this.caster = caster;
            dashDirection = caster.GetComponent<AimIndicator>().Direction.normalized;

            if (dashTime != 0)
            {
                caster.GetComponent<PlayerMovement>().Dashing = true;
                dashSpeed = dashDistance / dashTime;
                cdController.ResetDashCooldown(cooldown);
            }
            else
            {
                if (InstantDash() == true)
                {
                    cdController.ResetDashCooldown(cooldown);
                }
            }
        }
    }

    public override void Update()
    {
        if (GameManager.instance.Paused == false)
        {
            if (caster != null)
            {
                if (caster.GetComponent<PlayerMovement>().Dashing == true)
                {
                    NormalDash();
                }
            }
        }
    }

    /// <summary>
    /// Räknar ut positionen användaren ska förflyttas till
    /// </summary>
    /// <returns>Returnerar om positionen är möjlig att förflyttas till (Om det finns ett golv där)</returns>
    private bool InstantDash()
    {
        Vector3 targetPosition = caster.transform.position + (Vector3)dashDirection * dashDistance / GameManager.instance.TileSize;

        if (GameManager.instance.CurrentRoom.GetComponent<Room>().CheckOnlyGroundTileWorldToCell(targetPosition) == true)
        {
            caster.transform.position = targetPosition;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Förflyttar användaren över tid
    /// </summary>
    private void NormalDash()
    {
        if (timeCounter < dashTime)
        {
            caster.GetComponent<Rigidbody2D>().velocity = dashDirection * dashSpeed;
            timeCounter += Time.deltaTime;
        }
        else
        {
            timeCounter = 0;
            caster.GetComponent<PlayerMovement>().Dashing = false;
        }
    }
}