using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public class LoadModel : MonoBehaviour
{

    void Start()
    {
        var defaultMat = Resources.Load<Material>("OurMat");
        ObjImporter newMesh = new ObjImporter();
      
        // Assets.ThirdParty.CGALfirst();
        // Assets.ThirdParty.testVHACD();
        // Assets.ThirdParty.CGALsecond();
        Debug.Log("LoadModel");

        string path = Directory.GetCurrentDirectory() + "\\data";
        for (int i = 1; i < 4; i++)
        {
            List<Collider> sbparts = new List<Collider>();
            string currentName = "*second" + i.ToString() + ").obj";
            Debug.Log(currentName);
            string[] dirs = Directory.GetFiles(path, "*(" + currentName);
            GameObject MainCactusPart = new GameObject("MainCactusPart");
            //MainCactusPart.transform.position = new Vector3(i, i, i);
            foreach (string dir in dirs)
            {
                // Debug.Log(dir);
                var CactusPart = MainCactusPart;
               // GameObject CactusPart = new GameObject("CactusPart");
                CactusPart.AddComponent<Rigidbody>();
                CactusPart.GetComponent<Rigidbody>().isKinematic = false;
                //CactusPart.GetComponent<Rigidbody>().useGravity = false;
                CactusPart.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                CactusPart.transform.position = new Vector3(0, 0, 0);
                CactusPart.transform.parent = MainCactusPart.transform;


                Mesh holderMesh = new Mesh();
                holderMesh = newMesh.ImportFile(dir);

                //MeshRenderer renderer = CactusPart.AddComponent<MeshRenderer>();
                //MeshFilter filter = CactusPart.AddComponent<MeshFilter>();
                // filter.mesh = holderMesh;

                var collider = CactusPart.AddComponent<MeshCollider>();
                collider.sharedMesh = null;
                collider.sharedMesh = holderMesh;
                collider.convex = true;

                //CactusPart.GetComponent<MeshRenderer>().material = defaultMat;
                sbparts.Add(collider);
            }
            Mesh holderMesh1 = new Mesh();
            holderMesh1 = newMesh.ImportFile(Directory.GetCurrentDirectory() + "\\data\\" + i.ToString()+"_part_" + i.ToString()+"(first).obj");
            MainCactusPart.AddComponent<CactusPart>();
            MeshRenderer renderer = MainCactusPart.AddComponent<MeshRenderer>();
            MeshFilter filter = MainCactusPart.AddComponent<MeshFilter>();
             filter.mesh = holderMesh1;
            foreach (var collider1 in sbparts)
            {
                foreach (var collider2 in sbparts)
                {
                    Physics.IgnoreCollision(collider1, collider2, true);
                }
            }
        }

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.name == "CactusPart")
                go.AddComponent<CactusPart>();
        }

    }

    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Ono rabotaet!!!");



    }



    void Update()
    {


    }


}
