using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;

namespace Assets
{
    class ThirdParty
    {



        public static void CGALfirst()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("C:/dev/CGAL-4.10/build/examples/Surface_mesh_segmentation/Release/segmentation_from_sdf_values_OpenMesh_example.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = "C:/dev/CGAL-4.10/examples/Surface_mesh_segmentation/data/cactus.off first";
            Process myProc = Process.Start(startInfo);
            UnityEngine.Debug.Log(Directory.GetCurrentDirectory());
            myProc.WaitForExit();
            myProc.Close();
        }


        public static void testVHACD()
        {
            string path = Directory.GetCurrentDirectory() + "\\data";

            string[] dirs = Directory.GetFiles(path, "*.obj");
            foreach (string dir in dirs)
            {

                ProcessStartInfo startInfo = new ProcessStartInfo("C:/Users/user/Documents/Unity/Elephant/tools/testVHACD.exe");
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.Arguments = "--input " + dir + " --output " + dir/*.Replace(".obj", "(1).obj")*/ + " --resolution 10000";
                Process myProc = Process.Start(startInfo);
                myProc.WaitForExit();
                myProc.Close();

            }


        }

        public static void CGALsecond()
        {
            
            

            string path = Directory.GetCurrentDirectory() + "\\data";
            int count = 1;
            string[] dirs = Directory.GetFiles(path, "*first*.obj");
            foreach (string dir in dirs)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("C:/dev/CGAL-4.10/build/examples/Surface_mesh_segmentation/Release/segmentation_from_sdf_values_OpenMesh_example.exe");
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                string number = "second" + count.ToString();
                startInfo.Arguments = dir+" " + number;
                Process myProc = Process.Start(startInfo);
                UnityEngine.Debug.Log("Hello "+Directory.GetCurrentDirectory());
                myProc.WaitForExit();
                myProc.Close();
                count++;
            }
        }

    }
}