using UnityEngine;
using TMPro;

public class MapController : MonoBehaviour
{
    public GameObject mapPrefab;
    private GameObject map;
    private float initialClockRotation = 240.0f;
    private int anomalyIndex = -1;
    private const int maxAnomalyCount = 10;
    private Anomaly[] anomalies;

    //only for anomlay testing
    public bool test;
    public int testAnomaly;

    void Start()
    {
        anomalies = new Anomaly[]
        {
            gameObject.AddComponent<EasyPianoAnomaly>(),
            gameObject.AddComponent<EasyDiceAnomaly>(),
            gameObject.AddComponent<EasyLaptopAnomaly>(),
            gameObject.AddComponent<EasyDigitalClockAnomaly>(),
            gameObject.AddComponent<EasySpintopAnomaly>(),
        };
    }
    public GameObject GenerateMap(bool haveAnomaly, int stage)
    {
        if (!haveAnomaly)
        {
            map = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity, transform);
            SetClock(stage);
            return map;
        }
        else
        {
            map = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity, transform);
            SetClock(stage);
            if (test) SetAnomaly(anomalies[testAnomaly]);
            else SetAnomaly(anomalies[++anomalyIndex % anomalies.Length]);
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
        if (anomalyIndex < anomalies.Length)
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
