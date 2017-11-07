using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MoveJudge : MonoBehaviour
{
    public class Creature
    {
        public List<settings> Settings = new List<settings>();
        public float score = 0;
    }
    public List<Creature> best = new List<Creature>();
    public List<Creature> BestofCreatures = new List<Creature>();
    private int count = 0;
    //private int creatu
    public int globalCount=1;
    public static int numberOfCycles =100;
    private int CreatureCount = 0;
    private int steps = 400;
    public int localCount = 0;
    public static bool canStart = false;
    public static bool firstStart = true;
    LineRenderer lineRenderer;

    private FileStream fstream;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = -1;
        lineRenderer = GameObject.Find("LineRenderer").GetComponent(typeof(LineRenderer)) as LineRenderer;//gameObject.AddComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = 0;
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.loop = false;
    }


    public void Start()
    {
       

        localCount = 0;
        if (canStart)
        {
            for (int i = 0; i < 100; ++i) // todo: make many geerations
            {
                var creature = Instantiate(Resources.Load("creatureWorld")) as GameObject;
                creature.transform.position += new Vector3(i * 30, 0, 0);
                var tm = creature.GetComponentInChildren<TestMove>();
                tm.controller = this;
                localCount++;


            }
        }

    }

    public void Update()
    {
        if (canStart&&firstStart)
        {
            Start();
            firstStart = false;
        }
    }

    public List<settings> GetNextSettings()
    {
        List<settings> result = new List<settings> { };
        try
        {
            result = BestofCreatures[CreatureCount].Settings;
            CreatureCount++;
        }
        catch(Exception e) {
            Debug.Log(e.Message);
        }
        return result;

    }


    public void StartAnotherGeneration()
    {
        
    }

    public void CheckMeOut(float score, List<settings> my) // todo Mix generations
    {
        if (globalCount==1) fstream = new FileStream(@"note.txt",
        FileMode.OpenOrCreate);
        fstream.Close();
        best.Add(new Creature() {Settings = my, score = score});

       // Debug.Log("current worst score: " + best.Last().score);
       
        count++;
        //Debug.Log(count+ " count");
        if (count == 100)
        {
            Debug.Log(globalCount+" generation ended");
            best = best.OrderBy(creature => creature.score).ToList();
            Debug.Log("current best score: " + best.First().score);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(globalCount-1, new Vector3((globalCount-1),0, best.First().score));

            count = 0;
            globalCount++;
            CreatureCount = 0;
            BestofCreatures.Clear();
            //�������� 8 ������
            //��������� -> 8*7*2 �����

            for (int i = 0; i < 8; i++)
            {
               for (int j = 0; j < 8; j++)
               {
                   if (i != j)
                   {
                       int k = 0;
                        List<settings> mainNewSettings = new List<settings>();
                        List<settings> mainNewSettings2 = new List<settings>();
                       int Middle = Random.Range(0, steps);

                        while (k < Middle)
                       {
                           settings newSettings = new settings() {number = best[i].Settings[k].number, strength = best[i].Settings[k].strength};
                           mainNewSettings.Add(newSettings);
                           newSettings = new settings() { number = best[j].Settings[k].number, strength = best[j].Settings[k].strength };
                           mainNewSettings2.Add(newSettings);

                            k++;
                       }

                        while (k < steps)
                        {
                            settings newSettings = new settings() { number = best[j].Settings[k].number, strength = best[j].Settings[k].strength };
                            mainNewSettings.Add(newSettings);
                            newSettings = new settings() { number = best[i].Settings[k].number, strength = best[i].Settings[k].strength };
                            mainNewSettings2.Add(newSettings);
                            k++;
                        }

                        BestofCreatures.Add(new Creature() { Settings = mainNewSettings });
                        BestofCreatures.Add(new Creature() { Settings = mainNewSettings2 });

                    }
                }
            }
            

            //�������� ����������
            for (int i = 0; i < BestofCreatures.Count; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    BestofCreatures[i].Settings[Random.Range(0, steps)] = new settings();
                }  
            }

            best.Clear();
            //� ����������� �������� ��������� start
            //�������� best
            //���� ������ �����!=10

            //Debug.Log(globalCount);
           // Debug.Log(BestofCreatures.Count);
            Debug.Log("Now next generation");
            if (globalCount < numberOfCycles)
                Start();
            else
            {
                String temp = "New Round! globalCount=" + globalCount + "\r\n";
                for (int i = 0; i < best[1].Settings.Count; i++)
                {
                    temp += best[0].Settings[i].number.ToString() + "   ";
                    temp += best[0].Settings[i].ToString() + "\r\n ";

                }
                byte[] array = System.Text.Encoding.Default.GetBytes(temp);
                // ������ ������� ������ � ����
                File.AppendAllText(@"note.txt", temp, Encoding.UTF8);
                Debug.Log("����� ������� � ����");
            }
        }
      // else if (count%10==0) Start();

    }


    public List<settings> GetSettings()
    {
       // var steps = 250;
        var result= new List<settings>();
        for (var i = 0; i < steps; ++i)
        {
            result.Add(new settings());
        }
        return result;
    }
}