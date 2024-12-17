using UnityEngine;

public class HardFruitDropAnomaly : HardAnomaly
{
    public override void Apply(GameObject map)
    {
        GameManager.GetInstance().um.ShowHealthImage();
        GameManager.GetInstance().player.transform.position = new Vector3(0.825f, 0.2f, -6.733f);
        GameObject bed = storage.bed;
        bed.tag = "Goal";
        GameObject fruitDropAnimationControllerObject = new GameObject("FruitDropAnimationController");
        fruitDropAnimationControllerObject.transform.SetParent(map.transform);
        FruitDropAnimationController fruitDropAnimationController = fruitDropAnimationControllerObject.AddComponent<FruitDropAnimationController>();
        fruitDropAnimationController.StartFruitDropAnimation(map);
    }

    public override AnomalyCode GetAnomalyCode()
    {
        return AnomalyCode.HardFruitDrop;
    }
}
