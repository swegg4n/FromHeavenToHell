using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour
{
    private Text endText;

    // Start is called before the first frame update
    void Start()
    {
        endText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameLost == true)
        {
            endText.text = "HAHA You lost scrub git gud N00B REEEEEEEE!!! \n Press Enter To Continue";
        }
        else if (GameManager.instance.gameWon == true)
        {
            endText.text = "AWWW SNAP You Won!? Gratz i guess... Not Sure What You Expected Here... \n Press Enter To Continue";
        }
    }
}
