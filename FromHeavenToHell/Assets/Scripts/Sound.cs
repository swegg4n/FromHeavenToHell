using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;  //Ljud-clippet som ska spelas

    [Range(0f, 1f)]
    public float volume;    //Ljud-clippets volym
    [Range(0.1f, 3f)]
    public float pitch;     //Ljud-clippets pitch

    public bool loop;       //Om ljudet ska loopas eller inte

    [HideInInspector]
    public AudioSource source;  //Ljudspelaren som ska spela ljud-clippet

}
