using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    //public bool enter = false;
    //public GameObject targetCollider;
    //public bool pickUp = false;
    //public GameObject subjectCollider;

    public int number;
    public GameObject[] highlightedGameObjects;
    public string targetTagName;
    public bool completed = false;

    private List<string> collisions = new List<string>();

    private void StartQuest()
    {

    }

    private void EndQuest()
    {
        //completed = true;
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
