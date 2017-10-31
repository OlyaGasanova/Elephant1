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
            Debug.Log("ghbd222");
        }

    public void OnPress()
        {
            InputField ifield = GameObject.Find("InputField").GetComponent(typeof(InputField)) as InputField;
            Debug.Log(ifield.text);
        }
}

