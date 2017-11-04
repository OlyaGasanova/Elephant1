using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class CustomActions : MonoBehaviour
    {
    void Start()
        {
        Button btn = GameObject.Find("Button").GetComponent(typeof(Button)) as Button;
        btn.onClick.AddListener(() => { OnPress(); });
        Button changeScene = GameObject.Find("ShowGraph").GetComponent(typeof(Button)) as Button;
            changeScene.onClick.AddListener(() => { ChangeScene(); });
        Camera first = GameObject.Find("Main Camera").GetComponent(typeof(Camera)) as Camera;
        first.enabled = true;
        Camera graphCamera = GameObject.Find("GraphCamera").GetComponent(typeof(Camera)) as Camera;
        graphCamera.enabled = false;

       


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

    }
}

