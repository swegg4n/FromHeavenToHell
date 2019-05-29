using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject inputSelectionPanel;

    [SerializeField] private Dropdown angelDropdown;
    [SerializeField] private Dropdown demonDropdown;


    private void Awake()
    {
        try
        {
            mainMenuPanel.SetActive(true);
            inputSelectionPanel.SetActive(false);
        }
        catch
        {
            Debug.LogError("MenuManager>mainMenuPanel or MenuManager>inputSelectionPanel not assigned");
        }

    }

    public void Play()
    {
        try
        {
            Inputs.AssignPlayer("PlayerAngel", angelDropdown.value);
            Inputs.AssignPlayer("PlayerDemon", demonDropdown.value);
        }
        catch
        {
            Debug.LogError("Cant assing players. Make sure dropdowns are assigned to the MenuManager");
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
