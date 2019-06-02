using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public void BackButton()
    {
        GameManager.instance.Paused = false;
    }
}
