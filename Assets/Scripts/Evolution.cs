using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Evolution : MonoBehaviour {

    public GameObject simulationPrefab; 
    public Genome bestGenome;
    public int generations = 1;
    public float simulationTime = 15f;
    public int variationsPerGeneration = 100;

    
    private Vector3 distance = new Vector3(0, 10, 0);
    private List<Creature> creatures = new List<Creature>();
    private float bestScore;

    void Start()
    {
        StartCoroutine(Simulation());
    }

	private IEnumerator Simulation()
    {
        for(int i = 0; i < generations; i++)
        {
            CreateCreatures();
            StartSimulation();

            yield return new WaitForSeconds(simulationTime);

            StopSimulation();
            EvaluateScore();
               
            DestroyCreatures();
        }

        PrefabUtility.CreatePrefab("Assets/Best/new" + DateTime.Now.ToString("dd h mm tt") + ".prefab", CreateBestCreature());
    }

    private void CreateCreatures()
    {
        for(int i = 0; i < variationsPerGeneration; i++)
        {
            Genome genome = bestGenome.Clone().Mutate();

            Vector3 position = distance * i;
            GameObject simulation = Instantiate(simulationPrefab, position, Quaternion.identity) as GameObject;
            Creature creature = simulation.transform.Find("creature").GetComponent<Creature>();

            creature.genome = genome;
            creatures.Add(creature);
        }
    }

    private void StartSimulation()
    {
        foreach (Creature creature in creatures)
            creature.enabled = true;
    }

    private void StopSimulation()
    {
        foreach (Creature creature in creatures)
            creature.enabled = false;
    }

    public void DestroyCreatures()
    {
        foreach (Creature creature in creatures)
            Destroy(creature.transform.parent.gameObject);

        creatures.Clear();
    }

    public void EvaluateScore()
    {
        foreach (Creature creature in creatures)
        {
            float score = creature.GetScore();
            if (score > bestScore)
            {
                bestScore = score;
                bestGenome = creature.genome.Clone();
            }
        }
    }

    private GameObject CreateBestCreature()
    {
        GameObject simulation = Instantiate(simulationPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        Creature creature = simulation.transform.Find("creature").GetComponent<Creature>();
        creature.genome = bestGenome.Clone();
        return simulation;
    }
}
