using UnityEngine;

public class EasySofaDisappearAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        ObjectStorage storage = map.GetComponent<ObjectStorage>();
        GameObject sofa = storage.sofa;
        sofa.SetActive(false);
    }
}