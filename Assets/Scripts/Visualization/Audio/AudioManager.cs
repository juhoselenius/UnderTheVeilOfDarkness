using UnityEngine;
using UnityEngine.Audio;
using System;
using Logic.Options;

public class AudioManager : MonoBehaviour
{
    private IOptionsManager _optionsManager;
    public Sound[] sounds;
    void Awake()
    {
        _optionsManager = ServiceLocator.GetService<IOptionsManager>();
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            //s.source.volume = s.volume;
            s.source.volume = _optionsManager.getSfxVolume();
            Debug.Log("Sourcen vola:" + s.source.volume);
            s.source.pitch = s.pitch;
            s.source.spatialBlend = s.spatialBlend;
            s.source.loop = s.loop;
        }
    }

    public void Play (string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);     
        s.source.Play();
    }

    public void StopPlay(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
