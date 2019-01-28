using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the object to the point, determined by WandOfMoveFurniture script
/// Requires also HighlightSelected script attached.
/// </summary>
public class MovableObject : MonoBehaviour {

    public GameObject selectedObject;

    HighlightSelected highlightSelected;
    bool lookingAtObject = false;
    bool coroutineStarted = false;
    bool startedMoving = false;
    Rigidbody rigidbody;
    public int cnt = 0;

    GameObject wand;
    WandOfMoveFurniture wandOfMove;


	// Use this for initialization
	void Start () {
        
        wand = GameObject.Find("WandOfMoveFurniture");
        if (!wand) Debug.Log("Can't find wand of move furniture");
        wandOfMove = wand.GetComponent<WandOfMoveFurniture>();
        if (!wandOfMove) Debug.Log("Can't find script WandOfMoveFurniture");
        highlightSelected = GetComponent<HighlightSelected>();
        if (!highlightSelected) Debug.Log("no HighlightSelected script attached");
        rigidbody = GetComponent<Rigidbody>();
        if (!rigidbody) Debug.Log("no Rigidbody attached");

        wandOfMove.PointToGo = transform.position;

	}
	
	// Update is called once per frame
	void FixedUpdate () {


        if (wandOfMove.move) //must hold right index finger to start moving coroutine
        {
            if (highlightSelected.rayHit && !coroutineStarted)
            {
                selectedObject = GameObject.Find(CastingToObject.selectedObject);
                lookingAtObject = true;
                StartCoroutine(MoveObject());
                coroutineStarted = true;

            }
            else if (!highlightSelected.rayHit && coroutineStarted)
            {
                lookingAtObject = false;
                StopCoroutine(MoveObject());
                coroutineStarted = false;
            }
        }

        if(coroutineStarted && !wandOfMove.move)
        {
            StopCoroutine(MoveObject());
        }
    }


    IEnumerator MoveObject()
    {

        while(wandOfMove.move)
        {

            Vector3 DirectionToGo = wandOfMove.PointToGo - transform.position;
            rigidbody.MovePosition(transform.position + DirectionToGo*Time.deltaTime);

            yield return new WaitForSeconds(0.05f); 
        }
    }
}
