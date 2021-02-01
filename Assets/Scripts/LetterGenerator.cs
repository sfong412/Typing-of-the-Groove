using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterGenerator : MonoBehaviour
{
    public static char getRandomLetter()
    {
        string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        char randomLetter = letters[Random.Range(0, letters.Length)];

        return randomLetter;
    }
}
