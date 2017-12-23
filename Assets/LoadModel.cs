using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoadModel : MonoBehaviour
{

   /* void Start()
    {
        

        var defaultMat = Resources.Load<Material>("OurMat");
        ObjImporter newMesh = new ObjImporter();
      
         Assets.ThirdParty.CGALfirst();
         Assets.ThirdParty.testVHACD();
         Assets.ThirdParty.CGALsecond();
        Debug.Log("LoadModel");
        List<Rigidbody> rigidbody = new List<Rigidbody>();
        GameObject El = new GameObject("El");

        string path = Directory.GetCurrentDirectory() + "\\data";
        for (int i = 1; i < 12; i++)
        {
            List<Collider> sbparts = new List<Collider>();
            

            string currentName = "*second" + i.ToString() + ").obj"; //i.ToString() + "(first).obj";//"*second" + i.ToString() + ").obj";
            Debug.Log(currentName);
            string[] dirs = Directory.GetFiles(path, "*(" + currentName);
            GameObject MainCactusPart = new GameObject("MainCactusPart");
            //MainCactusPart.transform.position = new Vector3(i, i, i);
            Debug.Log("вот чио");
            foreach (string dir in dirs)
            {
                 Debug.Log(dir);
                var CactusPart = MainCactusPart;
               //GameObject CactusPart = new GameObject("CactusPart");
               
                CactusPart.GetComponent<Rigidbody>().isKinematic = true;
                rigidbody.Add(CactusPart.GetComponent<Rigidbody>());
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
            if (MainCactusPart.GetComponent<CharacterJoint>()!=null)
            {
                var cj = MainCactusPart.GetComponent<CharacterJoint>();
                var cb = cj.connectedBody;
                var anc = cj.anchor;
                var conanc = cj.connectedAnchor;
                var sj = MainCactusPart.AddComponent<SpringJoint>();
                sj.connectedBody = cb;
                sj.connectedAnchor = conanc;
                sj.anchor = anc;
                Destroy(MainCactusPart.GetComponent<CharacterJoint>());
            }
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
            if (go.name == "MainCactusPart")
            {
                go.AddComponent<CactusPart>();
                foreach (GameObject go2 in allObjects)
                {
                    //var spr = go.AddComponent<SpringJoint>();
                    //spr.connectedBody = go2.GetComponent<Rigidbody>();
                    //spr.spring = 100;
                }
            }
        }

        foreach (var rb in rigidbody)
           rb.isKinematic = false;

     /*   foreach (GameObject go in allObjects)
        {
            var l = go.GetComponents<CharacterJoint>();
            if (go.name == "MainCactusPart" && l.Length!=0)
            {
                foreach (var cj in l)
                {
                    Debug.Log("Ghbdtnbrb2");
                    var cb = cj.connectedBody;
                    var anc = cj.anchor;
                    var conanc = cj.connectedAnchor;
                    var sj = go.AddComponent<SpringJoint>();
                    sj.connectedBody = cb;
                    sj.connectedAnchor = conanc;
                    sj.anchor = anc;
                }
                Destroy((go.GetComponents<CharacterJoint>())[0]);
                Destroy((go.GetComponents<CharacterJoint>())[1]);

            }

        }*/

    void Start() {
        var defaultMat = Resources.Load<Material>("OurMat");
        ObjImporter newMesh = new ObjImporter();
        if (Directory.GetFiles(Directory.GetCurrentDirectory() + "/data/", "*second*").Length <= 0)
        {
            Assets.ThirdParty.CGALfirst();
            Assets.ThirdParty.meshConv();
            Assets.ThirdParty.CGALFilledIn();
            Assets.ThirdParty.meshConvReverse();
            Assets.ThirdParty.testVHACD();
        }

        Debug.Log("LoadModel");

        string path = Directory.GetCurrentDirectory() + "\\data";
        for (int i = 1; i < 12; i++)
        {
            List<Collider> sbparts = new List<Collider>();
            string currentName = "_"+i.ToString()+ "(second)";
            Debug.Log("Ищем части "+currentName);
            string[] dirs = Directory.GetFiles(path, "*" + currentName+ "*");
            GameObject MainCactusPart = new GameObject("MainCactusPart"+i.ToString());
            //MainCactusPart.transform.position = new Vector3(i, i, i);
            foreach (string dir in dirs)
            {
                // Debug.Log(dir);
                Debug.Log("             " + dir);
                var CactusPart = MainCactusPart;
                // GameObject CactusPart = new GameObject("CactusPart");
                CactusPart.AddComponent<Rigidbody>();
                //CactusPart.GetComponent<Rigidbody>().isKinematic = true;
                CactusPart.GetComponent<Rigidbody>().useGravity = true;
                if (i == 2 || i == 5 || i == 10 || i == 6)
                    CactusPart.GetComponent<Rigidbody>().mass = 20;
                if (i == 4)
                    CactusPart.GetComponent<Rigidbody>().mass = 25;

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
            Debug.Log("         холедр меш    " + "\\data\\" + i.ToString() + "_part_" + i.ToString() + "(first).obj");
            holderMesh1 = newMesh.ImportFile(Directory.GetCurrentDirectory() + "\\data\\" + i.ToString() + "_part_" + i.ToString() + "(first).obj");
            MainCactusPart.AddComponent<CactusPart>();
            MeshRenderer renderer = MainCactusPart.AddComponent<MeshRenderer>();
            MeshFilter filter = MainCactusPart.AddComponent<MeshFilter>();
            renderer.material = new Material(Shader.Find("Standard"));
            filter.mesh = holderMesh1;
            filter.mesh.RecalculateNormals();
            foreach (var collider1 in sbparts)
            {
                foreach (var collider2 in sbparts)
                {
                    Physics.IgnoreCollision(collider1, collider2, true);
                }
            }
            Debug.Break();
        }

       /* GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.name == "MainCactusPart")
                go.AddComponent<CactusPart>();
        }*/

    }

    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Ono rabotaet!!!");



    }



    void Update()
    {


    }


}
