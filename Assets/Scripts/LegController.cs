using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour {

    public DistanceJoint2D spring; //Distance joint attached to leg object

    private float contractedDistance; //Contracted joint distance
    private float relaxedDistance; //Relaxed joint distance

    [Range(-1, +1)]
    public float position = +1; //Leg position, -1 is contracted distance, +1 is relaxed distance

    public float maxDistanceMultiplier;
    public float minDistanceMultiplier;

	void Start () {
        float distance = spring.distance; 
        relaxedDistance = distance * maxDistanceMultiplier; 
        contractedDistance = distance * minDistanceMultiplier;
	}

    void FixedUpdate()
    {
        //Based on current position value calculate spring distance
        spring.distance = linearInterpolation(-1, +1, contractedDistance, relaxedDistance, position);
    }

    public static float linearInterpolation(float x0, float x1, float y0, float y1, float x)
    {
        float d = x1 - x0;
        if (d == 0)
            return (y0 + y1) / 2;
        return y0 + (x - x0) * (y1 - y0) / d;
    }
}