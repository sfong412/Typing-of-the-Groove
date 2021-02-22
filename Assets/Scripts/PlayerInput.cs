using System.Collections;
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
        0,
        0,
        0
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

    public Animator dancer;

    char beatLetter;

    public bool playableState;

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
                wordManager.typeLetter(letter);
                //Debug.Log(wordManager.words.Count);
            }
        }
        menuButton();
    }

    void DetectHit(char c) 
    {
        //checks if active word is typed, the correct key is pressed & is on the correct 4th beat timing
        if (wordManager.words.Count == 0 && c == beatLetter && conductor.loopPositionInBeats > minHitRange && conductor.loopPositionInBeats < maxHitRange && myHitStatus == hitStatus.hitNotDone)
        {   
             myHitStatus = hitStatus.hitPass;
        } 
        else if (myHitStatus != hitStatus.hitPass)
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

    void hitFail() {
      //  failCounter = failCounter + 1;

        if (failCounter == 5)
        {
            myHitStatus = hitStatus.hitFail;
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
        if (playableState == true)
        {
            myHitStatus = hitStatus.hitNotDone;
            failCounter = 0;
            comboCounter = comboCounter + 1;
            score = score + scoreBasedOnCombo[comboCounter];    
            if (comboCounter == 9) 
            {
                comboCounter = 1;
            }   
            ui.SendMessage("onHitSuccess");
            dancer.SendMessage("onHitSuccess");
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
            ui.SendMessage("onHitFail");
            dancer.SendMessage("onHitFail");
        }
        return;
    }

    public void setLetter(char letter)
    {
        beatLetter = letter;
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
    }

    void setPlayableState()
    {
        playableState = true;
        ui.SendMessage("onPlayState");
    }

    void unsetPlayableState()
    {
        playableState = false;
    }
}
