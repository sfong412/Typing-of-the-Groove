using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

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
    public static Conductor conductor;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    AudioClip myClip;

    public PlayerInput p1;

    public PlayerInput p2;

    // Start is called before the first frame update
    void Start()
    {
        //Get song metadata
        songData = GetComponent<SongMetadata>();

        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Get player inputs
        p1 = GameObject.Find("Player 1").GetComponent<PlayerInput>();
        p2 = GameObject.Find("Player 2").GetComponent<PlayerInput>();

        //Calculate the number of seconds in each beat
        Debug.Log("Song: " + songData.title + ". The BPM is " + songBpm);
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
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
            p2.SendMessage("onFinishLoop");
        }
        loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;

        loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;
    }

    void Awake()
    {
        songData.ReadSongJSON();
        songData.UpdateSongInfo();
        songBpm = songData.bpm;
        beatsPerLoop = songData.beats;
        conductor = this;
    }
}
