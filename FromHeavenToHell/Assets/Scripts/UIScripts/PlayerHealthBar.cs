using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private int maxHp;
    private float healthPercentage, greenColorPercentage, redColorPercentage;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = PlayerManager.instance.GetHealth();
        GetComponent<Slider>().maxValue = maxHp;
        GetComponent<Slider>().value = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        //healthBarSlider.value = PlayerManager.instance.GetHealth();
        GetComponent<Slider>().value -= 1;

        //healthPercentage = PlayerManager.instance.GetHealth() / maxHp;
        healthPercentage = GetComponent<Slider>().value / maxHp;

        if(healthPercentage > 0.5f)
        {
            greenColorPercentage = 1;
            redColorPercentage = 1 - ((healthPercentage - 0.5f) * 2);
        }
        else
        {
            redColorPercentage = 1;
            greenColorPercentage = healthPercentage * 2;
        }

        GetComponent<Slider>().fillRect.GetComponent<Image>().color = new Color(redColorPercentage, greenColorPercentage, 0);
    }
}
