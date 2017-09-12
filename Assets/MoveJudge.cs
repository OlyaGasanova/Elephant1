using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveJudge : MonoBehaviour
{
    class Creature
    {
        public List<settings> Settings = new List<settings>();
        public float score = 0;
    }
    List<Creature> best = new List<Creature>();
    
    public void Start()
    {
        for (int i = 0; i < 10; ++i) // todo: make many geerations
        {
            var creature = Instantiate(Resources.Load("creatureWorld")) as GameObject;
            creature.transform.position += new Vector3(i*20,0,0);
            var tm = creature.GetComponentInChildren<TestMove>();
            tm.controller = this;
        }
    }

    public void CheckMeOut(float score, List<settings> my) // todo Mix generations
    {
        best.Add(new Creature() {Settings = my, score = score});
        best = best.OrderBy(creature => creature.score).ToList(); 

        Debug.Log("current best score: " + best.First().score);
        Debug.Log("current worst score: " + best.Last().score);
    }


    public List<settings> GetSettings()
    {
        var steps = 1000;
        var result= new List<settings>();
        for (var i = 0; i < steps; ++i)
        {
            result.Add(new settings());
        }
        return result;
    }
}