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

    private void CleanupCurrentMap()
    {
        if (currentMap != null)
        {
            Destroy(currentMap);
            currentMap = null;
        }
        GameObject[] mapObjects = GameObject.FindGameObjectsWithTag("Map");
        foreach (GameObject mapObject in mapObjects)
        {
            Destroy(mapObject);
        }
    }

    // returns current anomaly is hard or not
    public bool GenerateMap(bool haveAnomaly, int stage)
    {
        CleanupCurrentMap();
        Anomaly anomaly = null;

        if (!haveAnomaly)
        {
            currentMap = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity, transform);
            SetClock(stage);
            Debug.Log($"Stage {stage}: No Anomaly");
            return false;
        }
        else
        {
            currentMap = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity, transform);
            SetClock(stage);
            if (test)
            {
                anomaly = anomalies[testAnomaly];
                Debug.Log($"Test: Anomaly {anomaly.GetType()}");
            }
            else
            {
                anomaly = anomalies[++anomalyIndex % anomalies.Count];
                Debug.Log($"Stage {stage}: Anomaly {anomaly.GetType()}");
            }

            if (anomalyIndex >= maxAnomalyCount)
            {
                // TODO: There are two options
                // first option: just refill anomalies and keep playing game
                // second option: game over
            }

            SetAnomaly(anomaly);
            if (anomaly == null) return false;
            return anomaly is HardAnomaly;
        }
    }

    private void SetAnomaly(Anomaly anomaly)
    {
        if (anomalyIndex < anomalies.Count)
        {
            anomaly.Apply(currentMap);
            //Do Additional Setting For Each Hard Anomaly
            if (anomaly is HardAnomaly)
            {
                HardAnomaly ha = anomaly as HardAnomaly;
                ha.SetHardAnomalyCodeForLaptop();
            }
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
