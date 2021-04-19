using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MouseOverButton : MonoBehaviour, IPointerEnterHandler
{
    public Menu menu;
    public Settings settings;
    public TextMeshProUGUI songTitle;

    GameObject songSelectScreen;

    void Start()
    {
        songSelectScreen = GameObject.Find("SongSelectPanel");

        settings = songSelectScreen.GetComponent<Settings>();

        songTitle = gameObject.GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SongMetadata.ReadSongJSON(songTitle.text);
        SongMetadata.UpdateSongInfo();

        menu.highScoreDisplay.text = SongMetadata.artist + " - " + SongMetadata.title  + "\n Difficulty: " + settings.gameDifficultyText.text + "\n High score: " + showHighScore(SongMetadata.fileName, Settings.gameDifficulty).ToString() + "\n \n BPM: " + SongMetadata.bpm + "\n Genre: " + SongMetadata.genre;
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