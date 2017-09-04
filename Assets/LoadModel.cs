using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public class LoadModel : MonoBehaviour
{
   
    void Start()
    {

        


      // Assets.ThirdParty.CGALfirst();
      // Assets.ThirdParty.testVHACD();
       // Assets.ThirdParty.CGALsecond();
        Debug.Log("LoadModel");

        string path = Directory.GetCurrentDirectory() + "\\data";
        for (int i = 1; i < 4; i++)
        {
            string currentName = "*second" + i.ToString() + ").obj";
            Debug.Log(currentName);
            string[] dirs = Directory.GetFiles(path, "*("+currentName);
            GameObject MainCactusPart = new GameObject("MainCactusPart");
            //MainCactusPart.transform.position = new Vector3(i, i, i);
            foreach (string dir in dirs)
            {
                // Debug.Log(dir);


                GameObject CactusPart = new GameObject("CactusPart");
                CactusPart.AddComponent<Rigidbody>();
                CactusPart.GetComponent<Rigidbody>().isKinematic = false;
                CactusPart.GetComponent<Rigidbody>().useGravity = false;
                CactusPart.transform.position = new Vector3(0, 0, 0);
                CactusPart.transform.parent = MainCactusPart.transform;


                Mesh holderMesh = new Mesh();
                ObjImporter newMesh = new ObjImporter();
                holderMesh = newMesh.ImportFile(dir);

                MeshRenderer renderer = CactusPart.AddComponent<MeshRenderer>();
                MeshFilter filter = CactusPart.AddComponent<MeshFilter>();
                filter.mesh = holderMesh;

                CactusPart.AddComponent<MeshCollider>().sharedMesh = null;
                CactusPart.GetComponent<MeshCollider>().sharedMesh = holderMesh;
                CactusPart.GetComponent<MeshCollider>().convex = true;

               // CactusPart.AddComponent<CactusPart>();




            }
        }

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.name == "CactusPart")
                go.AddComponent<CactusPart>();
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
