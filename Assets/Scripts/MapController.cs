using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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
            // 필요한 만큼 추가
        };
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
        Debug.Log("Handling anomaly 1: " + anomaly);
    }

    private void HandleAnomaly2(int anomaly)
    {
        Debug.Log("Handling anomaly 2: " + anomaly);
    }

    private void HandleAnomaly3(int anomaly)
    {
        Debug.Log("Handling anomaly 3: " + anomaly);
    }
}
