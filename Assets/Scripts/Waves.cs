using System;
using UnityEngine;

[System.Serializable]
public class Waves 
{
    [HideInInspector]
    public GameObject[] ennemies;

    public GameObject ennemyFast;
    public int countFast;

    public GameObject ennemyMedium;
    public int countMedium;

    public GameObject ennemySlow;
    public int countSlow;

    public float rate;


    public void init()
    {
        ennemies = new GameObject[NbEnnemy()];
    }

    public int NbEnnemy()
    {
        return countFast + countMedium + countSlow;
    }

    public void RemoveElement(int index)
    {
        for (int i = index; i < ennemies.Length - 1; i++)
        {
            ennemies[i] = ennemies[i + 1];
        }

        Array.Resize(ref ennemies, ennemies.Length - 1);
    }
}
