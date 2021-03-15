using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncedAnimation : MonoBehaviour
{
    public Conductor conductor;
    public GameplayManager gameplayManager;

    hitStatus myHitStatus;
    
    //The animator controller attached to this GameObject
    public Animator animator;

    //Records the animation state or animation that the Animator is currently in
    public AnimatorStateInfo animatorStateInfo;

    //Used to address the current state within the Animator using the Play() function
    public int currentState;

    public int idleState = Animator.StringToHash("Idle");

    public int danceState;

    //add smoother dance transitions
    private int[] danceList = 
    {
        Animator.StringToHash("Dance01"),
        Animator.StringToHash("Dance02"),
        Animator.StringToHash("Dance03"),
        Animator.StringToHash("Dance04"),
        Animator.StringToHash("Dance05"),
        Animator.StringToHash("Dance06")
    };

    // Start is called before the first frame update
    void Start()
    {
         //Load the animator attached to this object
        //animator = GetComponent<Animator>();

        //Get the info about the current animator state
        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        //Convert the current state name to an integer hash for identification
        currentState = animatorStateInfo.fullPathHash;
    }

    // Update is called once per frame
    void Update()
    {
        //Start playing the current animation from wherever the current conductor loop is
        animator.Play(currentState, -1, conductor.loopPositionInAnalog);
            
        //Set the speed to 0 so it will only change frames when you next update it
        animator.speed = 0;
    }

    public void onHitSuccess()
    {
        int randomIndex = Random.Range(0, danceList.Length);
        danceState = danceList[randomIndex];

        currentState = danceState;
    }

    public void onHitFail()
    {
       currentState = idleState;
    }
}
