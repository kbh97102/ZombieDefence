using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieSoundController
{
    public enum ZombieSounds
    {
        Attack,
        Attacked,
        Idle,
        Death
    }
    
    private AudioClip idleSound;
    private AudioClip attackSound;
    private AudioClip attackedSound;
    private AudioClip deathSound;
    
    private AudioSource audioSource;

    private Dictionary<ZombieSounds, AudioClip> soundMap;
    
    public ZombieSoundController(AudioSource audioSource)
    {
        this.audioSource = audioSource;
        InitClip();

        soundMap = new Dictionary<ZombieSounds, AudioClip>();
        soundMap.Add(ZombieSounds.Attack, attackSound);
        soundMap.Add(ZombieSounds.Attacked, attackedSound);
        soundMap.Add(ZombieSounds.Idle, idleSound);
        soundMap.Add(ZombieSounds.Death, deathSound);
    }

    private void InitClip()
    {
        idleSound = Resources.Load<AudioClip>("idleSound");
        attackSound = Resources.Load<AudioClip>("attackSound");
        deathSound = Resources.Load<AudioClip>("deathSound");
        attackedSound = Resources.Load<AudioClip>("attackedSound");
    }

    public void PlaySound(ZombieSounds type)
    {
        AudioClip clip = soundMap[type];
        if (clip == null)
        {
            return;
        }
        audioSource.PlayOneShot(clip);
    }    
}
