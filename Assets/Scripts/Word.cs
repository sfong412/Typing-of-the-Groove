using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word : MonoBehaviour
{
    public string word;
    private int typeIndex;

    public char letter;

    GameplayUI display;

    PlayerInput letterInput;

    public Word(string _word, char _letter, GameplayUI _display, PlayerInput _letterInput)
    //public Word(string _word)
    {
        word = _word;
        typeIndex = 0;

        display = _display;
        letter = _letter;

        letterInput = _letterInput;

        display.setWord(word);
        display.setLetter(letter);

        letterInput.setLetter(letter);
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

