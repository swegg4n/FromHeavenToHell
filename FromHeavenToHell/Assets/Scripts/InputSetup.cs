using UnityEngine;

public class InputSetup : MonoBehaviour
{
    private bool playerDemonJoined = false;
    private bool playerAngelJoined = false;

    //private bool[] controllerJoined = new bool[] { false, false };
    private bool keyboardJoined = false;


    #region Input
    /*Demon Inputs*/
    public string PlayerDemonHorizontalAxis { get; private set; }
    public string PlayerDemonVerticalAxis { get; private set; }
    public string PlayerDemonHorizontalAimAxis { get; private set; }
    public string PlayerDemonVerticalAimAxis { get; private set; }
    public string[] PlayerDemonFire { get; private set; }
    /*---*/

    /*Angel Inputs*/
    public string PlayerAngelHorizontalAxis { get; private set; }
    public string PlayerAngelVerticalAxis { get; private set; }
    public string PlayerAngelHorizontalAimAxis { get; private set; }
    public string PlayerAngelVerticalAimAxis { get; private set; }
    public string[] PlayerAngelFire { get; private set; }
    /*---*/
    #endregion


    #region singleton
    public static InputSetup instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            PlayerDemonFire = new string[3];
            PlayerAngelFire = new string[3];
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
        if (Input.GetButtonDown("MouseLeftClick"))
            Test();
    }


    private void SelectInputMethod()
    {
        if (playerDemonJoined == false && playerAngelJoined == false)
        {
            if (Input.GetButtonDown("R1P1"))
            {
                AssignPlayer("PlayerDemon", 1);
            }
            else if (Input.GetButtonDown("R1P2"))
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
            if (Input.GetButtonDown("R1P1"))
            {
                AssignPlayer("PlayerAngel", 1);
            }
            else if (Input.GetButtonDown("R1P2"))
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

    private void Test()
    {
        PlayerAngelHorizontalAxis = "HorizontalMouse";
        PlayerAngelVerticalAxis = "VerticalMouse";
        //Dont set aimHorizontalAxis to anything
        //Dont set aimVerticalAxis to anything
        PlayerAngelFire[0] = "MouseLeftClick";
        PlayerAngelFire[1] = "MouseRightClick";
        PlayerAngelFire[2] = "ShiftClick";

        PlayerDemonHorizontalAxis = "HorizontalMouse";
        PlayerDemonVerticalAxis = "VerticalMouse";
        //Dont set aimHorizontalAxis to anything
        //Dont set aimVerticalAxis to anything
        PlayerDemonFire[0] = "MouseLeftClick";
        PlayerDemonFire[1] = "MouseRightClick";
        PlayerDemonFire[2] = "ShiftClick";

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

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
                        PlayerDemonFire[0] = "MouseLeftClick";
                        PlayerDemonFire[1] = "MouseRightClick";
                        PlayerDemonFire[2] = "ShiftClick";

                        keyboardJoined = true;

                        Debug.Log("DEMON using keyboard");
                    }
                    else
                    {
                        PlayerDemonHorizontalAxis = $"HorizontalP{controller}";
                        PlayerDemonVerticalAxis = $"VerticalP{controller}";
                        PlayerDemonHorizontalAimAxis = $"HorizontalRightStickP{controller}";
                        PlayerDemonVerticalAimAxis = $"VerticalRightStickP{controller}";
                        PlayerDemonFire[0] = $"R1P{controller}";
                        PlayerDemonFire[1] = $"R2P{controller}";
                        PlayerDemonFire[2] = $"L2P{controller}";

                        //controllerJoined[controller - 1] = true;

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
                        PlayerAngelFire[0] = "MouseLeftClick";
                        PlayerAngelFire[1] = "MouseRightClick";
                        PlayerAngelFire[2] = "ShiftClick";

                        keyboardJoined = true;

                        Debug.Log("ANGEL using keyboard");
                    }
                    else
                    {
                        PlayerAngelHorizontalAxis = $"HorizontalP{controller}";
                        PlayerAngelVerticalAxis = $"VerticalP{controller}";
                        PlayerAngelHorizontalAimAxis = $"HorizontalRightStickP{controller}";
                        PlayerAngelVerticalAimAxis = $"VerticalRightStickP{controller}";
                        PlayerAngelFire[0] = $"R1P{controller}";
                        PlayerAngelFire[1] = $"R2P{controller}";
                        PlayerAngelFire[2] = $"L2P{controller}";

                        //controllerJoined[controller - 1] = true;

                        Debug.Log($"ANGEL using controller {controller}");
                    }

                    playerAngelJoined = true;
                }
                break;
        }
    }

}