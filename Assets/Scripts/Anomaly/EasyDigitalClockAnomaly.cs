using TMPro;
using UnityEngine;

public class EasyDigitalClockAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        ObjectStorage storage = map.GetComponent<ObjectStorage>();
        TextMeshPro digitalClockText = storage.digitalClockText.GetComponent<TextMeshPro>();
        digitalClockText.text = "FF:FF";
    }
}