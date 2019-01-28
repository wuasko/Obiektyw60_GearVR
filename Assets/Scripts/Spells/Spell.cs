using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : ScriptableObject {

    public string spellName = "";
    public GameObject spellPrefab;
    public GameObject spellCollisionParticle;
    
    [Range(0, 20)]
    public int projectableSpeed;

}
