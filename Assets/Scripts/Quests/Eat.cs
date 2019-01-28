using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "OVRCameraRig")
        {
            PassEndQuestToQuestManager(((GameObject)this.gameObject).tag);
            Destroy(gameObject);
        }
    }

    private void PassEndQuestToQuestManager(string targetQuestName)
    {
        GameObject goManager = GameObject.Find("QuestManager");
        QuestManager questManager = (QuestManager)goManager.GetComponent(typeof(QuestManager));
        questManager.SetEndOfQuestByName(targetQuestName);
    }
}
