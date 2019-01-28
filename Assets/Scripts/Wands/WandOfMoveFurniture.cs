using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
/// <summary>
/// This script is a controller for other scripts that WandOfMoveFurniture uses
/// It checks if the user is currently grabbing an object using DistanceGrabbable script
/// 
/// It controls:
/// - scripts for animations: CurtainController, MoveDoor
/// - script for moving shelves
/// - script for casting spells
/// - script for drawing laser pointer
/// - script for detecting which object we hit
/// 
/// It also finds the point to move for MovableObject script
/// and sets move variable to turn on/off this script
/// </summary>
public class WandOfMoveFurniture : MonoBehaviour
{

    public bool move = false;
    public Vector3 PointToGo;
    public string CurtainObjectName = "zaslony";
    public string MovableLayerName = "Movable";

    DistanceGrabbable distanceGrabbable;
    CastingToObject castingToObject;
    DrawLaser drawLaser;
    MoveShelves moveShelves;
    MoveDoor moveDoor;
    CurtainController curtainController;
    CastSpells castSpells;

    Collider WandCollider; //Collider used for ignoring collison with player

    GameObject KitchenShelf;
    GameObject GameManager;
    GameObject Door;
    GameObject wandOfMoveFurnitures;

    bool UsingItem = false;
    bool SomethingIsUsed = false;

    // Variables for holding user input
    private float MovementInput;

    // Use this for initialization
    void Start()
    {
        wandOfMoveFurnitures = GameObject.Find("WandOfMoveFurniture");

        KitchenShelf = GameObject.Find("salonRegal");
        if (!KitchenShelf) Debug.Log("Can't find salonRegal");

        GameManager = GameObject.Find("GameManager");
        if (!GameManager) Debug.Log("Can't find GameManager");


        distanceGrabbable = GetComponent<DistanceGrabbable>();
        castingToObject = GetComponent<CastingToObject>();
        drawLaser = GetComponent<DrawLaser>();
        moveShelves = KitchenShelf.GetComponent<MoveShelves>();
        curtainController = GameManager.GetComponent<CurtainController>();

        if (!distanceGrabbable) Debug.Log("No DistanceGrabbable script attached");
        if (!castingToObject) Debug.Log("No CastingToObject script attached");
        if (!drawLaser) Debug.Log("No DrawLaser script attached");
        if (!moveShelves) Debug.Log("No MoveShelves script attached");
        if (!curtainController) Debug.Log("No CurtainController script attached to GameManager object");
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput = Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") + Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger");

        //Input test
        //Debug.Log("Lindex " +Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger")); //left index
        //Debug.Log("Rindex " +Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger")); //right index
        //Debug.Log("Primary hand "+Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger")); //left grab
        //Debug.Log("Secondary hand "+Input.GetAxis("Oculus_CrossPlatform_SecondaryHandTrigger")); //right grab
        //Debug.Log("Primary thumbstick " + Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstick"));
        //Debug.Log("Secondary thumbstick " + Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstick"));


        SomethingIsUsed = curtainController.Use || moveShelves.IsShelfSelected || move;

        if (IsSelectedShelf() && MovementInput > 0 && !SomethingIsUsed) moveShelves.IsShelfSelected = true; //moving shelves
        if (IsSelectedDoor() && MovementInput > 0 && !SomethingIsUsed) moveDoor.Move = true; //moving door
        if (IsSelectedCurtain() && MovementInput > 0 && !SomethingIsUsed) curtainController.Use = true; //using curtains

        if (/*/*IsSelectedMovableObject() &&*/ MovementInput > 0) { move = true; }
        else { move = false; }

        FindIntersectionPoint();

        if (distanceGrabbable.isGrabbed)
        {
            //use item
            if (!UsingItem)
            {
                castingToObject.IsCasting = true; //activating script for detecting which object we hit
                drawLaser.IsShowingLaser = true;
                UsingItem = true;

                CastSpells castSpells = (CastSpells)wandOfMoveFurnitures.GetComponent(typeof(CastSpells));  //activating the spells
                castSpells.StartUsingSpells();

            }
        }
        if (UsingItem && !distanceGrabbable.isGrabbed)
        {
            //stop using item
            UsingItem = false;
            castingToObject.IsCasting = false;
            drawLaser.IsShowingLaser = false;

            CastSpells castSpells = (CastSpells)wandOfMoveFurnitures.GetComponent(typeof(CastSpells));
            castSpells.StopUsingSpells();
        }
    }

    private bool IsSelectedShelf()
    {
        GameObject selectedObject = GameObject.Find(CastingToObject.selectedObject);
        if (!selectedObject) return false;
        if (selectedObject.transform.IsChildOf(KitchenShelf.transform))
        {
            return true;
        }
        else return false;
    }

    private bool IsSelectedDoor()
    {
        GameObject selectedObject = GameObject.Find(CastingToObject.selectedObject);
        if (!selectedObject) return false;
        if (selectedObject.GetComponent<MoveDoor>()) //if selected object has component MoveDoor
        {
            moveDoor = selectedObject.GetComponent<MoveDoor>();
            return true;
        }
        else return false;
    }

    //private bool IsSelectedMovableObject()
    //{
    //    GameObject selectedObject = GameObject.Find(CastingToObject.selectedObject);
    //    if (!selectedObject) return false;
    //    if (selectedObject.GetComponent<MovableObject>() || selectedObject.GetComponent<MovableInYAxis>()) //if selected object has component MoveDoor
    //    {
    //        return true;
    //    }
    //    else return false;
    //}

    private bool IsSelectedCurtain()
    {
        GameObject selectedObject = GameObject.Find(CastingToObject.selectedObject);
        //Debug.Log(selectedObject.name);
        if (!selectedObject) return false;
        if (selectedObject.name == CurtainObjectName) //This is an empty gameobject with boxcollider placed in front of curtains
        {
            return true;
        }
        else return false;
    }

    private void FindIntersectionPoint()
    {
        int layerMask = 1 << LayerMask.NameToLayer(MovableLayerName); 
        layerMask = ~layerMask; //cast rays against every layer, beside MovableLayerName

        RaycastHit objectHit;

        // Shoot raycast
        if(Physics.Raycast(transform.position, transform.forward, out objectHit, 50, layerMask))
        {
            PointToGo = objectHit.point;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerTag")
        {
            Debug.Log("collision omitted");
            Collider PlayerCollider = collision.gameObject.GetComponent<BoxCollider>();
            Physics.IgnoreCollision(PlayerCollider, WandCollider);
        }
    }

}
