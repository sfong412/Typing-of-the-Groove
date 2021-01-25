using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{

    public List<Word> words;

    public WordSpawner wordSpawner;

    //ui stuff
    public GameplayUI gameplayUI;

    private bool hasActiveWord;
    private Word activeWord;

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
            Word word = new Word(WordGenerator.getRandomWord(), gameplayUI);
            Debug.Log(word.word);
            words.Add(word);
        }
        else //OPTIMIZE THE CODE BELOW BECAUSE THERE'S REPEATING CODE THERE
        {
            hasActiveWord = false;
            words.Remove(words[0]);

            Word word = new Word(WordGenerator.getRandomWord(), gameplayUI);
            Debug.Log(word.word);
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
            }
        }

        if (hasActiveWord && activeWord.wordTyped())
        {
            hasActiveWord = false;
            words.Remove(activeWord);
        }
    }
}
