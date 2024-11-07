using UnityEngine;

public class EasyPianoAnomaly : Anomaly
{
    public override void Apply(GameObject myBedroom)
    {
        GameObject piano = myBedroom.transform.Find("Piano").gameObject;
        piano.transform.rotation = Quaternion.Euler(-90, 0, -140);
        piano.transform.position = new Vector3(0.53f, 0.177f, 9.1f);
    }
}