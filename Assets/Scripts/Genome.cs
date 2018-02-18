using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GenomeLeg
{
    public float m; //Minimum value
    public float M; //Maximum value
    public float o; //X offset
    public float p; //Period

    /// <summary>
    /// Parameterized sine function
    /// </summary>
    /// <param name="time">Current time</param>
    /// <returns>Returns function value of time parameter</returns>
    public float EvaluateAt(float time)
    {
        return (M - m) / 2 * (1 + Mathf.Sin((time + o) * Mathf.PI * 2 / p)) + m;
    }

    public GenomeLeg Clone()
    {
        GenomeLeg leg = new GenomeLeg();
        leg.m = m;
        leg.M = M;
        leg.o = o;
        leg.p = p;
        return leg;
    }

    /// <summary>
    /// Randomly mutates one parameter
    /// </summary>
    public void Mutate()
    {
        switch (Random.Range(0, 3 + 1))
        {
            case 0:
                m += Random.Range(-0.1f, 0.1f);
                m = Mathf.Clamp(m, -1f, +1f);
                break;
            case 1:
                M += Random.Range(-0.1f, 0.1f);
                M = Mathf.Clamp(M, -1f, +1f);
                break;
            case 2:
                p += Random.Range(-0.25f, 0.25f);
                p = Mathf.Clamp(p, 0.1f, 2f);
                break;
            case 3:
                o += Random.Range(-0.25f, 0.25f);
                o = Mathf.Clamp(o, -2f, 2f);
                break;
        }
    }
}

[System.Serializable]
public struct Genome
{
    public GenomeLeg left;
    public GenomeLeg right;

    public Genome Clone()
    {
        Genome genome = new Genome();
        genome.left = left.Clone();
        genome.right = right.Clone();
        return genome;
    }

    /// <summary>
    /// Randomly mutates one of the legs
    /// </summary>
    public Genome Mutate()
    {
        if( Random.Range(0f, 1f) > 0.5f)
        {
            left.Mutate();
        }
        else
        {
            right.Mutate();
        }

        return this;
    }
}

