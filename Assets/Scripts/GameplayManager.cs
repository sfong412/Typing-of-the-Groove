using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Made it global for Gameplay UI script. Might change organization of this*/
public enum hitStatus
    {
        hitPass,
        hitFail,
        hitNotDone
    }
    
public class GameplayManager : MonoBehaviour
{
    public int difficulty;

    void start() 
    {
        WordGenerator.wordDifficulty = 1;
        difficulty = WordGenerator.wordDifficulty;
    }

    private int[] numberOfPlayers =
    {
        1,
        2
    };

    public hitStatus myHitStatus;

    // Start is called before the first frame update
    void Start()
    {
    }

    public hitStatus GetHitStatus()
    {
        return myHitStatus;
    }
    
    public static bool isGameplay()
    {
        Scene scene = SceneManager.GetActiveScene();

        bool gameplay;

        if (scene.name == "Song Gameplay")
        {
            gameplay = true;
        }
        else
        {
            gameplay = false;
        }

        return gameplay;
    }
}
