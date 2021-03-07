﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    //public SongMetadata songData;

    public GameObject MenuPanel;
    public GameObject SongSelectPanel;
    public GameObject HowToPlayPanel;

    public Conductor conductor;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
        SongSelectPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);
        Debug.Log("Menu Panel shown");
    }

    public void ShowSongSelectPanel()
    {
        MenuPanel.SetActive(false);
        SongSelectPanel.SetActive(true);
        HowToPlayPanel.SetActive(false);
        Debug.Log("Song Select Panel shown");
    }

    public void ShowHowToPlayPanel()
    {
        MenuPanel.SetActive(false);
        HowToPlayPanel.SetActive(true);
        Debug.Log("How to Play Panel shown");
    }

    public void LoadSong(string file)
    {
        MenuPanel.SetActive(false);
        SongSelectPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);
        SceneManager.LoadScene("Song Gameplay");
        Debug.Log("Now loading " + file);
        jsonFile = songFile(file);
        Conductor.setFileName(file);
    }

    string songFile(string songName)
    {
        string path = Application.streamingAssetsPath + "/" + songName + ".json";
        return path;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
