using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Made it global for Gameplay UI script. Might change organization of this*/
public enum hitStatus
    {
        hitPass,
        hitFail,
        hitNotDone
    }
    
public class GameplayManager : MonoBehaviour
{
    //Conductor instance
    //public static Conductor conductor;

    private int[] numberOfPlayers =
    {
        1,
        2
    };

    public hitStatus myHitStatus;

    //public List<Word> words;

    // Start is called before the first frame update
    void Start()
    {
    //    conductor = GameObject.Find("Conductor").GetComponent<Conductor>();
    }

    public hitStatus GetHitStatus()
    {
        return myHitStatus;
    }
    
}
