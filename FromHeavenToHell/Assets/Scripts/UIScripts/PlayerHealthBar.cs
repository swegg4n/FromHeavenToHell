using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private int maxHp;  //Spelarnas maximala liv
    private float healthPercentage, greenColorPercentage, redColorPercentage;   //Spelarnas nuvarnade liv     //Andel som ska vara grön   //Andel som ska vara röd


    void Start()
    {
        maxHp = PlayerManager.instance.Health;
        GetComponent<Slider>().maxValue = maxHp;
    }

    /// <summary>
    /// Uppdaterar spelarnas health bar beroden på deras liv
    /// </summary>
    void Update()
    {
        GetComponent<Slider>().value = PlayerManager.instance.Health;

        healthPercentage = PlayerManager.instance.Health / (float)maxHp;

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

        gameObject.GetComponent<Slider>().fillRect.GetComponent<Image>().color = new Color(redColorPercentage, greenColorPercentage, 0, 0.5f);
    }
}
