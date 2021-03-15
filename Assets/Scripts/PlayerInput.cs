﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        10,
        20,
        40,
        80,
        160,
        320, //cool
        640, //chillin'
        1280, //freeze!
        2560,
        5120,
        10240
    };

    //Score multiplier based on word generator difficulty
    // TO-DO -- ADD FUNCTION IF WORD DIFFICULTY MAXES OUT TO END OF ARRAY
    private float[] scoreMultiplier =
    {
        (float)0,
        (float)1,
        (float)2,
        (float)3,
        (float)4,
        (float)5,
        (float)6,
        (float)7,
        (float)8,
        (float)9,
        (float)10,
        (float)11
    };

    //Keeps track of failed button inputs in a beat
    public int failCounter;

   // private bool isCombo;

    public static Conductor conductor;

    public static GameplayManager gameplayManager;

    public hitStatus myHitStatus;

    public GameplayUI ui;

    public WordManager wordManager;

    public WordGenerator letterGenerator;

    public SyncedAnimation dancer;

    char beatLetter;

    public bool playableState;

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
        //isCombo = false;

        beatLetter = '\n';

        playableState = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playableState == true)
        {
            foreach (char letter in Input.inputString)
            {
                DetectHit(letter);

                if (myHitStatus != hitStatus.hitFail)
                {
                    wordManager.typeLetter(letter);
                }
                Debug.Log(isLevelingUp() + " " + comboCounter.ToString() + " Difficulty: " + WordGenerator.wordDifficulty);
            }
        }
        menuButton();
    }

    void DetectHit(char c) 
    {
        if (wordManager.words.Count == 0 && c == beatLetter && conductor.loopPositionInBeats > minHitRange && conductor.loopPositionInBeats < maxHitRange && myHitStatus == hitStatus.hitNotDone)
        {   
             myHitStatus = hitStatus.hitPass;
        } 
        else if (myHitStatus != hitStatus.hitPass)
        {
            if (failCounter < 5 && wordManager.words.Count == 0)
            {
                failCounter = failCounter + 1;
            }
            if (failCounter == 5)
            {
                myHitStatus = hitStatus.hitFail;
            }
        }
    }

    void hitFail() 
    {
        if (failCounter < 5 && wordManager.words.Count != 0)
        {
            failCounter = failCounter + 1;
        }

        if (failCounter == 5)
        {
            myHitStatus = hitStatus.hitFail;
        }
    }

    public void onFinishLoop()
    {
        failCounter = 0;

        if (myHitStatus == hitStatus.hitPass && isLevelingUp() == false)
            {
                callHitSuccess();

                if (comboCounter == 9)
                {
                    unsetPlayableState();
                }
            score = score + (scoreBasedOnCombo[comboCounter] * scoreMultiplier[setMultiplier(WordGenerator.wordDifficulty)]);
            } 
        else if (myHitStatus != hitStatus.hitPass && isLevelingUp() == false)
            {
                callHitFail();
            }
        else if (isLevelingUp() == true)
            {
                setPlayableState();
                comboCounter = 1;
            }
     }

    void setHitRange()
    {
        minHitRange = 2.8f;
        maxHitRange = 3.2f;
    }

    void callHitSuccess()
    {
        if (playableState == true)
        {
            myHitStatus = hitStatus.hitNotDone;
            failCounter = 0;
            comboCounter = comboCounter + 1;
            //score = score + (scoreBasedOnCombo[comboCounter] * scoreMultiplier[setMultiplier(WordGenerator.wordDifficulty)]);    
            if (comboCounter == 10) 
            {
                comboCounter = 1;
            }   
            ui.onHitSuccess();
            dancer.onHitSuccess();
        }
        return;
    }

    void callHitFail()
    {
        if (playableState == true)
        {
            myHitStatus = hitStatus.hitNotDone;
            failCounter = 0;
            comboCounter = 1;
            ui.onHitFail();
            dancer.onHitFail();
        }
        return;
    }

    public void setLetter(char letter)
    {
        beatLetter = letter;
    }

    public int setMultiplier(int difficulty)
    {
        if (comboCounter == 9)
        {
            return difficulty - 1;
        }
        else
        {
            return difficulty;
        }
    }

    public hitStatus GetHitStatus()
    {
        return myHitStatus;
    }

    void menuButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            goToMenu();
        }
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("Song Menu");
        Resources.UnloadAsset(Conductor.songClip);
    }

    public void setPlayableState()
    {
        playableState = true;
        ui.showReadyText(0);
    }

    public void unsetPlayableState()
    {
        playableState = false;
    }

    void onLevelingUp()
    {

    }

    bool isLevelingUp()
    {
        bool levelUp;

        //combo counter 9 is the freeze phase
        if (comboCounter == 9)
        {
            return levelUp = true;
        }
        else
        {
            return levelUp = false;
        }
    }
}
