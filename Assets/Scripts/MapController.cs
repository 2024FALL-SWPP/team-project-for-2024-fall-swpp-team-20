using UnityEngine;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Collections.Generic;
using TMPro;
using Shuffle = System.Random;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    public GameObject mapPrefab;
    private GameObject currentMap;
    private float initialClockRotation = 240.0f;
    private int anomalyIndex = -1;
    private const int maxAnomalyCount = 50;

    private AnomalyManager anomalyManager;

    //only for anomaly testing

    private void Start()
    {
        currentMap = GameObject.FindGameObjectWithTag("Map");
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
    public HardAnomalyCode GenerateMap(bool haveAnomaly, int stage)
    {
        CleanupCurrentMap();
        Anomaly anomaly = null;
        anomalyManager = FindObjectOfType<AnomalyManager>();
        bool test = anomalyManager.test;
        int testAnomaly = anomalyManager.testAnomaly;

        if (!haveAnomaly)
        {
            currentMap = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity, transform);
            SetClock(stage);
            Debug.Log($"Stage {stage}: No Anomaly");
            return HardAnomalyCode.NotInHard;
        }
        else
        {
            currentMap = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity, transform);
            SetClock(stage);
            if (test)
            {
                anomaly = anomalyManager.anomalies[testAnomaly];
                Debug.Log($"Test: Anomaly {anomaly.GetType()}");
            }
            else
            {
                anomaly = anomalyManager.anomalies[++anomalyIndex % anomalyManager.anomalies.Count];
                Debug.Log($"Stage {stage}: Anomaly {anomaly.GetType()}");
            }

            if (anomalyIndex >= maxAnomalyCount)
            {
                // TODO: There are two options
                // first option: just refill anomalies and keep playing game
                // second option: game over
            }

            SetAnomaly(anomaly);
            if (anomaly is HardAnomaly)
            {
                return (anomaly as HardAnomaly).GetHardAnomalyCode();
            }
            else
            {
                return HardAnomalyCode.NotInHard;
            }
        }
    }

    private void SetAnomaly(Anomaly anomaly)
    {
        if (anomalyIndex < anomalyManager.anomalies.Count)
        {
            anomaly.Apply(currentMap);
            //Do Additional Setting For Each Hard Anomaly
            if (anomaly is HardAnomaly)
            {
                HardAnomaly ha = anomaly as HardAnomaly;
                ha.SetHardAnomalyCode();
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
