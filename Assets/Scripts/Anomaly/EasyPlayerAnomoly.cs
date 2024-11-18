using UnityEngine;

public class EasyPlayerAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject playerSleeping = map.transform.Find("Player_Sleeping").gameObject;
        GameObject playerAwake = map.transform.Find("Player_Awake").gameObject;
        playerSleeping.SetActive(false);
        playerAwake.SetActive(true);
    }
}