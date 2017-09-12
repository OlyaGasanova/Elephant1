using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settings // todo make it more meaningfull
{
   public int number = Random.Range(0, 7);
    public float strength = Random.Range(0.0f, 1000.0f);
}

public class TestMove : MonoBehaviour
{
    public MoveJudge controller;

    List<List<float>> Path = new List<List<float>>();
    List<float> partOfPath=new List<float>();
    SpringJoint[] Joints;
    public int step = 0;
    private GameObject go;
    private GameObject cactus;
    public GameObject start;
    private List<settings> settings;
    public GameObject target;
    // Use this for initialization
    void Start ()
    {
        settings = controller.GetSettings();
        go = Instantiate(Resources.Load("AnimalКобаска"), transform) as GameObject;
        go.name = "TestCactus";
        go.transform.localPosition = Vector3.zero;
        Joints = go.GetComponentsInChildren<SpringJoint>();
    }

    // Update is called once per frame
    void Update () {
        if (settings.Count > step)
        {
            partOfPath.Add(settings[step].number);
            partOfPath.Add(settings[step].strength);
            Joints[settings[step].number].spring = settings[step].strength;
        }
        step++;
        if (step == settings.Count)
	    {
	        var position = Vector3.zero;
            foreach(Transform t in go.transform)
            {
                position += t.position;
            }
	        position /= go.transform.childCount;
            Debug.Log("+++ "+Vector3.Distance(position, target.transform.position));
            controller.CheckMeOut(Vector3.Distance(position, target.transform.position), settings);
            Object.Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
