using UnityEngine;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Collections.Generic;
using TMPro;
using Shuffle = System.Random;

public class MapManager : MonoBehaviour
{
    public GameObject mapPrefab;
    private GameObject currentMap;
    private float initialClockRotation = 240.0f;
    private int anomalyIndex = -1;
    private const int maxAnomalyCount = 50;
    private List<Anomaly> anomalies;

    //only for anomlay testing
    public bool test;
    public int testAnomaly;

    private void Start()
    {
        currentMap = GameObject.FindGameObjectWithTag("Map");
    }

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

    private void CleanupCurrentMap()
    {
        if (currentMap != null)
        {
            Destroy(currentMap);
            currentMap = null;
        }
    }

    public GameObject GenerateMap(bool haveAnomaly, int stage)
    {
        CleanupCurrentMap();

        if (!haveAnomaly)
        {
            currentMap = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity, transform);
            SetClock(stage);
            Debug.Log($"Stage {stage}: No Anomaly");
            return currentMap;
        }
        else
        {
            currentMap = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity, transform);
            SetClock(stage);
            if (test)
            {
                if (stage != 0)
                {
                    SetAnomaly(anomalies[testAnomaly]);
                    Debug.Log($"Stage {stage}: Anomaly {anomalies[testAnomaly].GetType()}");
                }
            }
            else
            {
                SetAnomaly(anomalies[++anomalyIndex % anomalies.Count]);
                if (anomalyIndex >= maxAnomalyCount)
                {
                    // TODO: There are two options
                    // first option: just refill anomalies and keep playing game
                    // second option: game over
                }
                Debug.Log($"Stage {stage}: Anomaly {anomalies[anomalyIndex % anomalies.Count].GetType()}");
            }
            return currentMap;
        }
    }

    private void SetAnomaly(Anomaly anomaly)
    {
        if (anomalyIndex < anomalies.Count)
        {
            anomaly.Apply(currentMap);
        }
    }

    private void SetClock(int stage)
    {
        GameObject clockHourHand = currentMap.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Props").Find("clock").Find("Hour Hand").gameObject;
        clockHourHand.transform.localRotation = Quaternion.Euler(-90, 0, initialClockRotation + 30 * stage);
        TextMeshPro digitalClockText = currentMap.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Props").Find("digital_clock").Find("ClockText").GetComponent<TextMeshPro>();
        digitalClockText.text = stage == 0 ? "00:00" : "0" + stage.ToString() + ":00";
    }
}
