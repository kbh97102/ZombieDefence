using UnityEngine;

public class WaveSound 
{
    private AudioClip win;
    private AudioClip lose;

    private AudioSource source;

    public WaveSound(AudioSource source)
    {
        this.source = source;

        win = Resources.Load<AudioClip>("winSound");
        lose = Resources.Load<AudioClip>("loseSound");

    }

    public void PlaySound(string type)
    {
        if (type.Equals("win"))
        {
            source.PlayOneShot(win);
        }
        else 
        {
            source.PlayOneShot(lose);
        }
    }
}