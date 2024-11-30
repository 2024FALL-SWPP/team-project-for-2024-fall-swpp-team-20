using UnityEngine;

public class HardReverseMapAnomaly : HardAnomaly
{
    public override void Apply(GameObject map)
    {
        map.transform.Rotate(180, 0, 0);
        GameManager.GetInstance().player.transform.position = new Vector3(1.838f, -3.827f, 7.026f);
        GameManager.GetInstance().player.transform.rotation = Quaternion.Euler(0, 180, 0);
        map.transform.Find("Interior").Find("2nd Floor").
            Find("Apartment_01").Find("Furniture").Find("Bed").gameObject.tag = "Goal";
    }

    public override void SetHardAnomalyCode()
    {
        GameManager.GetInstance().um.ShowCharacterScript(HardAnomalyCode.ReverseMap);
    }
}