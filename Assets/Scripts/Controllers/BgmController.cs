using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmController : MonoBehaviour
{
    private AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>("backgroundMusic");
        source.clip = clip;
        source.loop = true;
        source.volume = 0.2f;
        source.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StopBGM();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayBGM();
        }
    }

    public void PlayBGM()
    {
        source.Play();
    }

    public void StopBGM()
    {
        source.Stop();
    }
}
