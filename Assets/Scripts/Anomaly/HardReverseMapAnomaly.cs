using UnityEngine;

public class HardReverseMapAnomaly : HardAnomaly
{
    public override void Apply(GameObject map)
    {
        map.transform.Rotate(180, 0, 0);
        PlayerController pc = GameManager.GetInstance().player.GetComponent<PlayerController>();
        pc.SetPlayerController(SpawnPosition.Reverse);
        storage.bed.tag = "Goal";
    }

    public override AnomalyCode GetAnomalyCode()
    {
        return AnomalyCode.HardReverseMap;
    }
}
