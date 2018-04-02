using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoadModel : MonoBehaviour
{


    public void Start() {
        startsecond();
    }

    public void startsecond(){

        List<GameObject> Parts = new List<GameObject>();
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

        //Debug.Log("LoadModel");
        GameObject Eleph = new GameObject("Eleph");
        string path = Directory.GetCurrentDirectory() + "\\data";
        for (int i = 1; i < 19; i++)
        {
            List<Collider> sbparts = new List<Collider>();
            string currentName = "_"+i.ToString()+ "(second)";
            //Debug.Log("Ищем части "+currentName);
            string[] dirs = Directory.GetFiles(path, "*" + currentName+ "*");
            GameObject MainCactusPart = new GameObject("MainCactusPart"+i.ToString());
            Parts.Add(MainCactusPart);
            //MainCactusPart.transform.position = new Vector3(i, i, i);
            foreach (string dir in dirs)
            {
                // Debug.Log(dir);
                //Debug.Log("             " + dir);
                var CactusPart = MainCactusPart;
                // GameObject CactusPart = new GameObject("CactusPart");
                 CactusPart.AddComponent<Rigidbody>();
                //CactusPart.GetComponent<Rigidbody>().isKinematic = true;
                CactusPart.GetComponent<Rigidbody>().useGravity = false;
                if (i == 2 || i == 5 || i == 10 || i == 6)
                    CactusPart.GetComponent<Rigidbody>().mass = 20;
                if (i == 4)
                    CactusPart.GetComponent<Rigidbody>().mass = 25;
                CactusPart.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
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
            //Debug.Log("         холедр меш    " + "\\data\\" + i.ToString() + "_part_" + i.ToString() + "(first).obj");
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
            //Debug.Break();
           MainCactusPart.transform.parent = Eleph.transform;
        
        }
        //testCopm tc = new testCopm();
        foreach (var part in Parts)
        {
            //Debug.Log(part.transform.name);
            if (part.transform.name != "MainCactusPart4")
                part.transform.parent = GameObject.Find("MainCactusPart4").GetComponent(typeof(Rigidbody)).transform;
            //else part.transform.name = "Body";

        }
        //yield return new WaitForSeconds(5);
        StartCoroutine(MyCoroutine());
        //wieringReadyClick();
        //StartCoroutine(MyCoroutine());

        //yield return new WaitForSeconds(5);
        //Teach();


        // Eleph.AddComponent<testCopm>();

    }

    public IEnumerator MyCoroutine()
    {
        //This is a coroutine
        yield return new WaitForSeconds((float)0.5);    //Wait one frame
        wieringReadyClick();
        yield return new WaitForSeconds((float)0.5);
        Teach();

    }

    public static void Teach()
    {
        GameObject Body = GameObject.Find("MainCactusPart4");
        var count = Body.transform.childCount;
        Transform[] transformChilds = new Transform[4];
        int k = 0;
        for (int i = 0; i < count; i++)
        {
            var part = Body.transform.GetChild(i);
            if (part.name.Contains("Part2") || part.name.Contains("Part5") || part.name.Contains("Part6") || part.name.Contains("Part10"))
            {
                ElephLeg leg = part.gameObject.AddComponent<ElephLeg>();
                transformChilds[k] = Body.transform.GetChild(i);
                k++;
            }
            else { part.gameObject.AddComponent<ElephBody>(); }
            //transformChilds[i] = Body.transform.GetChild(i);

        }
        Body.AddComponent<ElephBody>();
        //ElephAgent ea = new ElephAgent(transformChilds);
        //2 5 6 10
        GameObject Eleph = GameObject.Find("Eleph");
        ElephAgent elephAgent = Eleph.AddComponent<ElephAgent>();
        elephAgent.observations = new List<Camera>();
        elephAgent.limbs = (Transform[])transformChilds.Clone();
        elephAgent.maxStep = 1000;
        elephAgent.strength = 1000;
        elephAgent.resetOnDone = true;
        elephAgent.GiveBrain(GameObject.Find("Brain").GetComponent<Brain>());
        print(elephAgent.enabled + " - enabled");
        GameObject Academy = GameObject.Find("Academy");
       Academy.AddComponent<ElephAcademy>().defaultResetParameters = new Academy.ResetParameter[] { new Academy.ResetParameter("steps", 1) };
       // elephAcademy.defaultResetParameters = new Academy.ResetParameter[] { new Academy.ResetParameter("steps", 1) };
    }


    public static void wieringReadyClick()
    {
        foreach (var cactusPart in FindObjectsOfType<CactusPart>())
        {
            cactusPart.flag = true;
            var rb = cactusPart.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.constraints = 0;
        }

        foreach (var rb in FindObjectsOfType<Rigidbody>())
        {
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        }
    }

    void OnCollisionEnter(Collision collision)
    {

        //Debug.Log("Ono rabotaet!!!");



    }



    void Update()
    {


    }


}
