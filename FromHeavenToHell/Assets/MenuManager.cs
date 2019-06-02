using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject inputSelectionPanel;

    [SerializeField] private Dropdown angelDropdown;    //Dropdown för hur ängeln ska styras
    [SerializeField] private Dropdown demonDropdown;    //Dropdown för hur demonen ska styras


    private void Awake()
    {
        try
        {
            mainMenuPanel.SetActive(true);
            inputSelectionPanel.SetActive(false);
        }
        catch (System.Exception)
        {
            Debug.LogError("MenuManager>mainMenuPanel or MenuManager>inputSelectionPanel not assigned");
        }
    }

    /// <summary>
    /// Assignerar karaktärernas styrsätt berodene på vad som valts i dropdown-menyerna
    /// </summary>
    public void Play()
    {
        try
        {
            Inputs.AssignPlayer(GameManager.Objects.PlayerAngel, angelDropdown.value);
            Inputs.AssignPlayer(GameManager.Objects.PlayerDemon, demonDropdown.value);
        }
        catch (System.Exception)
        {
            Debug.LogError("Cant assing players. Make sure dropdowns are assigned to the MenuManager");
        }

        //Byter till spel-scenen (startar spelet)
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Stänger programmet
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

}
