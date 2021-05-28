using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSoundController
{
    private AudioClip fireSound;
    private AudioClip reloadSound;

    private Dictionary<string, AudioClip> soundMap;

    private AudioSource source;

    public FireSoundController(AudioSource source)
    {
        this.source = source;

        fireSound = Resources.Load<AudioClip>("fireSound");
        reloadSound = Resources.Load<AudioClip>("reloadSound");

        soundMap = new Dictionary<string, AudioClip>();
        soundMap.Add("fire", fireSound);
        soundMap.Add("reload", reloadSound);
    }


    public void PlaySound(string type)
    {
        source.PlayOneShot(soundMap[type]);
    }
}