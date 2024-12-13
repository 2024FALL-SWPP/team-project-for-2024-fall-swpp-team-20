using UnityEngine;

public abstract class Anomaly
{
    public ObjectStorage storage;
    public void ApplyAnomaly(GameObject map)
    {
        storage = map.GetComponent<ObjectStorage>();
        Apply(map);
    }
    public abstract void Apply(GameObject map);
}