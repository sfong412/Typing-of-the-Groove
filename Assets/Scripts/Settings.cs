using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public TextMeshProUGUI gameDifficultyText;
    public static int gameDifficulty = 1;

    public TextMeshProUGUI showBeatIndicatorText;
    public static int showBeatIndicator = 2;

    void Start()
    {       
        loadSettings();

        switch (gameDifficulty)
        {
            case 1:
                gameDifficultyText.text = "Normal";
                break;
            case 2:
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
                gameDifficultyText.text = "Hard";
                break;
            case 2:
                gameDifficulty = 1;
                gameDifficultyText.text = "Normal";
                break;
        }
        Debug.Log(gameDifficulty);
        saveSettings();
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
            gameDifficulty = 1;
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
    
}
