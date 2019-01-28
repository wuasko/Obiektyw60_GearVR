using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class CastSpells : MonoBehaviour
{

    private enum TypeOfSpell
    {
        MovingSpell,
        ChangeColorSpell,
        ResizeSpell,
        CookingSpell
    }

    private bool startUsingSpells;

    Spell spell;
    private int spellNumber;
    public List<Spell> spellsList = new List<Spell>();
    private float oculusTriggerInput;
    DistanceGrabbable distanceGrabbable;

    void Start()
    {
        startUsingSpells = false;

        distanceGrabbable = GetComponent<DistanceGrabbable>();
        if (!distanceGrabbable) Debug.Log("Script DistanceGrabbable not attached");

        spellNumber = 0;
        spell = (Spell)Resources.Load("Spells/" + gameObject.name, typeof(Spell));
        List<Spell> spellDatabase = GameObject.Find("SpellManager").GetComponent<SpellManager>().spellList;

        foreach (Spell spell_ in spellDatabase)
        {
            spellsList.Add(spell_);
        }
    }

    void FixedUpdate()
    {
        oculusTriggerInput = Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger");

        if (Input.GetKeyDown(KeyCode.U) && startUsingSpells && this.gameObject.name == "WandOfMoveFurniture")
        {
            Spell moveSpell;
            foreach (Spell s in spellsList)
            {
                if (s.spellName == "MoveSpell")
                {
                    moveSpell = s;
                    CastMagic(s, TypeOfSpell.MovingSpell);
                    break;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            Spell colorSpell;
            foreach (Spell s in spellsList)
            {
                if (s.spellName == "ColorSpell")
                {
                    colorSpell = s;
                    CastMagic(s, TypeOfSpell.MovingSpell);
                    break;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            Spell cookingSpell;
            foreach (Spell s in spellsList)
            {
                if (s.spellName == "CookingSpell")
                {
                    cookingSpell = s;
                    CastMagic(s, TypeOfSpell.MovingSpell);
                    break;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Spell moveSpell;
            foreach (Spell s in spellsList)
            {
                if (s.spellName == "SizeSpell")
                {
                    moveSpell = s;
                    CastMagic(s, TypeOfSpell.MovingSpell);
                    break;
                }
            }
        }
    }

    void CastMagic(Spell spell, TypeOfSpell typeOfSpell)
    {
        if (!spell.spellPrefab)
        {
            Debug.LogWarning("No prefab assigned to the spell!");
            return;
        }
        else
        {
            GameObject spellObject = new GameObject();

            switch (typeOfSpell)
            {
                case TypeOfSpell.MovingSpell:
                    spellObject = Instantiate(spell.spellPrefab, GameObject.FindGameObjectWithTag("MoveMagicSpawn").GetComponent<Transform>().position, Camera.main.GetComponent<Transform>().rotation);
                    break;
                case TypeOfSpell.ChangeColorSpell:
                    spellObject = Instantiate(spell.spellPrefab, GameObject.FindGameObjectWithTag("ColorMagicSpawn").GetComponent<Transform>().position, Camera.main.GetComponent<Transform>().rotation);
                    break;
                case TypeOfSpell.CookingSpell:
                    spellObject = Instantiate(spell.spellPrefab, GameObject.FindGameObjectWithTag("CookingMagicSpawn").GetComponent<Transform>().position, Camera.main.GetComponent<Transform>().rotation);
                    break;
                case TypeOfSpell.ResizeSpell:
                    spellObject = Instantiate(spell.spellPrefab, GameObject.FindGameObjectWithTag("SizeMagicSpawn").GetComponent<Transform>().position, Camera.main.GetComponent<Transform>().rotation);
                    break;
            }
            spellObject.AddComponent<Rigidbody>();
            spellObject.GetComponent<Rigidbody>().useGravity = false;
            spellObject.GetComponent<Rigidbody>().velocity = spellObject.transform.forward * spell.projectableSpeed;
            spellObject.name = spell.spellName;
            spellObject.transform.parent = GameObject.Find("SpellManager").transform;

            Destroy(spellObject, 2);
        }
    }

    public void StartUsingSpells()
    {
        startUsingSpells = true;
    }

    public void StopUsingSpells()
    {
        startUsingSpells = false;
    }
}
