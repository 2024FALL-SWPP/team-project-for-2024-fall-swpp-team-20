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
    private int[] anomalies = new int[maxAnomalyCount];

    public delegate void AnomalyHandler(int anomaly);

    // 델리게이트 배열 생성 : 
    //refer by Github Copilot : 이상현상 구현 관련 basic structure 조언 받음
    private AnomalyHandler[] anomalyHandlers;

    void Start()
    {
        // 델리게이트 배열 초기화
        anomalyHandlers = new AnomalyHandler[]
        {
            HandleAnomaly1,
            HandleAnomaly2,
            HandleAnomaly3
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

    private void SetAnomaly(int anomaly)
    {
        if (anomalyIndex < anomalyHandlers.Length)
        {
            anomalyHandlers[anomalyIndex](anomaly);
        }
        else
        {
            Debug.LogWarning("No handler for anomaly index: " + anomalyIndex);
        }
    }

    // 예시 핸들러 함수들
    private void HandleAnomaly1(int anomaly)
    {
        GameObject piano = myBedroom.transform.Find("Piano").gameObject;
        piano.transform.rotation = Quaternion.Euler(-90, 0, -140);
        piano.transform.position = new Vector3(0.53f, 0.177f, 9.1f);
    }

    private void HandleAnomaly2(int anomaly)
    {
        //TODO: Implement anomaly 2
    }

    private void HandleAnomaly3(int anomaly)
    {
        //TODO: Implement anomaly 3
    }
}
