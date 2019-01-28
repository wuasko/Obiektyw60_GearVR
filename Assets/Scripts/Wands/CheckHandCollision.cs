using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
/// <summary>
/// Checking collision with hand colliders
/// Activates the object's HighlightCollided script
/// </summary>
public class CheckHandCollision : MonoBehaviour {


    DistanceGrabbable distanceGrabbable;
    HighlightCollided highlightCollided;
    GameObject HandRight;
    GameObject HandLeft;

    bool IsSet = false;

    // Use this for initialization
    void Start () {
        HandRight = GameObject.Find("DistanceHandRight");
        if (!HandRight) Debug.Log("Can't find DistanceHandRight");
        HandLeft = GameObject.Find("DistanceHandLeft");
        if (!HandLeft) Debug.Log("Can't find DistanceHandLeft");
        highlightCollided = GetComponent<HighlightCollided>();
        if (!highlightCollided) Debug.Log("Script HighlightCollided not attached");
        distanceGrabbable = GetComponent<DistanceGrabbable>();
        if (!distanceGrabbable) Debug.Log("Script DistanceGrabbable not attached");

        //TODO find all colliders from HandRight, save them in list. Check for these colliders in OnCollisionEnter
    }
	
	// Update is called once per frame
	void Update () {
        if (distanceGrabbable.isGrabbed && !IsSet)
        {
            highlightCollided.rayHit = true;
            IsSet = true;
        }
        if(!distanceGrabbable.isGrabbed && IsSet)
        {
            highlightCollided.rayHit = false;
            IsSet = false;
        }
	}
    void OnTriggerEnter(Collider col)
    {
        if (!IsSet)
        {
            Debug.Log("Detecting collision with hand");
            if (col.gameObject.name == "Cone")
            {
                highlightCollided.rayHit = true;
            }

            if (col.gameObject.name == "GrabVolumeBig")
            {
                highlightCollided.rayHit = true;
            }
            if (col.gameObject.name == "GrabVolumeSmall")
            {
                highlightCollided.rayHit = true;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (!IsSet)
        {
            if (col.gameObject.name == "Cone")
            {
                highlightCollided.rayHit = false;
            }

            if (col.gameObject.name == "GrabVolumeBig")
            {
                highlightCollided.rayHit = false;
            }
            if (col.gameObject.name == "GrabVolumeSmall")
            {
                highlightCollided.rayHit = false;
            }
        }
    }

}
