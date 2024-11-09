using TMPro;
using UnityEngine;

public class EasyDigitalClockAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        TextMeshPro digitalClockText = map.transform.Find("Bedroom").Find("digital_clock").Find("ClockText").GetComponent<TextMeshPro>();
        digitalClockText.text = "FF:FF";
    }
}