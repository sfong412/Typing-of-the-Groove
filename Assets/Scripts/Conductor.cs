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

    public static AudioClip songClip;

    public static string loadedSongFile;

    public PlayerInput p1;

    public WordManager wordManager;

    Scene scene;

    //fix song loading thing
    void Awake()
    {
       // scene = SceneManager.GetActiveScene();
        //Debug.Log(scene.name);

        //loadedSongFile = "Bust a Groove OST - Kitty N";
        
        

        //conductor = this;
        
    }

    void OnEnable()
    {
        if (GameplayManager.isGameplay() == true)
        {

        }
        else
        {

        }
    }


    // BUG: desync happens when loading the song for the first time [first 2 bars for 1st time]
    
    // Start is called before the first frame update
    void Start()
    {
        if (GameplayManager.isGameplay() == true)
        {
            SongMetadata.ReadSongJSON(loadedSongFile);
            SongMetadata.UpdateSongInfo();
        }

        //Get player inputs
        if (GameplayManager.isGameplay() == true)
        {
            p1 = GameObject.Find("Player 1").GetComponent<PlayerInput>();

            wordManager = GameObject.Find("Word Manager").GetComponent<WordManager>();

            songBpm = SongMetadata.bpm;
            beatsPerLoop = SongMetadata.beats;
            songClip = Resources.Load<AudioClip>("Sounds/" + loadedSongFile);

            //Calculate the number of seconds in each beat
            secPerBeat = 60f / songBpm;

            //Record the time when the music starts
            dspSongTime = (float)AudioSettings.dspTime;

            musicSource.clip = songClip;

            //Start the music
            musicSource.Play();
        }
        else
        {
            p1 = null;
            wordManager = null;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        setEvents();

        //only start if music is playing
        if (musicSource.isPlaying == false)
        {
            return;
        }

        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        //calculate the loop position
        if (songPositionInBeats >= (completedLoops + 1) * beatsPerLoop)
        {
            completedLoops++;

            p1.onFinishLoop();
            wordManager.addWord();
        }

        loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;

        loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;
    }

    public static void setFileName(string _fileName)
    {
        loadedSongFile = _fileName;
    }

    void setEvents()
    {
        float songStartEvent = SongMetadata.songStart;
        firstBeatOffset = (float)SongMetadata.offset;
        float playStartEvent = SongMetadata.playStart;
        float playEndEvent = SongMetadata.playEnd;
        float songEndEvent = SongMetadata.songEnd;

        //song starts
        if (completedLoops == playStartEvent)
        {
            p1.setPlayableState();
        }

        //song ends
        if (completedLoops == playEndEvent)
        {
            p1.playableState = false;
        }

        if (completedLoops == songEndEvent)
        {
            // -- TO ADD: record score function --
            p1.goToMenu();
            Resources.UnloadAsset(Conductor.songClip);
            WordGenerator.wordDifficulty = 1;
        }
    }

    public IEnumerator waitAndPlay()
    {
        // Wait a frame so every Awake and Start method is called
        //yield return new WaitForEndOfFrame();

        while (SceneManager.GetActiveScene().name == "Song Gameplay")
        {
          yield return new WaitForSeconds(5);
        }
    }
}
