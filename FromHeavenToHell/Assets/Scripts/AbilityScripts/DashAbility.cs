using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Dash Ability")]
public class DashAbility : Ability
{
    private GameObject caster;

    [SerializeField] private float dashDistance;
    [SerializeField] private float dashTime;
    [SerializeField] private float cooldown;
    private float dashSpeed;
    private Vector2 dashDirection;
    private float timeCounter;
    private bool instantDash;


    public override void TriggerAbility(GameObject caster)
    {
        CooldownController cdController = caster.GetComponent<CooldownController>();

        if(cdController.CooldownPassed() == true)
        {
            this.caster = caster;
            dashDirection = caster.GetComponent<AimIndicator>().direction.normalized;
            

            if (dashTime != 0)
            {
                caster.GetComponent<Movement>().dashing = true;
                dashSpeed = dashDistance / dashTime;
                cdController.ResetCooldown(cooldown);
            }
            else
            {
                if(InstantDash() == true)
                {
                    cdController.ResetCooldown(cooldown);
                }
            }
        }
    }

    public override void Update()
    {
        if(caster != null)
        {
            if (caster.GetComponent<Movement>().dashing == true)
            {
                NormalDash();
            }
        }
    }

    private bool InstantDash()
    {
        Vector3 targetPosition = caster.transform.position + (Vector3)dashDirection * dashDistance / GameManager.instance.tileSize;
        if (GameManager.instance.CheckOnlyGroundTile(targetPosition) == true)
        {
            caster.transform.position = targetPosition;
            return true;
        }
        return false;
    }

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
            caster.GetComponent<Movement>().dashing = false;
        }
    }
}