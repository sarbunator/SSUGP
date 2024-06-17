using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sprite checkedBoxSprite;
    public Sprite uncheckedBoxSprite;
    public Image musicCheckBoxImage;
    public Image sfxCheckBoxImage;

    private bool isMusicMuted = false;
    private bool areSFXMuted = false;

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

    public void Play(string[] names)
    {
        int randomIndex = UnityEngine.Random.Range(0, names.Length);
        string name = names[randomIndex];

        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void ToggleMusic()
    {
        isMusicMuted = !isMusicMuted;
        foreach (Sound s in sounds)
        {
            if (s.isMusic)
            {
                s.source.mute = isMusicMuted;
            }
        }
        PlayerPrefs.SetInt("MusicMuted", isMusicMuted ? 1 : 0);
        UpdateMusicButtonImage();
    }

    public void ToggleSoundEffects()
    {
        areSFXMuted = !areSFXMuted;
        foreach (Sound s in sounds)
        {
            if (!s.isMusic)
            {
                s.source.mute = areSFXMuted;
            }
        }
        PlayerPrefs.SetInt("SFXMuted", areSFXMuted ? 1 : 0);
        UpdateSFXButtonImage();
    }

    private void UpdateMusicButtonImage()
    {
        if (musicCheckBoxImage != null)
        {
            musicCheckBoxImage.sprite = isMusicMuted ? uncheckedBoxSprite : checkedBoxSprite;
        }
    }

    private void UpdateSFXButtonImage()
    {
        if (sfxCheckBoxImage != null)
        {
            sfxCheckBoxImage.sprite = areSFXMuted ? uncheckedBoxSprite : checkedBoxSprite;
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("MusicMuted"))
        {
            isMusicMuted = PlayerPrefs.GetInt("MusicMuted") == 1;
            UpdateMusicButtonImage();
            foreach (Sound s in sounds)
            {
                if (s.isMusic)
                {
                    s.source.mute = isMusicMuted;
                }
            }
        }

        if (PlayerPrefs.HasKey("SFXMuted"))
        {
            areSFXMuted = PlayerPrefs.GetInt("SFXMuted") == 1;
            UpdateSFXButtonImage();
            foreach (Sound s in sounds)
            {
                if (!s.isMusic)
                {
                    s.source.mute = areSFXMuted;
                }
            }
        }
    }
}
