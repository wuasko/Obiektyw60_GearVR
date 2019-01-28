using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {

    public GameObject OtherLightSwitch;
    public bool IsOn = false;

    HighlightCollided highlightCollided;
    GameObject HandRight;
    GameObject HandLeft;
    LightSwitch lightSwitch;
    Collider collider;

    // Use this for initialization
    void Start()
    {
        HandRight = GameObject.Find("DistanceHandRight");
        if (!HandRight) Debug.Log("Can't find DistanceHandRight");
        HandLeft = GameObject.Find("DistanceHandLeft");
        if (!HandLeft) Debug.Log("Can't find DistanceHandLeft");
        highlightCollided = GetComponent<HighlightCollided>();
        if (!highlightCollided) Debug.Log("Script HighlightSelected not attached");

        lightSwitch = OtherLightSwitch.GetComponent<LightSwitch>();
        if (!lightSwitch) Debug.Log("Script LightSwitch not attached to other lightswitch");
        collider = GetComponent<BoxCollider>();
        if (!collider) Debug.Log("No Box Collider attached");
        //BoxCollider [] triggers = gameObject.GetComponentsInChildren<BoxCollider>();
        
        //Trigger1 = triggers[0];
        //Trigger2 = triggers[1];

        //TODO find all colliders from HandRight, save them in list. Check for these colliders in OnCollisionEnter
    }


    void OnTriggerEnter(Collider col)
    {
        //TODO try rotating parent object here, and when trigger enters again it will wait until the other part of switch rotates
            Debug.Log("Detecting collision with hand");

        if (!lightSwitch.IsOn) //The hand touches two switches at the same time
        {
            if (col.gameObject.name == "GrabVolumeBig")
            {
                IsOn = true;
            }
            if (col.gameObject.name == "GrabVolumeSmall")
            {
                IsOn = true;
            }
        }

    }

    void OnTriggerExit(Collider col)
    {

            if (col.gameObject.name == "GrabVolumeBig")
            {
                IsOn = false;
            }
            if (col.gameObject.name == "GrabVolumeSmall")
            {
                IsOn = false;
            }

    }


}
