using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    #region Attributes
    [Header("General Settings")]
    public GameObject player;
  
    [Tooltip("Source that handles Game Over music")]
    public AudioSource gameOverMusic;
    [Tooltip("Source that handles Winning music")]
    public AudioSource winMusic;

    [Tooltip("list of all sound effects in the level")]
    public AudioSource[] sfx;

    [Header("Player General SFX")]
    [Tooltip("clips of player breathing in case of emergencies")]
    public AudioClip[] sfxBreathingPlayer;


    #endregion

    #region singleton

    public static AudioManager instance;



    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        
        }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
       PlayLevelMusic();
       PlayBreathing();
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    /// <summary>
    /// This function play game over music when player dies
    /// </summary>
    public void PlayGameOverMusic()
    {
        MusicManager.instance.StopMusic();
        gameOverMusic.Play();
    }
    /// <summary>
    /// This function play win music track when player wins against boss or win in the game
    /// </summary>
    public void PlayWinMusic()
    {
        MusicManager.instance.StopMusic();
        winMusic.Stop();
        gameOverMusic.Stop();
        
        foreach (var item in sfx)
        {
            item.Stop();
        }
        winMusic.Play();
    }
    /// <summary>
    /// Play the normal background music of this level
    /// </summary>
    public void PlayLevelMusic()
    {

        MusicManager.instance.StartMusic();

    }
    /// <summary>
    /// Play the sound effect from the sfx list by indicating the index of the track we want to play
    /// </summary>
    /// <param name="index"> index of the track in the list of sfx sounds</param>
    public void PlaySFX(int index)
    {
        sfx[index].Stop();
        sfx[index].Play();
    }

    public void StopAllMusic()
    {
        MusicManager.instance.StopMusic();
        gameOverMusic.Stop();
        winMusic.Stop();
    }
    public void PlayBreathing()
    {
        AudioSource playerAudioBreath = player.GetComponent<AudioSource>();
        switch (GameStateManager.currentState)
        {
            case GameState.Explore:
                playerAudioBreath.clip = sfxBreathingPlayer[0];
                playerAudioBreath.Stop();
                playerAudioBreath.Play();
                break;
            case GameState.Warning:
                playerAudioBreath.clip = sfxBreathingPlayer[1];
                playerAudioBreath.Stop();
                playerAudioBreath.Play();
                break;
            case GameState.Combat:
                playerAudioBreath.clip = sfxBreathingPlayer[2];
                playerAudioBreath.Stop();
                playerAudioBreath.Play();
                break;
           
                
               
        }
        playerAudioBreath.clip = sfxBreathingPlayer[0];
        playerAudioBreath.Stop();
        playerAudioBreath.Play();
    }

    //public void PlayTrack(int index)
    //{
         
    //}
}
