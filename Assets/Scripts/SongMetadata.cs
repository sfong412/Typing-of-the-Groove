using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SongMetadata : MonoBehaviour
{
    Menu menu;
    
    //JSON object
   // public string jsonFile;

    //JSON string
   // public string jsonString;
   
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

    public static SongInfo song;

    // C# song path thingy
    public string songPath;

    // Start is called before the first frame update
    void Start()
    {
        //Reads song JSON file
       // ReadSongJSON();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //break this and the tempo will break
    public static void ReadSongJSON(string fileName)//string filepath
    {
        string jsonFile = Application.streamingAssetsPath + "/" + fileName+ ".json";
        //jsonFile = menu.jsonFile;
        string jsonString = File.ReadAllText(jsonFile);
        song = JsonUtility.FromJson<SongInfo>(jsonString);
        //Debug.Log(jsonFile);
    }

    public static void UpdateSongInfo()
    {
        fileName = song.fileName;
        title = song.title;
        artist = song.artist;
        album = song.album;
        genre = song.genre;
        year = song.year;
        bpm = song.bpm;
        beats = song.beats;

        songStart = song.e_songStart;
        playStart = song.e_playStart;
        playEnd = song.e_playEnd;
        songEnd = song.e_songEnd;
    }

    public float getPlayStart()
    {
        return playStart;
    }

    [System.Serializable]
    public class SongInfo {
        //filename of the audio file
        public string fileName;
        
        //String for song title
        public string title;

        //String for artist / band of the song
        public string artist;

        //String for album name
        public string album;

        //String for song genre
        public string genre;

        //String for release year of the song
        public int year;

        //Interger for beats per minute (BPM)
        public float bpm;

        //Number of beats
        public int beats;

        //4/4 beat position of 1st beat
        public float e_songStart;

        //4/4 beat position of beat that starts the game's playable state
        public float e_playStart;

        //4/4 beat position of beat that ends the song / playable state
        public float e_playEnd;

        //
        public float e_songEnd;
    }
}
