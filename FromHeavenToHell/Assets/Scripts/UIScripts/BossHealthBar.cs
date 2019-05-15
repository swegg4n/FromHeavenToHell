using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    private int maxHP;
    private float healthPercentage, greenColorPercentage, redColorPercentage;
    private bool maxHPSet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.CurrentRoom.GetComponent<Room>().Objective.IsBossObjective == true
            && GameManager.instance.GetComponent<ObjectiveController>().BossCompleted == false)
        {
            if(maxHPSet == false)
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
