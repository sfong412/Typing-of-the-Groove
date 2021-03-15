using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public TextMeshProUGUI gameDifficultyText;
    public static int gameDifficulty = 1;

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

    public void saveSettings()
    {
        PlayerPrefs.SetInt("Game Difficulty", gameDifficulty);
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
    }
    
}
