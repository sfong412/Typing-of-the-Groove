using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Made it global for Gameplay UI script. Might change organization of this*/
public enum hitStatus
    {
        hitPass,
        hitFail,
        hitNotDone
    }
    
public class GameplayManager : MonoBehaviour
{

    //Reference to the songMetadata script
    public SongMetadata songData;

    //Song beats per minute
    //This is determined by the song you're trying to sync up to
   // public float songBpm;

    //The number of seconds for each song beat
   // public float secPerBeat;

    //Current song position, in seconds
   // public float songPosition;

    //Current song position, in beats
   // public float songPositionInBeats;

    //How many seconds have passed since the song started
   // public float dspSongTime;

    //The offset to the first beat of the song in seconds
   // public float firstBeatOffset;

    //the number of beats in each loop
  //  public float beatsPerLoop;

    //the total number of loops completed since the looping clip first started
   // public int completedLoops = 0;

    //The current position of the song within the loop in beats.
   // public float loopPositionInBeats;

    //The current relative position of the song within the loop measured between 0 and 1.
   // public float loopPositionInAnalog;

    //Conductor instance
    public static Conductor conductor;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    AudioClip myClip;

    //Is gameplay on
    private bool isGameplay;
   
    //Detects if hit state is successful or not
    private bool hitState;

    //For making the groove-tron determine if thing is successful or not
    private bool hitSuccess;
/*
    //For checking if the 4th beat key is pressed once
    public enum hitStatus
        {
            hitPass,
            hitFail,
            hitNotDone
        }
*/

    private int[] numberOfPlayers =
    {
        1,
        2
    };

    //Keep track of scores for P1 and P2
    public float P1Score;
    public float P2Score;

    //Power-ups counter for P1 and P2
    private int P1Power;
    private int P2Power;

    //Hit range for the fourth beat timing
    private float minHitRange;
    private float maxHitRange;

    //Keeps track of diffculty of button presses before 4th beat
    private int difficultyLevel;

    //Keeps track of player's combo counter
    public int comboCounter;

    //Score distribution based on player's combo counter
    private int[] scoreBasedOnCombo = 
    {
        0,
        100,
        200,
        400,
        800,
        1600,
        3200,
        6400
    };

    //Keeps track of failed button inputs in a beat
    public int failCounter;

    private bool isCombo;
    
    //For score addition number based on combo conunter
    //private float addScore;

    public static GameplayManager gameplayManager;

    public PlayerInput p1;

    public PlayerInput p2;

    public hitStatus myHitStatus;

    //public List<Word> words;

    // Start is called before the first frame update
    void Start()
    {
        //Get song metadata
        //songData = GetComponent<SongMetadata>();

        //Load the AudioSource attached to the Conductor GameObject
       // musicSource = GetComponent<AudioSource>();

        conductor = GameObject.Find("Conductor").GetComponent<Conductor>();

        //p1 = GameObject.Find("Player 1").GetComponent<PlayerInput>();
        //p2 = GameObject.Find("Player 2").GetComponent<PlayerInput>();

        //Calculate the number of seconds in each beat
       // Debug.Log("Song: " + songData.title + ". The BPM is " + songBpm);
        //secPerBeat = 60f / songBpm;

        //Record the time when the music starts
       // dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
       // musicSource.Play();

        setHitRange();
        myHitStatus = hitStatus.hitNotDone;

        P1Score = 0;
        P2Score = 0;

        P1Power = 0;
        P2Power = 0;

        difficultyLevel = 1;
        comboCounter = 1;
        failCounter = 0;
        isCombo = false;
    }

    // Update is called once per frame
    void Update()
    {
       // trackPlayback();
       // DetectHit(conductor);
    }

    void FixedUpdate() 
    {
    }

    void Awake()
    {
       // songData.ReadSongJSON();
       // songData.UpdateSongInfo();
      //  songBpm = songData.bpm;
      //  beatsPerLoop = songData.beats;
        gameplayManager = this;
    }

    void DetectHit(Conductor c) 
    {
        //if (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyUp(KeyCode.W)))
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (c.loopPositionInBeats > minHitRange && c.loopPositionInBeats < maxHitRange && myHitStatus == hitStatus.hitNotDone)
                {   
                    myHitStatus = hitStatus.hitPass;
                } else if (myHitStatus != hitStatus.hitPass)
                {
                    if (failCounter < 5)
                    {
                        failCounter = failCounter + 1;
                    }
                    if (failCounter == 5)
                    {
                        myHitStatus = hitStatus.hitFail;
                    }
                }
        }
            if (myHitStatus == hitStatus.hitPass)
                {
                    callHitSuccess();
                } 
            else if (myHitStatus != hitStatus.hitPass)
                {
                    callHitFail();
                }
    }
            /*
         //   failCounter = 0;

            if (myHitStatus == hitStatus.hitPass)
                {
           //         callHitSuccess();
                } 
            else if (myHitStatus != hitStatus.hitPass)
                {
            //        callHitFail();
                }
    }
/*
    void trackPlayback()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        //calculate the loop position
        if (songPositionInBeats >= (completedLoops + 1) * beatsPerLoop)
        {
            onFinishLoop();
        }
        loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;

        loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;
    }
*/
    void comboScoreUp(float score, int[] combo, int counter) {
        score = score + combo[counter];
    }

    public void onFinishLoop()
        {
           // completedLoops++;

            //p1.SendMessage("onFinishLoop");
            //p2.SendMessage("onFinishLoop");
/*
            fail = 0;

            if (status == hitStatus.hitPass)
                {
           //         callHitSuccess();
                } 
            else if (status != hitStatus.hitPass)
                {
            //        callHitFail();
                }
            /*
         //   failCounter = 0;

            if (myHitStatus == hitStatus.hitPass)
                {
           //         callHitSuccess();
                } 
            else if (myHitStatus != hitStatus.hitPass)
                {
            //        callHitFail();
                }
            */
        }

    void setHitRange()
    {
        minHitRange = 2.8f;
        maxHitRange = 3.2f;
    }


    void callHitSuccess()
    {
        myHitStatus = hitStatus.hitNotDone;
        failCounter = 0;

        if (comboCounter > 7) 
            {
                comboCounter = 1;
            }
        else 
            {
                comboCounter = comboCounter + 1;
                //comboScoreUp(P1Score, scoreBasedOnCombo, comboCounter);
                P1Score = P1Score + scoreBasedOnCombo[comboCounter];
            }
        return;
    }

    void callHitFail()
    {
        myHitStatus = hitStatus.hitNotDone;
        failCounter = 0;
        comboCounter = 1;
        return;
    }

    void checkNumberOfPlayers(int players) 
    {
        
    }

    public hitStatus GetHitStatus()
    {
        return myHitStatus;
    }
}
