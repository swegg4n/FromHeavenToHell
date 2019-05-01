using UnityEngine;

public class InputSetup : MonoBehaviour
{
    private bool playerDemonJoined = false;
    private bool playerAngelJoined = false;

    private bool[] controllerJoined = new bool[] { false, false };
    private bool keyboardJoined = false;


    #region Input
    /*Demon Inputs*/
    public string PlayerDemonHorizontalAxis { get; private set; }
    public string PlayerDemonVerticalAxis { get; private set; }
    public string PlayerDemonHorizontalAimAxis { get; private set; }
    public string PlayerDemonVerticalAimAxis { get; private set; }
    public string PlayerDemonFire { get; private set; }
    /*---*/

    /*Angel Inputs*/
    public string PlayerAngelHorizontalAxis { get; private set; }
    public string PlayerAngelVerticalAxis { get; private set; }
    public string PlayerAngelHorizontalAimAxis { get; private set; }
    public string PlayerAngelVerticalAimAxis { get; private set; }
    public string PlayerAngelFire { get; private set; }
    /*---*/
    #endregion


    #region singleton
    public static InputSetup instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion


    void Update()
    {
        SelectInputMethod();
    }


    private void SelectInputMethod()
    {
        if (playerDemonJoined == false && playerAngelJoined == false)
        {
            if (Input.GetButtonDown("R1P1") && controllerJoined[0] == false)
            {
                AssignPlayer("PlayerDemon", 1);
            }
            else if (Input.GetButtonDown("R1P2") && controllerJoined[1] == false)
            {
                AssignPlayer("PlayerDemon", 2);
            }
            else if (Input.GetButtonDown("MouseLeftClick") && keyboardJoined == false)
            {
                AssignPlayer("PlayerDemon", -1);
            }
        }
        else if (playerDemonJoined == true && playerAngelJoined == false)
        {
            if (Input.GetButtonDown("R1P1") && controllerJoined[0] == false)
            {
                AssignPlayer("PlayerAngel", 1);
            }
            else if (Input.GetButtonDown("R1P2") && controllerJoined[1] == false)
            {
                AssignPlayer("PlayerAngel", 2);
            }
            else if (Input.GetButtonDown("MouseLeftClick") && keyboardJoined == false)
            {
                AssignPlayer("PlayerAngel", -1);
            }
        }
        else if (playerDemonJoined == true && playerAngelJoined == true)
        {
            /*TEMP - vill nog ha en "gå vidare"-knapp sen som gör detta*/
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            enabled = false;
        }

    }

    private void AssignPlayer(string playerTag, int controller)
    {
        switch (playerTag)
        {
            case "PlayerDemon":
                {
                    if (controller == -1)
                    {
                        PlayerDemonHorizontalAxis = "HorizontalMouse";
                        PlayerDemonVerticalAxis = "VerticalMouse";
                        //Dont set aimHorizontalAxis to anything
                        //Dont set aimVerticalAxis to anything
                        PlayerDemonFire = "MouseLeftClick";

                        keyboardJoined = true;

                        Debug.Log("DEMON using keyboard");
                    }
                    else
                    {
                        PlayerDemonHorizontalAxis = $"HorizontalP{controller}";
                        PlayerDemonVerticalAxis = $"VerticalP{controller}";
                        PlayerDemonHorizontalAimAxis = $"HorizontalRightStickP{controller}";
                        PlayerDemonVerticalAimAxis = $"VerticalRightStickP{controller}";
                        PlayerDemonFire = $"R1P{controller}";

                        controllerJoined[controller - 1] = true;

                        Debug.Log($"DEMON using controller {controller}");
                    }

                    playerDemonJoined = true;
                }
                break;

            case "PlayerAngel":
                {
                    if (controller == -1)
                    {
                        PlayerAngelHorizontalAxis = "HorizontalMouse";
                        PlayerAngelVerticalAxis = "VerticalMouse";
                        //Dont set aimHorizontalAxis to anything
                        //Dont set aimVerticalAxis to anything
                        PlayerAngelFire = "MouseLeftClick";

                        keyboardJoined = true;

                        Debug.Log("ANGEL using keyboard");
                    }
                    else
                    {
                        PlayerAngelHorizontalAxis = $"HorizontalP{controller}";
                        PlayerAngelVerticalAxis = $"VerticalP{controller}";
                        PlayerAngelHorizontalAimAxis = $"HorizontalRightStickP{controller}";
                        PlayerAngelVerticalAimAxis = $"VerticalRightStickP{controller}";
                        PlayerAngelFire = $"R1P{controller}";

                        controllerJoined[controller - 1] = true;

                        Debug.Log($"ANGEL using controller {controller}");
                    }

                    playerAngelJoined = true;
                }
                break;
        }
    }

}