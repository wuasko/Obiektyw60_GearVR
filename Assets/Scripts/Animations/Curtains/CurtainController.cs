using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays the curtain animation forward or backward
/// 
/// WandOfMoveFurniture controls this script
/// </summary>
public class CurtainController : MonoBehaviour {

    #region Singleton

    public static CurtainController instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of CurtainController found!");
            return;
        }
        instance = this;
    }
    #endregion

    public bool Use;
    public GameObject curtainLeft;
    public GameObject curtainRight;
    public GameObject curtainMid;

    Animation curtainLeftAnim;
    Animation curtainRightAnim;
    Animation curtainMidAnim;

    string leftAnimName;
    string rightAnimName;
    string midAnimName;

    bool IsPlayingForward = false;
    bool AnimationStarted = false;


    // Use this for initialization
    void Start () {

        if (!curtainLeft) Debug.Log("no curtainLeft object assigned");
        if (!curtainRight) Debug.Log("no curtainRight object assigned");
        if (!curtainMid) Debug.Log("no curtainMid object assigned");

        curtainLeftAnim = curtainLeft.GetComponent<Animation>();
        curtainRightAnim = curtainRight.GetComponent<Animation>();
        curtainMidAnim = curtainMid.GetComponent<Animation>();
        
        foreach (AnimationState state in curtainLeftAnim) leftAnimName = state.name;
        foreach (AnimationState state in curtainRightAnim) rightAnimName = state.name;
        foreach (AnimationState state in curtainMidAnim) midAnimName = state.name;
    }
	
	// Update is called once per frame
	void Update () {

        if (IsPlayingForward && !AnimationStarted)
        {
            curtainLeftAnim[leftAnimName].time = 0;
            curtainLeftAnim[leftAnimName].speed = 1;
            curtainRightAnim[rightAnimName].time = 0;
            curtainRightAnim[rightAnimName].speed = 1;
            curtainMidAnim[midAnimName].time = 0;
            curtainMidAnim[midAnimName].speed = 1;
            AnimationStarted = true;
        }
        else if(!IsPlayingForward && AnimationStarted)
        {
            curtainLeftAnim[leftAnimName].time = curtainLeftAnim[leftAnimName].length;
            curtainLeftAnim[leftAnimName].speed = -1;
            curtainRightAnim[rightAnimName].time = curtainRightAnim[rightAnimName].length;
            curtainRightAnim[rightAnimName].speed = -1;
            curtainMidAnim[midAnimName].time = curtainMidAnim[midAnimName].length;
            curtainMidAnim[midAnimName].speed = -1;
            AnimationStarted = false;
        }


        if (!curtainLeftAnim.isPlaying)
        {
            if (Use)
            {

                //curtainLeft.GetComponent<HighlightSelected>().rayHit = true;
                IsPlayingForward = !IsPlayingForward;
                curtainLeftAnim.Play(leftAnimName);
                curtainRightAnim.Play(rightAnimName);
                curtainMidAnim.Play(midAnimName);
                
            }
        }
        if(Use) Use = false;
    }
}
