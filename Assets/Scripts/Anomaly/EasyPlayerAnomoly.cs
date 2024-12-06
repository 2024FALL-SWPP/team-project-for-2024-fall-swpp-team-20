using UnityEngine;

public class EasyPlayerAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        ObjectStorage storage = map.GetComponent<ObjectStorage>();
        GameObject playerSleeping = storage.playerSleeping;
        GameObject playerAwake = storage.playerAwake;
        playerSleeping.SetActive(false);
        playerAwake.SetActive(true);
    }
}