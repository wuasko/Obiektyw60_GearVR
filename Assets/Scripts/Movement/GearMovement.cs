using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMovement : MonoBehaviour {


    public float speed = 1.0f;

    CharacterController controller;

    public GameObject centerEye;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        //Define the forward vector using your facing direction
        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 forward = centerEye.transform.forward;

        // If the touchpad is being held down, move forward in the direction you are facing.
        if (Input.GetMouseButton(0))
        {
            controller.SimpleMove(forward * speed);
        }
    }
}
