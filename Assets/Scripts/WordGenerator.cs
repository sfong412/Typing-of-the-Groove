using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    
    // !-- find a way to skip spaces in strings
    // !-- add difficulty scaling to wordList
    private static string[] wordList = 
    {
        "bob", "jones", "capussi", "please", "eggs", "stealing", "phobias", "space", "monster", "ending", "kirby", "robotron", "fever", "dancing", "totally", "limo", "flat", "earth", "conspiracy", "dog", "cat", "attach", "screw", "manager", "wall", "democrat", "republican", "dictionary", "dedede", "the", "essay", "bogus", "nauseous", "conscientious", "paraphernalia", "onomatopoeia", "croc", "doki", "sugoi", "family", "western", "romance", "genres", "bot", "cheese", "milk", "meat", "vegan", "operation", "tempo", "alphabet", "bees", "eating", "wagon", "mushroom", "hundred", "sodium", "calcium", "instrument", "fast", "irony", "guitar", "mafia", "yakuza", "spoilers", "spills", "random", "typing", "texting", "roster", "racing", "quiet", "controller", "confusion"
    };

    public static string getRandomWord() 
    {
        int randomIndex = Random.Range(0, wordList.Length);
        string randomWord = wordList[randomIndex];

        return randomWord;
    }
}
