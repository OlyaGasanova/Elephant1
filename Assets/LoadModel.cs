using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public class LoadModel : MonoBehaviour
{
   
    void Start()
    {

        Debug.Log("LoadModel");

        string path = Directory.GetCurrentDirectory() + "\\Assets";

        string[] dirs = Directory.GetFiles(path, "*.obj");
        foreach (string dir in dirs)
        {
           // Debug.Log(dir);


            GameObject CactusPart = new GameObject("CactusPart");
            CactusPart.AddComponent<Rigidbody>();
            CactusPart.GetComponent<Rigidbody>().isKinematic = false;
            CactusPart.GetComponent<Rigidbody>().useGravity = false;
            CactusPart.transform.position = new Vector3(0, 0, 0);


            Mesh holderMesh = new Mesh();
            ObjImporter newMesh = new ObjImporter();
            holderMesh = newMesh.ImportFile(dir);

            MeshRenderer renderer = CactusPart.AddComponent<MeshRenderer>();
            MeshFilter filter = CactusPart.AddComponent<MeshFilter>();
            filter.mesh = holderMesh;

            CactusPart.AddComponent<MeshCollider>().sharedMesh = null;
            CactusPart.GetComponent<MeshCollider>().sharedMesh = holderMesh;
            CactusPart.GetComponent<MeshCollider>().convex = true;
        }



      


    }

    void OnCollisionEnter(Collision collision )
    {

        Debug.Log("Ono rabotaet!!!");



    }



    void Update()
    {
        
       
    }


}
