using UnityEngine;

public class EasySofaDisappearAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject sofa = storage.sofa;
        sofa.SetActive(false);
    }
}