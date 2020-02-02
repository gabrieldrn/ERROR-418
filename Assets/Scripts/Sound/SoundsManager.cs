using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public AudioClip gameTheme;
    public AudioClip gameWon;
    public AudioClip gameLost;
    public bool loopBackgroundMusic;

    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        playBackgroundTheme();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playBackgroundTheme()
    {
        audio.clip = gameTheme;
        audio.volume = 0.3f;
        audio.loop = loopBackgroundMusic;
        audio.Play();
    }

    public void playWinTheme()
    {
        audio.clip = gameWon;
        audio.volume = 1;
        audio.Play();
    }

    public void playLooseTheme()
    {
        audio.clip = gameLost;
        audio.volume = 1;
        audio.Play();
    }
}
