using TMPro;
using UnityEngine;

public class EasyDigitalClockAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        TextMeshPro digitalClockText = storage.digitalClockText.GetComponent<TextMeshPro>();
        digitalClockText.text = "FF:FF";
    }
    public override AnomalyCode GetAnomalyCode()
    {
        return AnomalyCode.EasyDigitalClock;
    }
}