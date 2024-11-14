using UnityEngine;

public class EasyCanvasDisappearAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject Canvas = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Props").Find("Giraffe_Canvas").gameObject;
        Canvas.SetActive(false);
    }
}