using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System;
using Shuffle = System.Random;

public class AnomalyManager : MonoBehaviour
{
    public static AnomalyManager instance;

    public List<Anomaly> anomalies;
    public bool test;
    public int testAnomaly;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void FillAnomaly()
    {
        // Asked for chatGPT about how to use assembly
        Assembly assembly = Assembly.GetExecutingAssembly();

        // filling delegate list
        anomalies = new List<Anomaly>();
        foreach (Type type in assembly.GetTypes())
        {
            //Debug.Log($"{type} isAnomaly:{type == typeof(Anomaly)} isHardAnomaly:{type == typeof(HardAnomaly)}");
            if (type == typeof(Anomaly) || type == typeof(HardAnomaly)) continue;
            if (typeof(Anomaly).IsAssignableFrom(type))
            {
                // Create an instance of each type and add it to the list.
                Anomaly instance = (Anomaly)Activator.CreateInstance(type);
                anomalies.Add(instance);
            }
        }

        if (!test)
        {
            Shuffle s = new();
            anomalies = anomalies.OrderBy(_ => s.Next()).ToList();
        }

        for (int index = 0; index < anomalies.Count; index++)
        {
            Debug.Log($"Index {index}: {anomalies[index].GetType()}");
        }
    }
}