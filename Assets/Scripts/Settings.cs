using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{    
    public Menu menu;
    public TextMeshProUGUI songTitle;
    public TextMeshProUGUI gameDifficultyText;
    public static int gameDifficulty = 1;

    public TextMeshProUGUI showBeatIndicatorText;
    public static int showBeatIndicator = 2;

    void Start()
    {       
        initializeSettings();
    }

    void OnEnable()
    {
        initializeSettings();
    }

    public void initializeSettings()
    {
        loadSettings();

        switch (gameDifficulty)
        {
            case 1:
                gameDifficultyText.text = "Easy";
                break;
            case 2:
                gameDifficultyText.text = "Normal";
                break;
            case 3:
                gameDifficultyText.text = "Hard";
                break;
        }

        switch (showBeatIndicator)
        {
            case 1:
                showBeatIndicatorText.text = "Off";
                break;
            case 2:
                showBeatIndicatorText.text = "On";
                break;
        }
    }

    public void setGameDifficulty()
    {
        switch (gameDifficulty)
        {
            case 1:
                gameDifficulty = 2;
                gameDifficultyText.text = "Normal";
                break;
            case 2:
                gameDifficulty = 3;
                gameDifficultyText.text = "Hard";
                break;
            case 3:
                gameDifficulty = 1;
                gameDifficultyText.text = "Easy";
                break;
        }
        saveSettings();

        if (SongMetadata.fileName != "")
        {
            menu.highScoreDisplay.text = SongMetadata.artist + " - " + SongMetadata.title  + "\n Difficulty: " + gameDifficultyText.text + "\n High score: " + showHighScore(SongMetadata.fileName, Settings.gameDifficulty).ToString() + "\n \n BPM: " + SongMetadata.bpm + "\n Genre: " + SongMetadata.genre;
        }
    }

    public void setBeatIndicator()
    {
        //int switch = 1;

        switch (showBeatIndicator)
        {
            case 1:
                showBeatIndicator = 2;
                showBeatIndicatorText.text = "On";
                break;
            case 2:
                showBeatIndicator = 1;
                showBeatIndicatorText.text = "Off";
                break;
        }
        Debug.Log("Beat Indicator: " + showBeatIndicator);
        saveSettings();
    }

    public void saveSettings()
    {
        PlayerPrefs.SetInt("Game Difficulty", gameDifficulty);
        PlayerPrefs.SetInt("Show Beat Indicator", showBeatIndicator);
    }

    public void loadSettings()
    {
        if (PlayerPrefs.HasKey("Game Difficulty"))
        {
            gameDifficulty = PlayerPrefs.GetInt("Game Difficulty");
        }
        else
        {
            gameDifficulty = 2;
        }

        if (PlayerPrefs.HasKey("Show Beat Indicator"))
        {
            showBeatIndicator = PlayerPrefs.GetInt("Show Beat Indicator");
        }
        else
        {
            showBeatIndicator = 1;
        }
    }
    
    public int showHighScore(string songName, int currentDifficulty)
    {
        string difficulty = "";

        if (currentDifficulty == 1)
        {
            difficulty = "easy_";
        }
        else if (currentDifficulty == 2)
        {
            difficulty = "normal_";
        }
        else if (currentDifficulty == 3)
        {
            difficulty = "hard_";
        }

        string key = "score_" + difficulty + songName;

        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }
        else
        {
            return 0;
        }
    }
}
