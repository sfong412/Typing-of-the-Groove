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
    public Image p2GrooveUI;
    private Color onBeat;
    private Color offBeat;
    private Color failColor;

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

        gameplayManager = GameObject.Find("Gameplay Manager").GetComponent<GameplayManager>();

        p1 = GameObject.Find("Player 1").GetComponent<PlayerInput>();
        p2 = GameObject.Find("Player 2").GetComponent<PlayerInput>();

        //Call the image attached to the game object
        p1GrooveUI = GameObject.Find("P1 Groovetron").GetComponent<Image>();
        p2GrooveUI = GameObject.Find("P2 Groovetron").GetComponent<Image>();

        p1ScoreUI = GameObject.Find("P1 Scoreboard Text").GetComponent<Text>();
        p2ScoreUI = GameObject.Find("P2 Scoreboard Text").GetComponent<Text>();

        onBeat = new Color(0.9f, 0.9f, 0.9f, 1f);

        offBeat = new Color(0.7f, 0.7f, 0.7f, 1f);

        failColor = new Color(0.5f, 0.5f, 0.5f, 1f);

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
        changeButtonColor(p2GrooveUI, conductor, gameplayManager);

        changeScore(p1ScoreUI, gameplayManager, 1);
        changeScore(p2ScoreUI, gameplayManager, 2);
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
}
