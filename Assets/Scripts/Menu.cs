using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public SongMetadata songData;

    public GameObject MenuPanel;
    public GameObject SongSelectPanel;

    public GameObject Conductor;

   // public SongInfo song;

    // Start is called before the first frame update
    void Start()
    {
        //Get song metadata
        songData = GetComponent<SongMetadata>();

        MenuPanel.SetActive(true);
        SongSelectPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
        SongSelectPanel.SetActive(false);
        Debug.Log("Menu Panel shown");
    }

    public void ShowSongSelectPanel()
    {
        MenuPanel.SetActive(false);
        SongSelectPanel.SetActive(true);
        Debug.Log("Song Select Panel shown");
    }

    public void LoadSong(string sceneName)
    {
        MenuPanel.SetActive(false);
        SongSelectPanel.SetActive(false);
        SceneManager.LoadScene(sceneName);
        Debug.Log("Now loading " + sceneName);
    }
}
