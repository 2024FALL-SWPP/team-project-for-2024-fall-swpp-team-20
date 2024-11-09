using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Reflection;

public class MapController : MonoBehaviour
{
    public GameObject mapPrefab;
    private GameObject map;
    private int anomalyIndex = -1;
    private const int maxAnomalyCount = 50;
    private List<Anomaly> anomalies;

    //only for anomlay testing
    public bool test;
    public int testAnomaly;

    void Start()
    {
        // Asked for chatGPT about how to use assembly
        Assembly assembly = Assembly.GetExecutingAssembly();

        // filling delegate list
        anomalies = new List<Anomaly>();
        foreach (Type type in assembly.GetTypes())
        {
            if (type.IsSubclassOf(typeof(Anomaly)) && !type.IsSubclassOf(typeof(MonoBehaviour)))
            {
                // Create an instance of each type and add it to the list.
                Anomaly instance = (Anomaly)Activator.CreateInstance(type);
                anomalies.Add(instance);
            }
        }

    }
    public GameObject GenerateMap(bool haveAnomaly)
    {
        if (!haveAnomaly)
        {
            map = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity, transform);
            return map;
        }
        else
        {
            map = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity, transform);
            if (test) SetAnomaly(anomalies[testAnomaly]);
            else SetAnomaly(anomalies[++anomalyIndex % anomalies.Count]);
            if (anomalyIndex >= maxAnomalyCount)
            {
                // TODO: There are two options
                // first option: just refill anomalies and keep playing game
                // second option: game over
            }
            return map;
        }
    }

    private void SetAnomaly(Anomaly anomaly)
    {
        if (anomalyIndex < anomalies.Count)
        {
            anomaly.Apply(map);
        }
    }
}
