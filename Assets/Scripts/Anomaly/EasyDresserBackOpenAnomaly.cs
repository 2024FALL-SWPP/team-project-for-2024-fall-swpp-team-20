using UnityEngine;

public class EasyDresserBackOpenAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        ObjectStorage storage = map.GetComponent<ObjectStorage>();
        GameObject dresser = storage.dresser;
        GameObject backOpenedDresser = storage.backOpenedDresser;
        dresser.SetActive(false);
        backOpenedDresser.SetActive(true);
    }
}