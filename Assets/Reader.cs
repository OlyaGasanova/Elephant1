using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

    class Reader: MonoBehaviour
    {

        public List<settings> settings;
        private GameObject go;
        public MoveJudge controller;
        SpringJoint[] Joints;


    void Start()
        {
            string line;
            System.IO.StreamReader file =
            new System.IO.StreamReader(@"C: \Users\user\Documents\Software\Elephant\note.txt");
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
            }


            //settings = controller.GetNextSettings();
            settings = controller.GetSettings();
            go = Instantiate(Resources.Load("AnimalКобаска"), transform) as GameObject;
            go.name = "TestCactus";
            go.transform.localPosition = Vector3.zero;
            Joints = go.GetComponentsInChildren<SpringJoint>();
        }

    }
