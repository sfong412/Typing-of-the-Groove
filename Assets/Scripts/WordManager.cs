using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{

    public List<Word> words;

    public WordSpawner wordSpawner;

    //ui stuff
    public GameplayUI gameplayUI;

    //custom player input
    public PlayerInput playerInput;

    private bool hasActiveWord;
    private Word activeWord;

    public char activeLetter;

    // Start is called before the first frame update
    void Start()
    {
        addWord();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addWord()
    {
        if (words.Count == 0)
        {
            Word word = new Word(WordGenerator.getRandomWord(), LetterGenerator.getRandomLetter(), gameplayUI, playerInput);
         //   Debug.Log(word.word);
            words.Add(word);
        }
        else //OPTIMIZE THE CODE BELOW BECAUSE THERE'S REPEATING CODE THERE
        {
            hasActiveWord = false;
            words.Remove(words[0]);

            Word word = new Word(WordGenerator.getRandomWord(), LetterGenerator.getRandomLetter(),gameplayUI, playerInput);
         //   Debug.Log(word.word);
            words.Add(word); 
        }
    }

    public void typeLetter(char letter)
    {
        if (hasActiveWord)
        {
            if (activeWord.getNextLetter() == letter)
            {
                activeWord.typeLetter();
            }
            else if (activeWord.getNextLetter() != letter)
            {
                if (playerInput.failCounter < 5)
                {
                    playerInput.SendMessage("hitFail"); //change to reach max limit
                }
            }
        }
        else
        {
            foreach (Word word in words) 
            {
                if (word.getNextLetter() == letter)
                {
                    activeWord = word;
                    hasActiveWord = true;
                    word.typeLetter();
                    break;
                }
                else if (word.getNextLetter() != letter)
                {
                    if (playerInput.failCounter < 5)
                    {
                        playerInput.SendMessage("hitFail"); //see above
                    }
                }
            }
        }

        if (hasActiveWord && activeWord.wordTyped())
        {
            hasActiveWord = false;
            words.Remove(activeWord);
        }
    }
}
