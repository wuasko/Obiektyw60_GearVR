using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignMesh : MonoBehaviour {

    MeshCollider meshCol;
    MeshCollider wandMeshCol;
    MeshFilter mesh;
    MeshFilter wandMesh;
	// Use this for initialization
	void Start () {
        meshCol = GetComponent<MeshCollider>();
        mesh = GetComponent<MeshFilter>();
        wandMeshCol = gameObject.GetComponentInChildren<MeshCollider>();
        wandMesh = GetComponentInChildren<MeshFilter>();
        mesh.mesh = wandMesh.mesh;

        meshCol.sharedMesh = null;
        meshCol.sharedMesh = wandMeshCol.sharedMesh;


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
