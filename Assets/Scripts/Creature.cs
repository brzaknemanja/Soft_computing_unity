using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

    public Genome genome; //Creature genotype
    public Transform head; //Cretures head object
    public LegController left; //Left leg controller
    public LegController right; //Right leg controller

	
    public void FixedUpdate()
    {
        //Set current leg positions based on time passed since start of program
        left.position = genome.left.EvaluateAt(Time.time);
        right.position = genome.right.EvaluateAt(Time.time);
    }

    /// <summary>
    /// Calculates creature success score
    /// </summary>
    public float GetScore()
    {
        float walkingDistance = head.position.x;
        return walkingDistance
             * (IsDown() ? 0.5f : 1f)
             + (IsUp() ? 2f : 0);
    }

    /// <summary>
    /// Checks if creature has fallen on ground
    /// </summary>
    private bool IsDown()
    {
        return head.eulerAngles.z > 135 && head.eulerAngles.z < 225;
    }

    /// <summary>
    /// Checks if creature is standing up
    /// </summary>
    private bool IsUp()
    {
        return head.eulerAngles.z < 30 || head.eulerAngles.z > 330;
    }


}
