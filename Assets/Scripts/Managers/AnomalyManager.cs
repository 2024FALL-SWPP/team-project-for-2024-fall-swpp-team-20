using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System;
using Shuffle = System.Random;

public class AnomalyManager : MonoBehaviour
{
    private static AnomalyManager instance;
    public List<Anomaly> easyAnomalies;
    public List<Anomaly> hardAnomalies;
    public int easyAnomalyIndex;
    public int hardAnomalyIndex;
    public bool test;
    public int testAnomaly;
    public bool testHard;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void initializeAnomalyIndex()
    {
        easyAnomalyIndex = 0;
        hardAnomalyIndex = 0;
    }

    public bool noAnomalyCheck(int stage)
    {
        if (stage < 5 && easyAnomalyIndex >= easyAnomalies.Count)
        {
            return true;
        }
        else if ((stage == 5 && hardAnomalyIndex >= hardAnomalies.Count - 1) || (stage == 6 && hardAnomalyIndex >= hardAnomalies.Count))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void FillAnomaly()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        easyAnomalies = new List<Anomaly>();
        hardAnomalies = new List<Anomaly>();

        foreach (Type type in assembly.GetTypes())
        {
            if (type == typeof(Anomaly) || type == typeof(HardAnomaly)) continue;
            if (typeof(Anomaly).IsAssignableFrom(type))
            {
                Anomaly instance = (Anomaly)Activator.CreateInstance(type);
                if (type.IsSubclassOf(typeof(HardAnomaly)))
                {
                    hardAnomalies.Add(instance);
                }
                else
                {
                    easyAnomalies.Add(instance);
                }
            }
        }

        if (!test)
        {
            Shuffle s = new();
            easyAnomalies = easyAnomalies.OrderBy(_ => s.Next()).ToList();
            hardAnomalies = hardAnomalies.OrderBy(_ => s.Next()).ToList();
        }

        for (int index = 0; index < easyAnomalies.Count; index++)
        {
            Debug.Log($"Easy Index {index}: {easyAnomalies[index].GetType()}");
        }

        for (int index = 0; index < hardAnomalies.Count; index++)
        {
            Debug.Log($"Hard Index {index}: {hardAnomalies[index].GetType()}");
        }
    }
}