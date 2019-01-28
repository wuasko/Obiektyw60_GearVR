using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class SpellCreator : EditorWindow {

    [MenuItem("Spell Maker/ Spell Wizard")]
    static void Init()
    {
        SpellCreator spellWindow = (SpellCreator)CreateInstance(typeof(SpellCreator));
        spellWindow.Show();
    }

    SpellManager spellManager = null;
    Spell tempSpell = null;

    private void OnGUI()
    {
        if(spellManager == null)
        {
            spellManager = GameObject.Find("SpellManager").GetComponent<SpellManager>();
        }
        if (tempSpell)
        {
            tempSpell.spellName = EditorGUILayout.TextField("Spell Name", tempSpell.spellName);
            tempSpell.spellPrefab = (GameObject)EditorGUILayout.ObjectField("Spell prefab", tempSpell.spellPrefab, typeof(GameObject), false);
            tempSpell.spellCollisionParticle = (GameObject)EditorGUILayout.ObjectField("Spell collisoion effect", tempSpell.spellCollisionParticle, typeof(GameObject), false);
            tempSpell.projectableSpeed = EditorGUILayout.IntField("Spell speed", tempSpell.projectableSpeed);
        }

        EditorGUILayout.Space();

        if (tempSpell == null)
        {
            if(GUILayout.Button("create Spell"))
            {
                tempSpell = (Spell)CreateInstance<Spell>();
            }
        }
        else
        {
            if (GUILayout.Button("Create Scriptable Object"))
            {
                AssetDatabase.CreateAsset(tempSpell, "Assets/Resources/Spells/" + tempSpell.spellName + ".asset");
                AssetDatabase.SaveAssets();
                spellManager.spellList.Add(tempSpell);
                Selection.activeObject = tempSpell;

                tempSpell = null;
            }


            if (GUILayout.Button("Reset"))
            {
                Reset();
            }
        }
    }

    public void Reset()
    {
        if (tempSpell)
        {
            tempSpell.spellName = "";
            tempSpell.spellPrefab = null;
            tempSpell.spellCollisionParticle = null; ;
            tempSpell.projectableSpeed = 0;
        }
    }
}
