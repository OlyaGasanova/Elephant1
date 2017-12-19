using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class settings // todo make it more meaningfull
{
   public int number = Random.Range(0, 10);
    public float strength = Random.Range(0.0f, 1000.0f);
}

public class TestMove : MonoBehaviour
{
    public MoveJudge controller;

    List<List<float>> Path = new List<List<float>>();
    List<float> partOfPath=new List<float>();
    SpringJoint[] Joints;
    FixedJoint[] Joints2;
    public int step = 0;
    private GameObject go;
    private GameObject cactus;
    public GameObject start;
    public List<settings> settings;
    public GameObject target;


    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = -1;
    }

    public void TurnOff()
    {
        this.enabled = false;
    }

    // Use this for initialization
    void Start ()
    {
        if (controller.globalCount < MoveJudge.numberOfCycles)
        {
            if (controller.globalCount == 1) settings = controller.GetSettings();
            else
            {
                settings = controller.GetNextSettings();
                // Debug.Log(controller.globalCount+"   current "+controller.localCount);
            }

            go = Instantiate(Resources.Load("AnimalКобаска"), transform) as GameObject;
            go.name = "TestCactus";
            go.transform.localPosition = Vector3.zero;
            Joints = go.GetComponentsInChildren<SpringJoint>();
            var cj = Joints[0];
            var bla = cj.transform.parent.GetComponentsInChildren<Rigidbody>();
            foreach (var b in bla)
                b.isKinematic = false;


        }
    }


    

    // Update is called once per frame
    void Update()
    {


            if (settings.Count > step)
            {
                float currentStrength = 0;
                float currentNumber = 0;


                partOfPath.Add(settings[step].number);
                partOfPath.Add(settings[step].strength);
                try
                {
                    var cj = Joints[settings[step].number];
                    var bla = cj.transform.parent.GetComponentsInChildren<Rigidbody>();
                    foreach (var b in bla)
                        b.isKinematic = false;
                    cj.connectedBody.isKinematic = false;
                    Joints[settings[step].number].spring = settings[step].strength;
                    bla = cj.transform.parent.GetComponentsInChildren<Rigidbody>();
                    foreach (var b in bla)
                        b.isKinematic = true;
                    cj.connectedBody.isKinematic = true;

                    // cj = sj;



                }
                catch (System.IndexOutOfRangeException e)  // CS0168
                {
                    Debug.Log(e.Message + Joints.Length);
                    Debug.Log(Joints.Length + "    trying get" + settings[step].number);
                    Debug.Log(controller.globalCount + "   " + currentNumber);

                }
            }
            step++;
            if (step == settings.Count)
            {
                var position = Vector3.zero;
                foreach (Transform t in go.transform)
                {
                    position += t.position;
                }
                position /= go.transform.childCount;
                try
                {
                    controller.CheckMeOut(Vector3.Distance(position, target.transform.position), settings);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                    TurnOff();
                }
                Object.Destroy(gameObject.transform.parent.gameObject);
            }
    }
}
