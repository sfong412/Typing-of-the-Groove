using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{

    //public SongMetadata songData;

    public GameObject MenuPanel;
    public GameObject SongSelectPanel;
    public GameObject HowToPlayPanel;
    public GameObject SettingsPanel;

    public Conductor conductor;

    public TextMeshProUGUI highScoreDisplay;

    //location of song's jsonFile metadata
    public string jsonFile;

   // public SongInfo song;

    // Start is called before the first frame update
    void Start()
    {
        //Get song metadata
       // songData = GetComponent<SongMetadata>();

        //GameplayManager.isGameplay() = false;

        MenuPanel.SetActive(true);
        SongSelectPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MouseEnterEvent()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
    }

    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
        SongSelectPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        Debug.Log("Menu Panel shown");
    }

    public void ShowSongSelectPanel()
    {
        //Settings.displayGameDifficulty();
        MenuPanel.SetActive(false);
        SongSelectPanel.SetActive(true);
        HowToPlayPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        Debug.Log("Song Select Panel shown");
    }

    public void ShowHowToPlayPanel()
    {
        MenuPanel.SetActive(false);
        SongSelectPanel.SetActive(false);
        HowToPlayPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        Debug.Log("How to Play Panel shown");
    }

    public void ShowSettingsPanel()
    {
      //  Settings.displayGameDifficulty();
        MenuPanel.SetActive(false);
        SongSelectPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void LoadSong(string file)
    {
        MenuPanel.SetActive(false);
        SongSelectPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        Debug.Log("Now loading " + file);
        Conductor.setFileName(file);
        SceneManager.LoadScene("Song Gameplay", LoadSceneMode.Single);
    }

    public float showHighScore(string songName, int currentDifficulty)
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
            return 0f;
        }
    }

    string songFile(string songName)
    {
        string path = Application.streamingAssetsPath + "/" + songName + ".json";
        return path;
    }

    public void QuitGame()
    {
        Resources.UnloadUnusedAssets();
        Application.Quit();
    }
}
