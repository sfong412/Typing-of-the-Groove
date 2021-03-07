using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Conductor : MonoBehaviour
{
    //Reference to the songMetadata script
    public SongMetadata songData;

    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;

    //the number of beats in each loop
    public float beatsPerLoop;

    //the total number of loops completed since the looping clip first started
    public int completedLoops = 0;

    //The current position of the song within the loop in beats.
    public float loopPositionInBeats;

    //The current relative position of the song within the loop measured between 0 and 1.
    public float loopPositionInAnalog;

    //Conductor instance
    //public static Conductor conductor;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    AudioClip songClip;

    public static string loadedSongFile;

    public PlayerInput p1;

    public WordManager wordManager;

    void Awake()
    {
        //loadedSongFile = "Bust a Groove OST - Kitty N";
        
        if (GameplayManager.isGameplay() == true)
        {
            SongMetadata.ReadSongJSON(loadedSongFile);
            SongMetadata.UpdateSongInfo();
        }

        
        
        //conductor = this;
        
    }

    void OnEnable()
    {
        if (GameplayManager.isGameplay() == true)
        {
            p1 = GameObject.Find("Player 1").GetComponent<PlayerInput>();

            wordManager = GameObject.Find("Word Manager").GetComponent<WordManager>();

            songBpm = SongMetadata.bpm;
            beatsPerLoop = SongMetadata.beats;
            songClip = Resources.Load<AudioClip>("Sounds/" + loadedSongFile);

            //Calculate the number of seconds in each beat
            //Debug.Log("Song: " + songData.title + ". The BPM is " + songBpm);
            secPerBeat = 60f / songBpm;

            //Record the time when the music starts
            dspSongTime = (float)AudioSettings.dspTime;

            musicSource.clip = songClip;
        }
        else
        {
            p1 = null;
            wordManager = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get player inputs
        if (GameplayManager.isGameplay() == true && SceneManager.GetActiveScene().name == "Song Gameplay" && SceneManager.GetActiveScene().isLoaded == true)
        {
            /*
            p1 = GameObject.Find("Player 1").GetComponent<PlayerInput>();

            wordManager = GameObject.Find("Word Manager").GetComponent<WordManager>();

            //Calculate the number of seconds in each beat
            //Debug.Log("Song: " + songData.title + ". The BPM is " + songBpm);
            secPerBeat = 60f / songBpm;

            //Record the time when the music starts
            dspSongTime = (float)AudioSettings.dspTime;

            musicSource.clip = songClip;
            */

            //Start the music
            musicSource.Play();
        }
        else
        {
            //p1 = null;
            //wordManager = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameplayManager.isGameplay() == true && SceneManager.GetActiveScene().name == "Song Gameplay" && SceneManager.GetActiveScene().isLoaded == true)
        {
            //determine how many seconds since the song started
            songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

            //determine how many beats since the song started
            songPositionInBeats = songPosition / secPerBeat;

            //calculate the loop position
            if (songPositionInBeats >= (completedLoops + 1) * beatsPerLoop)
            {
                completedLoops++;

                p1.SendMessage("onFinishLoop");
                wordManager.SendMessage("addWord");
            }
            loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;

            loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;

            setEvents();
        }
    }



    public static void setFileName(string _fileName)
    {
        loadedSongFile = _fileName;
    }

    void setEvents()
    {
        // -- find a way to convery song start position to first beat offset variable --
        float songStartEvent = SongMetadata.songStart;
        firstBeatOffset = (float)SongMetadata.offset;
        float playStartEvent = SongMetadata.playStart;
        float playEndEvent = SongMetadata.playEnd;
        float songEndEvent = SongMetadata.songEnd;

        //song starts
        if (completedLoops == playStartEvent)
        {
            //p1.playableState = true;
            p1.SendMessage("setPlayableState");
        }

        //song ends
        if (completedLoops == playEndEvent)
        {
            p1.playableState = false;
        }

        if (completedLoops == songEndEvent)
        {
            // -- record score function --
            p1.goToMenu();
        }
    }
}
