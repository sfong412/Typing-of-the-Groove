using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MetadataManager : MonoBehaviour
{
    //filename of the audio file
    public static string fileName;
    
    //String for song title
    public static string title;

    //String for artist / band of the song
    public static string artist;

    //String for album name
    public static string album;

    //String for song genre
    public static string genre;

    //String for release year of the song
    public static int year;

    //Interger for beats per minute (BPM)
    public static float bpm;

    //Number of beats
    public static int beats;

    public static float songStart;

    public static float playStart;

    public static float playEnd;

    public static float songEnd;

    static string getFileName(string fileName)
    {
        string jsonFile = Application.streamingAssetsPath + "/" + fileName+ ".json";
        string jsonString = File.ReadAllText(jsonFile);
        //string _fileName = JsonUtility.FromJson<MetadataManager>(jsonString).fileName;
        return fileName;
    }

    static void ReadSongJSON(string fileName)//string filepath
    {
    }
}
