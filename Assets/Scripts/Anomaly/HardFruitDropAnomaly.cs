using UnityEngine;

public class HardFruitDropAnomaly : HardAnomaly
{
    public override void SetHardAnomalyCode()
    {
        GameManager.GetInstance().um.ShowCharacterScript(HardAnomalyCode.FruitDrop);
    }

    public override void Apply(GameObject map)
    {
        GameManager.GetInstance().um.ShowHealthImage();
        FruitDropAnimationController fruitDropAnimationController = map.GetComponent<FruitDropAnimationController>();
        fruitDropAnimationController.StartFruitDropAnimation(map);
    }
}