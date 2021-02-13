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

    public float songStart;

    public float playStart;

    public float songEnd;

    public SongInfo song;

    // C# song path thingy
    public string songPath;

    // Start is called before the first frame update
    void Start()
    {
        //Reads song JSON file
        ReadSongJSON();

        //Checks initial BPM
       // Debug.Log("before: " + bpm);

        //Checks current BPM
        //Debug.Log("after: " + bpm);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //break this and the tempo will break
    public void ReadSongJSON()//string filepath
    {
        string jsonFile = Application.streamingAssetsPath + "/Kim Petras - Heart to Break.json";
        //jsonFile = menu.jsonFile;
        string jsonString = File.ReadAllText(jsonFile);
        song = JsonUtility.FromJson<SongInfo>(jsonString);
        //Debug.Log(jsonFile);
    }

    public void UpdateSongInfo()
    {
        fileName = song.fileName;
        title = song.title;
        artist = song.artist;
        album = song.album;
        genre = song.genre;
        year = song.year;
        bpm = song.bpm;
        beats = song.beats;
    }

    public float getPlayStart()
    {
        playStart = song.e_playStart;
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

        public float e_songStart;

        public float e_playStart;

        public float e_songEnd;
    }
}
