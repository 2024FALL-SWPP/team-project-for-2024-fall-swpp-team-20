using UnityEngine;

public class EasyPlayerAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject playerSleeping = storage.playerSleeping;
        GameObject playerAwake = storage.playerAwake;
        playerSleeping.SetActive(false);
        playerAwake.SetActive(true);
    }

    public override AnomalyCode GetAnomalyCode()
    {
        return AnomalyCode.EasyPlayer;
    }
}