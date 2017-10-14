using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
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
    public int numberOfCycles =3;
    private int CreatureCount = 0;
    private int steps = 400;
    public int localCount = 0;

    private FileStream fstream;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = -1;
    }


    public void Start()
    {
        localCount = 0;
       
            for (int i = 0; i < 100; ++i) // todo: make many geerations
            {
                var creature = Instantiate(Resources.Load("creatureWorld")) as GameObject;
                creature.transform.position += new Vector3(i * 30, 0, 0);
                var tm = creature.GetComponentInChildren<TestMove>();
                tm.controller = this;
                localCount++;
           

            }
        

    }

    public List<settings> GetNextSettings()
    {
        //Debug.Log(CreatureCount+"    "+BestofCreatures.Count);
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

            count = 0;
            globalCount++;
            CreatureCount = 0;
            BestofCreatures.Clear();
            //отобрать 8 лучших
            //скрестить -> 8*7*2 детей

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
            

            //немножко мутировать
            for (int i = 0; i < BestofCreatures.Count; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    BestofCreatures[i].Settings[Random.Range(0, steps)] = new settings();
                }  
            }

            best.Clear();
            //с полученными сеттингс запустить start
            //очистить best
            //пока глобал каунт!=10

            //Debug.Log(globalCount);
           // Debug.Log(BestofCreatures.Count);
            Debug.Log("Now next generation");
            if (globalCount < numberOfCycles)
            {
        Debug.Log(numberOfCycles + " " + globalCount);
                Start();
            }
            else
            {
                //TestMove.TurnOff();
                String temp = "New Round! globalCount=" + globalCount + "\r\n";
                for (int i = 0; i < best[1].Settings.Count; i++)
                {
                    temp += best[0].Settings[i].number.ToString() + "   ";
                    temp += best[0].Settings[i].ToString() + "\r\n ";

                }
                byte[] array = System.Text.Encoding.Default.GetBytes(temp);
                // запись массива байтов в файл
                File.AppendAllText(@"note.txt", temp, Encoding.UTF8);
                Debug.Log("Текст записан в файл");
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