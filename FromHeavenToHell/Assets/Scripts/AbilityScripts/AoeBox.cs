using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AOE Box Ability")]
public class AoeBox : Ability
{
    [SerializeField] private float cooldown;
    //[Tooltip("X = Length \nY = Width")] [SerializeField] private Vector2 boxSize;
    [SerializeField] private float activeDuration;
    Animation animation;



    public override void TriggerAbility(GameObject caster)
    {
        CooldownController cdController = caster.GetComponent<CooldownController>();


        if (cdController.CooldownPassed() == true)
        {
            float radAngle = Mathf.Atan2(caster.GetComponent<AimIndicator>().direction.y, caster.GetComponent<AimIndicator>().direction.x);
            float degAngle = (radAngle / (2 * Mathf.PI)) * 360;

            var aoeBox = Instantiate(abilityPrefab, caster.transform.position, Quaternion.Euler(0, 0, degAngle));
            //start scale animation
            Destroy(aoeBox, activeDuration);

            cdController.ResetCooldown(cooldown);  
        }
    }

    /*
    private IEnumerator ScaleOverTime(GameObject aoeBox)
    {
        float currentTime = 0f;

        do
        {
            aoeBox.transform.localScale = new Vector3(Mathf.Lerp(0, boxSize.x, currentTime / scaleTime), boxSize.y);
            currentTime += Time.deltaTime;
        }
        while (currentTime < scaleTime);

        yield break;
    }
    */
}