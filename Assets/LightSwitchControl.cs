using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchControl : MonoBehaviour {

    public GameObject lightSwitchObjectUp;
    public GameObject lightSwitchObjectDown;
    

    LightSwitch lightSwitchUp;
    LightSwitch lightSwitchDown;
    Transform MovingPart;
    bool Rotated = false;


    // Use this for initialization
    void Start () {
        lightSwitchUp = lightSwitchObjectUp.GetComponent<LightSwitch>();
        if (!lightSwitchUp) Debug.Log("no lightswitch1 component");
        lightSwitchDown = lightSwitchObjectDown.GetComponent<LightSwitch>();
        if (!lightSwitchDown) Debug.Log("no lightswitch2 component");

        MovingPart = transform.GetChild(0);
    }
	
	// Update is called once per frame
	void Update () {

        if (lightSwitchUp.IsOn && !Rotated)
        {
            MovingPart.Rotate(0, 0, 15f);
            Debug.Log("Light On");
            Rotated = true;
        }
        if (lightSwitchDown.IsOn && Rotated)
        {
            MovingPart.Rotate(0, 0, -15f);
            Debug.Log("Light Off");
            Rotated = false;
        }
	}
}
