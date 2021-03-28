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

    float songStartEvent;
    float playStartEvent;
    float songEndEvent;
    float playEndEvent;

    //the number of beats in each loop
    public float beatsPerLoop;

    public int completedBeats;

    //the total number of loops completed since the looping clip first started
    public int completedLoops = 0;

    public bool musicStarted = false;

    //The current position of the song within the loop in beats.
    public float loopPositionInBeats;

    //The current relative position of the song within the loop measured between 0 and 1.
    public float loopPositionInAnalog;

    public float beatPosition;

    //Conductor instance
    //public static Conductor conductor;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    public static AudioClip songClip;

    public static string loadedSongFile;

    public PlayerInput p1;

    public WordManager wordManager;

    public GameplayUI ui;

    public EnvironmentController environment;

    Scene scene;

    //fix song loading thing
    void Awake()
    {
        //loadedSongFile = "Bust a Groove OST - Kitty N";
        SongMetadata.ReadSongJSON(loadedSongFile);
        SongMetadata.UpdateSongInfo();
        setEvents();
    }

    void OnEnable()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get player inputs
        p1 = GameObject.Find("Player 1").GetComponent<PlayerInput>();

        wordManager = GameObject.Find("Word Manager").GetComponent<WordManager>();

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        StartCoroutine(countDown());
    }
    
    // Update is called once per frame
    void Update()
    {        
        if (musicStarted == false)
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
            environment.onFinishLoop();
            wordManager.addWord();
        }
        loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;

        loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;

        checkEvents();
    }

    void startMusic()
    {
        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        musicSource.Play();

        if (musicSource.isPlaying == true)
        {
            musicStarted = true;
        }
    }

    IEnumerator countDown()
    {
        yield return new WaitForSeconds(1f);
        //announcementAnimator.SetTrigger("getReady");
        for (int i=0; i<1; i++)
        {
            //Debug.Log("Countdown " + i.ToString());
            yield return new WaitForSeconds(1f);
        }
        startMusic();
        ui.showReadyText(1);
    }

    public static void setFileName(string _fileName)
    {
        loadedSongFile = _fileName;
    }

    void setEvents()
    {
        songBpm = SongMetadata.bpm;
        beatsPerLoop = SongMetadata.beats;
        songClip = Resources.Load<AudioClip>("Sounds/" + loadedSongFile);
        musicSource.clip = songClip;

        songStartEvent = SongMetadata.songStart;
        firstBeatOffset = (float)SongMetadata.offset;
        playStartEvent = SongMetadata.playStart;
        playEndEvent = SongMetadata.playEnd;
        songEndEvent = SongMetadata.songEnd;
    }

    void checkEvents()
    {
        //song starts
        if (completedLoops == playStartEvent)
        {
            p1.setPlayableState();
        }

        //song ends
        if (completedLoops == playEndEvent)
        {
            p1.playableState = false;
            ui.recordScore(SongMetadata.fileName);
        }

        if (completedLoops == songEndEvent)
        {
            // -- TO ADD: record score function --
            p1.goToMenu();
            Resources.UnloadAsset(Conductor.songClip);
            WordGenerator.wordDifficulty = 1;
        }
    }
}
