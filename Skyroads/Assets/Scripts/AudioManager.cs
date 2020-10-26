using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _effectSource;
    [SerializeField] private Sound[] soundEffect;
    [SerializeField] private Sound[] soundBg;
    
    private void Awake()
    {
        Init(_effectSource,soundEffect);
        Init(gameObject.AddComponent<AudioSource>(),soundBg);
    }

    private void Start()
    {
       PlayMusicInBackGround();
    }
    private void Init(AudioSource source, Sound[] sounds)
    {
        foreach (var s in sounds)
        {
            s.source = source;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.isLoop;
        }
    }

    private void PlayMusicInBackGround()
    {
        foreach (var s in soundBg)
            s.source.Play();
    }
    public void PlaySound(string name)
    {
        var s = Array.Find(soundEffect, sound => sound.name == name);
        if (s == null)
            return;
        _effectSource.enabled = true;
        _effectSource.PlayOneShot(s.clip);
    }

    public void StopSound()
    {
        _effectSource.enabled = false;
    }

    public void SliderBgSounds(Slider slider)
    {
        foreach (var s in soundBg)
            s.source.volume = slider.value;
    }

    public void SliderBgEffect(Slider slider)
    {
        _effectSource.volume = slider.value;
    }
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public AudioSource source;
    [Range(0, 1f)] public float volume;
    public bool isLoop;
}