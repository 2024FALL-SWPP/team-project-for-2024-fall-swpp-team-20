using UnityEngine;

public class EasyDrawerMissingContentAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        ObjectStorage storage = map.GetComponent<ObjectStorage>();
        GameObject missingContent = storage.drawerMissingContent;
        missingContent.SetActive(false);
    }
}