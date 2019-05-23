using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitbuttonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitButton()
    {
        Debug.Log("App has quit");
        Application.Quit();
    }
}
