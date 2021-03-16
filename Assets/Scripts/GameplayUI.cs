using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    //change ALL text to textmeshpro
    public TextMeshProUGUI p1ScoreUI;
    public TextMeshProUGUI p2ScoreUI;

    public Image p1GrooveUI;
    public Image p1GrooveHitUI;

    public TextMeshProUGUI p1GrooveText;
    public TextMeshProUGUI p1GrooveHitText;

    private float redValue;
    private float grooveOpacity;

    private Color onBeat;
    private Color offBeat;
    private Color failColor;

    public TextMeshProUGUI hitStatusText;
    public TextMeshProUGUI comboText;

    public TextMeshProUGUI wordText;

    public TextMeshProUGUI readyText;

    public Image beatIndicator1;
    public Image beatIndicator2;
    public Image beatIndicator3;
    public Image beatIndicator4;

    public AudioSource sfxPlayer;
    public AudioClip comboCompleteSfx;

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

        //Call the image attached to the game object
        p1GrooveUI = GameObject.Find("P1 Groovetron").GetComponent<Image>();
        p1GrooveHitUI = GameObject.Find("P1 Groovetron On Hit").GetComponent<Image>();

        p1GrooveText = GameObject.Find("P1 Groovetron Text").GetComponent<TextMeshProUGUI>();
        p1GrooveHitText = GameObject.Find("P1 Groovetron On Hit Text").GetComponent<TextMeshProUGUI>();

        p1ScoreUI = GameObject.Find("P1 Scoreboard Score").GetComponent<TextMeshProUGUI>();

        hitStatusText = GameObject.Find("Hit Status Text").GetComponent<TextMeshProUGUI>();
        comboText = GameObject.Find("Combo Text").GetComponent<TextMeshProUGUI>();

        readyText = GameObject.Find("Ready Text").GetComponent<TextMeshProUGUI>();

        onBeat = new Color(0.9f, 0.9f, 0.9f, 1f);

        offBeat = new Color(0.7f, 0.7f, 0.7f, 1f);

        failColor = new Color(0.5f, 0.5f, 0.5f, 1f);

        readyText.color = new Color(1f, 1f, 1f, 0f);

        redValue = 0;
        grooveOpacity = 1;

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
        changeButtonColor(p1GrooveUI, conductor, gameplayManager, false);
        changeButtonColor(p1GrooveHitUI, conductor, gameplayManager, true);

        changeScore(p1ScoreUI, gameplayManager, 1);

        if (Settings.showBeatIndicator == 2)
        {
            lightBeatIndicator(beatIndicator1, conductor, 1);
            lightBeatIndicator(beatIndicator2, conductor, 2);
            lightBeatIndicator(beatIndicator3, conductor, 3);
            lightBeatIndicator(beatIndicator4, conductor, 4);
        }
        else
        {
            beatIndicator1.color = new Color(1f, 0.5059f, 0.5490f, 0f);
            beatIndicator2.color = new Color(1f, 0.5059f, 0.5490f, 0f);
            beatIndicator3.color = new Color(1f, 0.5059f, 0.5490f, 0f);
            beatIndicator4.color = new Color(0.9059f, 0.3216f, 0.33f, 0f);
        }

        if (p1.playableState == true)
        {
            grooveOpacity = 1;
            p1GrooveText.color = new Color(redValue, 0f, 0f, grooveOpacity);
            p1GrooveHitText.color = new Color(0f, 0f, 0f, grooveOpacity);
        }
        else
        {
            grooveOpacity = 0;
            p1GrooveText.color = new Color(redValue, 0f, 0f, grooveOpacity);
            p1GrooveHitText.color = new Color(0f, 0f, 0f, grooveOpacity);
        }

      //  Debug.Log(p1.playableState);
    }

    public void changeButtonColor(Image i, Conductor c, GameplayManager g, bool isOnHit) {

        /*change the code to be dynamically based on number of beats instead of this hardcoded stuff*/
        /*better organize this code to reduce repetition */
        if (p1.playableState == true)
        {
            //lights up the left part of the groovetron
            if (g.myHitStatus != hitStatus.hitFail && isOnHit == false && p1.failCounter < 5)
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
            //lights up the right part of the groovetron
            else if (g.myHitStatus != hitStatus.hitFail && isOnHit == true && p1.failCounter < 5)
            {
                if (c.loopPositionInBeats > 3 - 0.1 && c.loopPositionInBeats < 3 + 0.1)
                {
                    i.color = onBeat;
                }
                else
                {
                    i.color = offBeat;
                }
            }
            //darkens the groovetron if fail counter reaches its limit
            else if (g.myHitStatus == hitStatus.hitFail || p1.failCounter == 5)
            {
                i.color = failColor;
            }
        }
    }

    //fix this mess of a code
    public void changeScore(TextMeshProUGUI t, GameplayManager g, int i) {
        if (i == 1)
        {
            t.text = p1.score.ToString();
        }
         if (i == 2)
        {
            t.text = p2.score.ToString();
        }
    }

    public void onHitSuccess()
    {
        if (p1.comboCounter == 1)
        {
            comboText.text = "";
            hitStatusText.text = "Great!";
        } 
        else if (p1.comboCounter > 1 && p1.comboCounter <= 8)
        {
            comboText.text = (p1.comboCounter - 1) + " Hits!";
            hitStatusText.text = "Great!";
        }

        if (p1.comboCounter == 9)
        {
            onCompleteCombo();
        } 

        StartCoroutine(FadeTextToZeroAlpha(1f, hitStatusText));
        return;
    }

    public void onHitFail()
    {
        comboText.text = "";
        hitStatusText.text = "Miss!";
        StartCoroutine(FadeTextToZeroAlpha(1f, hitStatusText));
        return;
    }

    public void onCompleteCombo()
    {
        WordGenerator.changeListDifficulty();
        comboText.text = "";
        hitStatusText.text = "Level Up!";
        sfxPlayer.Play();
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        yield return new WaitForSeconds(1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    // ~~change ALL text to textmeshpro plz~~
    public IEnumerator FadeTextToZeroAlphaPro(float t, TextMeshProUGUI i)
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
        p1GrooveText.text = word;
        redValue = 0;
    }

    public void removeLetter()
    {
        p1GrooveText.text = p1GrooveText.text.Remove(0,1);
        redValue = 1;
    }

    public void removeWord()
    {
    }

    public void setLetter(char letter)
    {
        p1GrooveHitText.text = letter.ToString();
    }

    //for lighting the beat indicator under the groovetron
    public void lightBeatIndicator(Image indicator, Conductor c, int b)
    {
        int beat = b - 1;
        if (p1.playableState == true)
        {
            if (c.loopPositionInBeats > beat - 0.1 && c.loopPositionInBeats < beat + 0.1)
            {
                if (b < 4)
                {
                    indicator.color = new Color(1f, 0.5059f, 0.5490f, 1f);
                }
                else
                {
                    indicator.color = new Color(0.3725f, 1f, 0.4157f, 1f);
                }
            }
            else
            {
                if (b < 4)
                {
                    indicator.color = new Color(0.9059f, 0.3216f, 0.33f, 1f);
                }
                else
                {
                    indicator.color = new Color(0f, 0.851f, 0.1569f, 1f);
                }  
            }
        }
        else
        {
            if (b < 4)
            {
                indicator.color = new Color(0.9059f, 0.3216f, 0.33f, 1f);
            }
            else
            {
                indicator.color = new Color(0f, 0.851f, 0.1569f, 1f);
            }  
        }
    }

    void eventText(Conductor c, PlayerInput p, TextMeshProUGUI t) {
        if (p.playableState == true)
        {
            StartCoroutine(FadeTextToZeroAlphaPro(1f, t));
        }
        return;
    }

    public void showReadyText(int opacity)
    {
        readyText.color = new Color(readyText.color.r, readyText.color.g, readyText.color.b, opacity);
    }
}
