using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] soundlist;

    public static SoundManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound sound in soundlist)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();

            sound.Source.clip = sound.AudioClip;

            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;

            if (sound.PlayAtStart)
            {
                Play(sound.Name);
            }
        }
    }
    public void Play(string Name)
    {
        Sound s =  Array.Find(soundlist, Sound => Sound.Name == Name);
        s.Source.Play();
    }
    public void Stop(string Name)
    {
        Sound s = Array.Find(soundlist, Sound => Sound.Name == Name);
        s.Source.Stop();
    }
    public void Mute(string Name , bool Mute)
    {
        Sound s = Array.Find(soundlist, Sound => Sound.Name == Name);
        if (Mute)
        {
            s.Source.mute = true;
        }
        else
        {
            s.Source.mute = false;
        }
    }
    public void slowPitch(bool IsAble)
    {
        AudioSource[] newSoundList = GetComponents<AudioSource>();
        if (IsAble)
        {
            foreach (AudioSource sound in newSoundList)
            {
                sound.pitch = 0.5f;
            }
        }
        else
        {
            foreach (AudioSource sound in newSoundList)
            {
                sound.pitch = 1f;
            }
        }
    }
}
