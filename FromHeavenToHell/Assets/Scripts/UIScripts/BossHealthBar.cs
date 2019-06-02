using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    private int maxHP;      //Bossens maximala liv
    private float healthPercentage, greenColorPercentage, redColorPercentage;   //Bossens nuvarnade liv     //Andel som ska vara grön   //Andel som ska vara röd
    private bool maxHPSet;  


    void Update()
    {
        UpdateBossHealthBar();
    }

    /// <summary>
    /// Uppdaterar bossens health bar beroden på bossens liv
    /// </summary>
    private void UpdateBossHealthBar()
    {
        if (GameManager.instance.CurrentRoom.GetComponent<Room>().Objective.IsBossObjective == true
            && GameManager.instance.GetComponent<ObjectiveController>().BossCompleted == false)
        {
            if (maxHPSet == false)
            {
                maxHP = GameManager.instance.CurrentRoom.GetComponentInChildren<EnemyBaseClass>().GetHealth();
                GetComponent<Slider>().maxValue = maxHP;
                maxHPSet = true;
            }

            GetComponent<Slider>().value = GameManager.instance.CurrentRoom.GetComponentInChildren<EnemyBaseClass>().GetHealth();

            healthPercentage = GetComponent<Slider>().value / maxHP;

            if (healthPercentage > 0.5f)
            {
                greenColorPercentage = 1;
                redColorPercentage = 1 - ((healthPercentage - 0.5f) * 2);
            }
            else
            {
                redColorPercentage = 1;
                greenColorPercentage = healthPercentage * 2;
            }

            gameObject.GetComponent<Slider>().fillRect.GetComponent<Image>().color =
                new Color(redColorPercentage, greenColorPercentage, 0, 0.5f);
        }
        else
        {
            maxHPSet = false;
        }
    }

}