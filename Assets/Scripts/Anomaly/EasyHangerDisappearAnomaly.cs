using UnityEngine;

public class EasyHangerDisappearAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        for (int i = 0; i < 4; i++)
        {
            storage.hangers[i].SetActive(false);
        }
    }
}