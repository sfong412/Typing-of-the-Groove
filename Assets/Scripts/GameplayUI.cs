using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{

    //The current position of the song within the loop in beats.
    //public float loopPositionInBeats;

    //Conductor instance
    public static Conductor conductor;

    public static GameplayManager gameplayManager;

    public PlayerInput p1;
    public PlayerInput p2;

    //public hitStatus status;

    public Text p1ScoreUI;
    public Text p2ScoreUI;

    public Image p1GrooveUI;
    public Image p1GrooveHitUI;

    public Text p1GrooveText;
    public Text p1GrooveHitText;

    private Color onBeat;
    private Color offBeat;
    private Color failColor;

    public Text hitStatusText;
    public Text comboText;

    public Text wordText;

    //GameObject canvas;

    //private float[] beats = {2.95, 3.05};

    public bool haveBG;

    public bool isScore;

    private float minThreshold;
    private float maxThreshold;

    private int[] beats;

    // Start is called before the first frame update
    void Start()
    {
        //Load the conductor
        conductor = GameObject.Find("Conductor").GetComponent<Conductor>();

        //Load the gameplay manager
        gameplayManager = GameObject.Find("Gameplay Manager").GetComponent<GameplayManager>();

        p1 = GameObject.Find("Player 1").GetComponent<PlayerInput>();
       // p2 = GameObject.Find("Player 2").GetComponent<PlayerInput>();

        //Call the image attached to the game object
        p1GrooveUI = GameObject.Find("P1 Groovetron").GetComponent<Image>();
        p1GrooveHitUI = GameObject.Find("P1 Groovetron On Hit").GetComponent<Image>();

        p1GrooveText = GameObject.Find("P1 Groovetron Text").GetComponent<Text>();
        p1GrooveHitText = GameObject.Find("P1 Groovetron On Hit Text").GetComponent<Text>();

        p1ScoreUI = GameObject.Find("P1 Scoreboard Score").GetComponent<Text>();
        //p2ScoreUI = GameObject.Find("P2 Scoreboard Text").GetComponent<Text>();

      //  canvas = GameObject.Find("Canvas");

        hitStatusText = GameObject.Find("Hit Status Text").GetComponent<Text>();
        comboText = GameObject.Find("Combo Text").GetComponent<Text>();

        onBeat = new Color(0.9f, 0.9f, 0.9f, 1f);

        offBeat = new Color(0.7f, 0.7f, 0.7f, 1f);

        failColor = new Color(0.5f, 0.5f, 0.5f, 1f);

        //wip - for dynamic time signature keyPress threshold thing
        beats = new int[(int)conductor.beatsPerLoop];

        for (int i = 0; i < beats.Length; i++) 
        {
            beats[i] = i;
        }
    }

    // Update is called once per frame
    void Update()
    {    
        changeButtonColor(p1GrooveUI, conductor, gameplayManager);
        //changeButtonColor(p2GrooveUI, conductor, gameplayManager);

        changeScore(p1ScoreUI, gameplayManager, 1);
        //changeScore(p2ScoreUI, gameplayManager, 2);
    }

    public void changeButtonColor(Image i, Conductor c, GameplayManager g) {

        /*change the code to be dynamically based on number of beats instead of this hardcoded stuff*/
        if (g.myHitStatus != hitStatus.hitFail)
        {
            if (c.loopPositionInBeats > 0 - 0.1 && c.loopPositionInBeats < 0 + 0.1
             || c.loopPositionInBeats > 1 - 0.1 && c.loopPositionInBeats < 1 + 0.1
             || c.loopPositionInBeats > 2 - 0.1 && c.loopPositionInBeats < 2 + 0.1
             || c.loopPositionInBeats > 3 - 0.1 && c.loopPositionInBeats < 3 + 0.1)
            {
                i.color = onBeat;
            }
            else
            {
                i.color = offBeat;
            }
        }
        else if (g.myHitStatus == hitStatus.hitFail)
        {
            i.color = failColor;
        }
    }

    public void changeScore(Text t, GameplayManager g, int i) {
        if (i == 1)
        {
            t.text = p1.score.ToString();
        }
         if (i == 2)
        {
            t.text = p2.score.ToString();
        }
    }

    void onHitSuccess()
    {
        if (p1.comboCounter == 1)
        {
            comboText.text = "";
        } 
        else if (p1.comboCounter > 1 && p1.comboCounter < 6)
        {
            comboText.text = (p1.comboCounter - 1) + " Hits!";
        }
        else if (p1.comboCounter == 6)
        {
            comboText.text = "Cool!";
        }
        else if (p1.comboCounter == 7)
        {
            comboText.text = "Chillin!";
        }
        else if (p1.comboCounter == 8)
        {
            comboText.text = "Freeze!";
        }

        hitStatusText.text = "Great!";
        StartCoroutine(FadeTextToZeroAlpha(1f, hitStatusText));
        return;
    }

    void onHitFail()
    {
        comboText.text = "";
        hitStatusText.text = "Miss!";
        StartCoroutine(FadeTextToZeroAlpha(1f, hitStatusText));
        return;
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        yield return new WaitForSeconds(1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    public void setWord(string word)
    {
        //Text p1GrooveUI = GameObject.Find("P1 Groovetron Score").GetComponent<Text>();
       // wordText.text = word;
        p1GrooveText.text = word;
        p1GrooveText.color = Color.black;
    }

    public void removeLetter()
    {
        p1GrooveText.text = p1GrooveText.text.Remove(0,1);
        p1GrooveText.color = Color.red;
    }

    public void removeWord()
    {
       // Destroy(gameObject);
    }

    public void setLetter(char letter)
    {
        p1GrooveHitText.text = letter.ToString();
        p1GrooveText.color = Color.black;
    }
}
