using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sfx : MonoBehaviour
{
    AudioSource sfxSource;
    public AudioClip sfxButton;
    private void Awake()
    {
        if (this.GetComponent<AudioSource>() != null)
        {
            sfxSource = this.GetComponent<AudioSource>();
        }
    }
    public void playSfx(AudioClip sfx)
    {
        if (sfx != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(sfx);
        }
    }

    public void playSfx()
    {
        playSfx(sfxButton);
    }
}
