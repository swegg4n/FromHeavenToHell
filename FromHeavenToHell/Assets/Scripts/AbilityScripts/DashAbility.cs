using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Dash Ability")]
public class DashAbility : Ability
{
    private GameObject caster;
    private bool dashing;

    [SerializeField] private float dashDistance;
    [SerializeField] private float dashSpeed;

    private Vector3 dashTarget;



    public override void TriggerAbility(GameObject caster)
    {
        this.caster = caster;
        //calculate dash target  (direction * dashDistance, detect colliders with raytrace)
        dashing = true;
    }


    public override void FixedUpdate()
    {
        if (dashing == true)
        {
            //dash to target
            //set: dashing = false
        }
    }
}