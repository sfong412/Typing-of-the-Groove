using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MouseOverButton : MonoBehaviour, IPointerEnterHandler
{
    public Menu menu;

    public TextMeshProUGUI songTitle;


    void Start()
    {
        songTitle = gameObject.GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        menu.highScoreDisplay.text = songTitle.text + "\n High score: " + showHighScore(songTitle.text).ToString();
        Debug.Log("score_" + songTitle.text + ".");
        Debug.Log("score_Bust a Groove OST - Kitty N");
    }

    public int showHighScore(string songName)
    {
        string key = "score_" + songName;

        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }
        else
        {
            return 420;
        }
    }
}