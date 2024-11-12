using UnityEngine;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Collections.Generic;
using TMPro;
using Shuffle = System.Random;

public class MapController : MonoBehaviour
{
    public GameObject mapPrefab;
    private GameObject map;
    private float initialClockRotation = 240.0f;
    private int anomalyIndex = -1;
    private const int maxAnomalyCount = 50;
    private List<Anomaly> anomalies;

    //only for anomlay testing
    public bool test;
    public int testAnomaly;

    public void FillAnomaly()
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

    public GameObject GenerateMap(bool haveAnomaly, int stage)
    {
        if (!haveAnomaly)
        {
            map = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity, transform);
            SetClock(stage);
            Debug.Log($"Stage {stage}: No Anomaly");
            return map;
        }
        else
        {
            map = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity, transform);
            SetClock(stage);
            if (test) SetAnomaly(anomalies[testAnomaly]);
            else SetAnomaly(anomalies[++anomalyIndex % anomalies.Count]);
            if (anomalyIndex >= maxAnomalyCount)
            {
                // TODO: There are two options
                // first option: just refill anomalies and keep playing game
                // second option: game over
            }
            Debug.Log($"Stage {stage}: Anomaly {anomalies[anomalyIndex].GetType()}");
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

    private void SetClock(int stage)
    {
        GameObject clockHourHand = map.transform.Find("Bedroom").Find("clock").Find("Hour Hand").gameObject;
        clockHourHand.transform.localRotation = Quaternion.Euler(-90, 0, initialClockRotation + 30 * stage);
        TextMeshPro digitalClockText = map.transform.Find("Bedroom").Find("digital_clock").Find("ClockText").GetComponent<TextMeshPro>();
        digitalClockText.text = stage == 0 ? "00:00" : "0" + stage.ToString() + ":00";
    }
}
