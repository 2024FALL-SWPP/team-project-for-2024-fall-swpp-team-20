using UnityEngine;

public abstract class Anomaly
{
    public ObjectStorage storage;
    public abstract AnomalyCode GetAnomalyCode();

    public void ApplyAnomaly(GameObject map)
    {
        storage = map.GetComponent<ObjectStorage>();
        Apply(map);
    }
    public abstract void Apply(GameObject map);

    public static bool AnomalyIsHard(AnomalyCode code)
    {
        if (code == AnomalyCode.HardLava || code == AnomalyCode.HardFruitDrop ||
            code == AnomalyCode.HardChess || code == AnomalyCode.HardReverseMap ||
            code == AnomalyCode.HardTimeBomb || code == AnomalyCode.HardVisibility) return true;
        else return false;
    }
}