using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    //Detects if hit state is successful or not
    private bool hitState;

    //For making the groove-tron determine if thing is successful or not
    private bool hitSuccess;

    //Keep track of scores for P1 and P2
    public float score;
    //public float P2Score;

    //Power-ups counter for P1 and P2
    private int power;
    //private int P2Power;

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
        3200, //cool
        6400, //chillin'
        128000 //freeze!
    };

    //Keeps track of failed button inputs in a beat
    public int failCounter;

    private bool isCombo;

    public static Conductor conductor;

    public static GameplayManager gameplayManager;

    public hitStatus myHitStatus;

    public GameplayUI ui;

  //  public WordManager wordManager;

    // Start is called before the first frame update
    void Start()
    {
        conductor = GameObject.Find("Conductor").GetComponent<Conductor>();

        gameplayManager = GameObject.Find("Gameplay Manager").GetComponent<GameplayManager>();

        ui = GameObject.Find("UI Manager").GetComponent<GameplayUI>();

        setHitRange();
        myHitStatus = hitStatus.hitNotDone;

        score = 0;
        
        power = 0;

        difficultyLevel = 1;
        comboCounter = 1;
        failCounter = 0;
        isCombo = false;
    }

    // Update is called once per frame
    void Update()
    {
        DetectHit();
/*
        foreach (char letter in Input.inputString)
        {
            wordManager.typeLetter(letter);
           // Debug.Log(letter);
        }*/
    }

    void DetectHit() 
    {
        //if (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyUp(KeyCode.W)))
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (conductor.loopPositionInBeats > minHitRange && conductor.loopPositionInBeats < maxHitRange && myHitStatus == hitStatus.hitNotDone)
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
    }

    void onFinishLoop()
    {
        failCounter = 0;

        if (myHitStatus == hitStatus.hitPass)
            {
                callHitSuccess();

            } 
        else if (myHitStatus != hitStatus.hitPass)
            {
                callHitFail();
            }
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
        comboCounter = comboCounter + 1;
        score = score + scoreBasedOnCombo[comboCounter];    
        if (comboCounter == 8) 
        {
            comboCounter = 1;
        }   
        ui.SendMessage("onHitSuccess");
        return;
    }

    void callHitFail()
    {
        myHitStatus = hitStatus.hitNotDone;
        failCounter = 0;
        comboCounter = 1;
        ui.SendMessage("onHitFail");
        return;
    }

    public hitStatus GetHitStatus()
    {
        return myHitStatus;
    }
}
