using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;  //Ljud som ska spelas


    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    /// <summary>
    /// Spelar ett ljud
    /// </summary>
    /// <param name="name">ljudet som ska spelas namn</param>
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: \"{name}\" wasnt found");
            return;
        }
        s.source.Play();
    }
}
