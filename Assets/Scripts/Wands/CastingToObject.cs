using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Determines the selectedObject using raycast from wand position, forward
/// Activates the HighlightSelected script
/// </summary>
public class CastingToObject : MonoBehaviour {

    public bool IsCasting = false;
    public static string selectedObject;
    public string internalObject;
    public RaycastHit theObject;

    HighlightSelected lastHighlighted = null; //last highlited object
    HighlightSelected Highlighted = null; //currently highlighted object
    Transform tempObject;


    // Use this for initialization
    void Start () {
        tempObject = transform; //temporary assignment
    }
	
	// Update is called once per frame
	void Update () {

        //activate the raycasting with r button, for development purposes only
        if (Input.GetKeyDown("r"))
        {
            IsCasting = !IsCasting;
        }

        if (IsCasting)
        {
            if (Physics.Raycast(transform.position, transform.forward, out theObject))
            {

                selectedObject = theObject.transform.gameObject.name;
                internalObject = theObject.transform.gameObject.name;

                if (tempObject.transform.gameObject.name != theObject.transform.gameObject.name)
                {

                    Highlighted = theObject.transform.gameObject.GetComponent<HighlightSelected>();
                    lastHighlighted = tempObject.gameObject.GetComponent<HighlightSelected>();

                    if (lastHighlighted)
                    {
                        lastHighlighted.rayHit = false;
                    }

                    if (Highlighted)
                    {
                        Highlighted.rayHit = true;
                    }

                    tempObject = theObject.transform;
                    tempObject.transform.gameObject.name = theObject.transform.gameObject.name;
                }


            }
        }
	}
}
