using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word : MonoBehaviour
{
    public string word;
    private int typeIndex;

    GameplayUI display;

    public Word(string _word, GameplayUI _display)
    //public Word(string _word)
    {
        word = _word;
        typeIndex = 0;

        display = _display;
        display.setWord(word);
    }

    public char getNextLetter()
    {
        return word[typeIndex];
    }

    public void typeLetter()
    {
        typeIndex++;

        //remove the ltter on screen
        display.removeLetter();
    }

    public bool wordTyped()
    {
        bool wordTyped = (typeIndex >= word.Length);
        if (wordTyped)
        {
            //remove word on screen
        }
        return wordTyped;
    }
}

