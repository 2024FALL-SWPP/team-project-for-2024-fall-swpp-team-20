using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MapController : MonoBehaviour
{
    public GameObject bedroomPrefab;
    public GameObject myBedroom;

    public List<int> anomalies;
    public int anomalyIndex;
    public int maxAnomalyCount = 30;

    // maxBasicAnomaly: Number of basic anomaly
    public int maxBasicAnomaly;
    //maxHardAnomaly: Number of hard anomaly
    public int maxHardAnomaly;


    private void Start()
    {
        anomalies = new List<int>();
        anomalyIndex = 0;
        ShuffleAnomaly();
    }

    private void ShuffleAnomaly()
    {
        for (int i = 0; i < maxAnomalyCount; i++)
        {
            anomalies.Add(i);
        }
        System.Random rand = new System.Random();
        anomalies = anomalies.OrderBy(_ => rand.Next()).ToList();
    }
    public GameObject GenerateMap(bool haveAnomaly)
    {
        if (!haveAnomaly)
        {
            myBedroom = Instantiate(bedroomPrefab, Vector3.zero, Quaternion.identity);
            return myBedroom;
        }
        else
        {

            myBedroom = Instantiate(bedroomPrefab, Vector3.zero, Quaternion.identity, transform);
            SetAnomaly(anomalies[++anomalyIndex]);
            if (anomalyIndex >= maxAnomalyCount)
            {
                // TODO: There are two options
                // first option: just refill anomalies and keep playing game
                // second option: game over
            }
            return myBedroom;
        }
    }

    /// <summary>
    /// Modify map with given anomaly
    /// </summary>
    /// <param name="anomaly"></param>
    private void SetAnomaly(int anomaly) { }
}
