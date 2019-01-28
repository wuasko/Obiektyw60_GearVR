using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectQuestDetect : MonoBehaviour {

    public bool reverseTarget; // if false, the object must collide with target to finish the quest, if true, objectt must stop colliding with target to finish the quest
    public GameObject collisionTarget;
    public GameObject alternateCollisionTarget;
    //public Material testMaterial;

    // Use this for initialization
    void Start()
    {
        //targetCollisionName = (((GameObject)this.gameObject).name); ;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if (!reverseTarget)
        {
            if ((collision.gameObject.name == collisionTarget.name) || (collision.gameObject.name == alternateCollisionTarget.name))
            {
                PassEndQuestToQuestManager(((GameObject)this.gameObject).tag);
            }
        }
        else
        {
            if (!((collision.gameObject.name == collisionTarget.name) || (collision.gameObject.name == alternateCollisionTarget.name)))
            {
                PassEndQuestToQuestManager(((GameObject)this.gameObject).tag);
            }
        }

    }

    private void PassEndQuestToQuestManager(string targetQuestName)
    {
        GameObject goManager = GameObject.Find("QuestManager");
        QuestManager questManager = (QuestManager)goManager.GetComponent(typeof(QuestManager));
        questManager.SetEndOfQuestByName(targetQuestName);
    }
}
