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
        conductor = GameObject.Find("Conductor").GetComponent<Conductor>();

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
        gameplayManager = this;
    }

    void DetectHit(Conductor c) 
    {
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

    void comboScoreUp(float score, int[] combo, int counter) {
        score = score + combo[counter];
    }

    public void onFinishLoop()
        {
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
                increaseDifficulty();
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

    void increaseDifficulty() 
    {
        difficultyLevel = difficultyLevel + 1;
        return;
    }

    public hitStatus GetHitStatus()
    {
        return myHitStatus;
    }
}
