using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusHand : MonoBehaviour {

    // Use this for initialization
   // public Component connectbody;

    void Start()
    {

        Mesh holderMesh = new Mesh();
        ObjImporter newMesh = new ObjImporter();
        holderMesh = newMesh.ImportFile("C:/dev/CGAL-4.10/build/examples/Surface_mesh_segmentation/data/2_part_2.obj");
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        filter.mesh = holderMesh;

        GetComponent<MeshCollider>().sharedMesh = null;
        GetComponent<MeshCollider>().sharedMesh = holderMesh;

        GameObject cactusHand1 = GameObject.Find("CactusHand1");
        cactusHand1.AddComponent<CharacterJoint>();
        CharacterJoint connectbody = cactusHand1.GetComponent<CharacterJoint>();
        Rigidbody rb = GameObject.Find("Cactus").GetComponent<Rigidbody>();

        connectbody.connectedBody = rb;

        // cactus1 = GameObject.Find("Cactus");
        // Debug.Log("Succes");
        // Debug.Log(cactus1.transform.position);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
