using UnityEngine;

public class MusicManager : MonoBehaviour
{
    
    AudioSource source;
    //Setup sources and clips
    public AudioSource musicSource;
    public AudioClip musicStart;
    public AudioClip musicStop;
    //used to determine the timing for the next clip in the sequence
    public float currentClipLength;


    //the array of music to play in the level, is set by dragging relevant clips to inspector slot
    public AudioClip[] nextClip;

    #region singleton

    public static MusicManager instance;



    void Awake()
    {
        source = GetComponent<AudioSource>();
      
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

    public void StartMusic()
    {
        //check to make sure the music isn't already playing
        if (!musicSource.isPlaying)
        {
            //ensure the correct starting clip is ready to play
            musicSource.clip = musicStart;
            //play the music
            musicSource.Play();

            //prepare to play the next clip once this clip finishes
            currentClipLength = musicSource.clip.length;
            Invoke("SequenceMusic", currentClipLength);
        }
    }

    //used as our jukebox/playlist system of music
    void SequenceMusic()
    {
        //the array of music to play in the level, is set by dragging relevant clips to inspector slot
        musicSource.clip = nextClip[Random.Range(0, nextClip.Length)];

        //musicSource.clip = nextClip;
        currentClipLength = musicSource.clip.length;
        musicSource.Play();
        Invoke("SequenceMusic", currentClipLength);
    }

    //used when re-entering the level after exiting
    public void ResetMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    //used when using the exit door to leave the level
    public void StopMusic()
    {
        //check to make sure the music is playing
        if (musicSource.isPlaying)
        {
            //stop the music
            musicSource.Stop();
            //play the tail of the music
            musicSource.clip = musicStop;
            musicSource.Play();

            //Stop calling the music sequence once we leavwe the level
            CancelInvoke("SequenceMusic");
        }
    }

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (!isExitTrigger)
    //    {
    //        if (col.gameObject.name == "Player")
    //        {
    //            AudioManager.instance.StopAllMusic();
    //            if (!clip.isPlaying)
    //            {
    //                clip.Play();
    //            }

    //        }
    //    }
    //    else
    //    {
    //        clip.Stop();
    //        AudioManager.instance.PlayLevelMusic();
    //        if (clip.isPlaying)
    //        {
    //            clip.Stop();
    //        }

    //    }
    //}

}
