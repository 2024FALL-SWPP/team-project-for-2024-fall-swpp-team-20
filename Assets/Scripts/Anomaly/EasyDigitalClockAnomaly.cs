using TMPro;
using UnityEngine;

public class EasyDigitalClockAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        TextMeshPro digitalClockText = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Props").Find("digital_clock").Find("ClockText").GetComponent<TextMeshPro>();
        digitalClockText.text = "FF:FF";
    }
}