using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

public class MapController : MonoBehaviour
{
    public GameObject bedroomPrefab;
    private GameObject myBedroom;
    private int anomalyIndex = -1;
    private const int maxAnomalyCount = 10;
    private Anomaly[] anomalies;

    void Start()
    {
        // 델리게이트 배열 초기화
        anomalies = new Anomaly[]
        { gameObject.AddComponent<EasyPianoAnomaly>(),
        };
    }
    public GameObject GenerateMap(bool haveAnomaly)
    {
        if (!haveAnomaly)
        {
            myBedroom = Instantiate(bedroomPrefab, Vector3.zero, Quaternion.identity, transform);
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

    private void SetAnomaly(Anomaly anomaly)
    {
        if (anomalyIndex < anomalies.Length)
        {
            anomaly.Apply(myBedroom);
        }
    }
}
