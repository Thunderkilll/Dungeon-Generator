using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    #region Attributes

    [Header("Audio Sources")]
    [Tooltip("Source that handles background music of the level")]
    public AudioSource levelMusic;
    [Tooltip("Source that handles Game Over music")]
    public AudioSource gameOverMusic;
    [Tooltip("Source that handles Winning music")]
    public AudioSource winMusic;
    #endregion

    #region singleton

    public static AudioManager instance;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            // DontDestroyOnLoad(this);
        }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This function play game over music when player dies
    /// </summary>
    public void PlayGameOverMusic()
    {
        levelMusic.Stop();
        gameOverMusic.Play();
    }
    /// <summary>
    /// This function play win music track when player wins against boss or win in the game
    /// </summary>
    public void PlayWinMusic()
    {
        levelMusic.Stop();
        winMusic.Play();
    }

}
