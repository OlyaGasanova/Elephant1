using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    class ElephAcademy : Academy
    {
        public override void InitializeAcademy()
        {
            Debug.Log("Jollll");
            Monitor.verticalOffset = 1f;
        }

        public override void AcademyReset()
        {


        }

        public override void AcademyStep()
        {


        }
    }
