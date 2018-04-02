using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    class ElephLeg : MonoBehaviour
    {

        ElephAgent agent;

        void Start()
        {
            agent = gameObject.transform.parent.gameObject.GetComponent<ElephAgent>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Ground")
            {
                //agent.fell = true;
            }
        }
    }

