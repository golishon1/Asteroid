using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
   [SerializeField] private Sound[] sound;

    private void Awake()
    {
        foreach(Sound s in sound)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.isLoop;
        }
    }

    private void Start()
    {
        foreach (Sound s in sound)
        {
            if (s.isBackGround)
            {
                s.source.Play();
            }
        }
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        if (s.source == null)
            return;
        s.source.enabled = true;
        s.source.Play();
        
    }
    public void StopSound(string name)
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        if (s.source == null)
            return;
        s.source.enabled = false;
    }

    public void SliderBgSounds(Slider slider)
    {
        foreach (var s in sound)
        {
            if (s.isBackGround)
            {
                s.source.volume = slider.value;
            }
        }
    }
    public void SliderBgEffect(Slider slider)
    {
        foreach (var s in sound)
        {
            if (!s.isBackGround)
            {
                s.source.volume = slider.value;
            }
        }
    }
    
 
    
    
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public AudioSource source;
    [Range(0,1f)]
    public float volume;
    public bool isLoop;
    public bool isBackGround;
}
