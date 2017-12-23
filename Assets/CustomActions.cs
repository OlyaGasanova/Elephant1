using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class CustomActions : MonoBehaviour
    {

    private Button right;
    private Button left;

    void Start()
        {

        Button btn = GameObject.Find("Button").GetComponent(typeof(Button)) as Button;
        btn.onClick.AddListener(() => { OnPress(); });

        Button wieringReady = GameObject.Find("wieringReady").GetComponent(typeof(Button)) as Button;
            wieringReady.onClick.AddListener(() => { ChangeScene(); });

        Button changeScene = GameObject.Find("ShowGraph").GetComponent(typeof(Button)) as Button;
            changeScene.onClick.AddListener(() => { ChangeScene(); });

        right = GameObject.Find("Right").GetComponent(typeof(Button)) as Button;
        right.onClick.AddListener(() => { MoveCamera(2); });
        left = GameObject.Find("Left").GetComponent(typeof(Button)) as Button;
        left.onClick.AddListener(() => { MoveCamera(-2); });
        right.enabled = false;
        left.enabled = false;

        Camera first = GameObject.Find("Main Camera").GetComponent(typeof(Camera)) as Camera;
        first.enabled = true;
        Camera graphCamera = GameObject.Find("GraphCamera").GetComponent(typeof(Camera)) as Camera;
        graphCamera.enabled = false;

       


    }

    public void wieringReady()
    {
        CactusPart[] allObjects = UnityEngine.Object.FindObjectsOfType<CactusPart>();
        foreach (CactusPart go in allObjects)
        {
            go.flag = true;
        }
    }

    public void OnPress()
        {
            InputField ifield = GameObject.Find("InputField").GetComponent(typeof(InputField)) as InputField;
            Debug.Log(ifield.text);
            int parsedNumber;
            try
                {
                    parsedNumber = int.Parse(ifield.text);
                    MoveJudge.numberOfCycles = parsedNumber;
                    MoveJudge.canStart = true;
                }
            catch(Exception ex)
                {
                    Debug.Log("Input integer numeric value");
                }

        }

    public void ChangeScene()
    {
        Camera graphCamera = GameObject.Find("GraphCamera").GetComponent(typeof(Camera)) as Camera;
        Camera first = GameObject.Find("Main Camera").GetComponent(typeof(Camera)) as Camera;
        first.enabled = !first.enabled;
        graphCamera.enabled = !graphCamera.enabled;
        right.enabled = true;
        left.enabled = true;

    }

    public void MoveCamera(int distance)
    {
        Camera graphCamera = GameObject.Find("GraphCamera").GetComponent(typeof(Camera)) as Camera;
        Debug.Log(graphCamera.transform.position);
        graphCamera.transform.position = new Vector3(graphCamera.transform.position.x + distance, graphCamera.transform.position.y, graphCamera.transform.position.z);

    }
}

