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
            ProcessStartInfo startInfo = new ProcessStartInfo(Directory.GetCurrentDirectory() + "/tools/segmentation_from_sdf_values_OpenMesh_example.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = Directory.GetCurrentDirectory() + "/tools/elephant.off first";
            Process myProc = Process.Start(startInfo);
            UnityEngine.Debug.Log(Directory.GetCurrentDirectory());
            myProc.WaitForExit();
            myProc.Close();
        }

        public static void meshConv()
        {
            string path = Directory.GetCurrentDirectory() + "\\data";

            string[] dirs = Directory.GetFiles(path, "*first*.obj");
            foreach (string dir in dirs)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(Directory.GetCurrentDirectory()+"/tools/meshconv");
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.Arguments = dir + " -c off -o " + dir.Replace(".obj", "");
                Process myProc = Process.Start(startInfo);
                UnityEngine.Debug.Log(Directory.GetCurrentDirectory());
                myProc.WaitForExit();
                myProc.Close();
            }

        }



        public static void CGALFilledIn()
        {
            string path = Directory.GetCurrentDirectory() + "\\data";

            string[] dirs = Directory.GetFiles(path, "*first*.off");
            foreach (string dir in dirs)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(Directory.GetCurrentDirectory()+ "/tools/hole_filling_example.exe");
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.Arguments = dir;
                Process myProc = Process.Start(startInfo);
                UnityEngine.Debug.Log(Directory.GetCurrentDirectory());
                myProc.WaitForExit();
                myProc.Close();
            }
        }


        public static void meshConvReverse()
        {
            string path = Directory.GetCurrentDirectory() + "\\data";

            string[] dirs = Directory.GetFiles(path, "*first*.off");
            foreach (string dir in dirs)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(Directory.GetCurrentDirectory() + "/tools/meshconv");
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.Arguments = dir + " -c obj -o " + dir.Replace(".off", "");
                Process myProc = Process.Start(startInfo);
                UnityEngine.Debug.Log(Directory.GetCurrentDirectory());
                myProc.WaitForExit();
                myProc.Close();
            }

        }

        public static void testVHACD()
        {
            string path = Directory.GetCurrentDirectory() + "\\data";

            string[] dirs = Directory.GetFiles(path, "*first*.off");
            foreach (string dir in dirs)
            {

                ProcessStartInfo startInfo = new ProcessStartInfo(Directory.GetCurrentDirectory() + "/tools/testVHACD.exe");
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.Arguments = "--input " + dir + " --output " + (dir.Replace("first","second")).Replace(".off","")+ " --resolution 10000  --depth 4"; //4
                 Process myProc = Process.Start(startInfo);
                myProc.WaitForExit();
                myProc.Close();

            }


        }



        public static void CGALsecond()
        {
            
                UnityEngine.Debug.Log("Second + ");


            string path = Directory.GetCurrentDirectory() + "\\data\\vhacd";
            int count = 1;
            string[] dirs = Directory.GetFiles(path, "*first*.obj");
            foreach (string dir in dirs)
            {
                UnityEngine.Debug.Log("Second + " + dir);
                ProcessStartInfo startInfo = new ProcessStartInfo(Directory.GetCurrentDirectory()+ "/tools/segmentation_from_sdf_values_OpenMesh_example.exe");
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