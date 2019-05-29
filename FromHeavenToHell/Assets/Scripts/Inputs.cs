using UnityEngine;

public static class Inputs
{
    /*Demon Inputs*/
    public static string PlayerDemonHorizontalAxis { get; private set; }
    public static string PlayerDemonVerticalAxis { get; private set; }
    public static string PlayerDemonHorizontalAimAxis { get; private set; }
    public static string PlayerDemonVerticalAimAxis { get; private set; }
    public static string[] PlayerDemonFire { get; private set; } = new string[3];

    /*Angel Inputs*/
    public static string PlayerAngelHorizontalAxis { get; private set; }
    public static string PlayerAngelVerticalAxis { get; private set; }
    public static string PlayerAngelHorizontalAimAxis { get; private set; }
    public static string PlayerAngelVerticalAimAxis { get; private set; }
    public static string[] PlayerAngelFire { get; private set; } = new string[3];


    public static void AssignPlayer(GameManager.Objects characterType, int controller)
    {
        switch (characterType)
        {
            case GameManager.Objects.PlayerDemon:
                {
                    if (controller == 0)
                    {
                        PlayerDemonHorizontalAxis = "HorizontalMouse";
                        PlayerDemonVerticalAxis = "VerticalMouse";
                        //Dont set aimHorizontalAxis to anything
                        //Dont set aimVerticalAxis to anything
                        PlayerDemonFire[0] = "MouseLeftClick";
                        PlayerDemonFire[1] = "MouseRightClick";
                        PlayerDemonFire[2] = "Space";

                        Debug.Log("DEMON using keyboard");
                    }
                    else
                    {
                        PlayerDemonHorizontalAxis = $"HorizontalP{controller}";
                        PlayerDemonVerticalAxis = $"VerticalP{controller}";
                        PlayerDemonHorizontalAimAxis = $"HorizontalRightStickP{controller}";
                        PlayerDemonVerticalAimAxis = $"VerticalRightStickP{controller}";
                        PlayerDemonFire[0] = $"R2P{controller}";
                        PlayerDemonFire[1] = $"L2P{controller}";
                        PlayerDemonFire[2] = $"R1P{controller}";

                        Debug.Log($"DEMON using controller {controller}");
                    }
                }
                break;

            case GameManager.Objects.PlayerAngel:
                {
                    if (controller == 0)
                    {
                        PlayerAngelHorizontalAxis = "HorizontalMouse";
                        PlayerAngelVerticalAxis = "VerticalMouse";
                        //Dont set aimHorizontalAxis to anything
                        //Dont set aimVerticalAxis to anything
                        PlayerAngelFire[0] = "MouseLeftClick";
                        PlayerAngelFire[1] = "MouseRightClick";
                        PlayerAngelFire[2] = "Space";

                        //keyboardJoined = true;

                        Debug.Log("ANGEL using keyboard");
                    }
                    else
                    {
                        PlayerAngelHorizontalAxis = $"HorizontalP{controller}";
                        PlayerAngelVerticalAxis = $"VerticalP{controller}";
                        PlayerAngelHorizontalAimAxis = $"HorizontalRightStickP{controller}";
                        PlayerAngelVerticalAimAxis = $"VerticalRightStickP{controller}";
                        PlayerAngelFire[0] = $"R2P{controller}";
                        PlayerAngelFire[1] = $"L2P{controller}";
                        PlayerAngelFire[2] = $"R1P{controller}";

                        Debug.Log($"ANGEL using controller {controller}");
                    }
                }
                break;
        }
    }

}