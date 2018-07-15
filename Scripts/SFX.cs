using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour {

    public static SFX instance;

    [SerializeField] private AudioSource TitleMusic, GameMusic, GameoverMusic;
    [SerializeField] private AudioSource hitGood, hitBad, playerHitground;
    [SerializeField] private AudioSource gato;
    [SerializeField] private AudioSource FireFar, FireClose;

    void Awake() {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void Play_TitleMusic() {
        if (TitleMusic != null && !TitleMusic.isPlaying) TitleMusic.Play();
    }
    public void Stop_TitleMusic() {
        if (TitleMusic != null && TitleMusic.isPlaying) TitleMusic.Stop();
    }

    public void Play_GameMusic()
    {
        if (GameMusic != null && !GameMusic.isPlaying) GameMusic.Play();
    }
    public void Stop_GameMusic() {
        if (GameMusic != null && GameMusic.isPlaying) GameMusic.Stop();
    }

    public void Play_GameoverMusic()
    {
        if (GameoverMusic != null && !GameoverMusic.isPlaying) GameoverMusic.Play();
    }
    public void Stop_GameoverMusic() {
        if (GameoverMusic != null && GameoverMusic.isPlaying) GameoverMusic.Stop();
    }

    public void Play_hitGood()
    {
        if (hitGood != null && !hitGood.isPlaying) hitGood.Play();
    }

    public void Play_hitBad()
    {
        if (hitBad != null && !hitBad.isPlaying) hitBad.Play();
    }

    public void Play_playerHitground()
    {
        if (playerHitground != null && !playerHitground.isPlaying) playerHitground.Play();
    }

    public void Play_gato()
    {
        if (gato != null && !gato.isPlaying) gato.Play();
    }

    public void Play_FireFar()
    {
        if (FireFar != null && !FireFar.isPlaying) FireFar.Play();
    }

    public void Play_FireClose()
    {
        if (FireClose != null && !FireClose.isPlaying) FireClose.Play();
    }
    public void Stop_FireClose() {
        if (FireClose != null && FireClose.isPlaying) FireClose.Stop();
    }

    public void Stop_Sound(AudioSource myAudio) {
        if (myAudio != null && myAudio.isPlaying) myAudio.Stop();
    }

}
