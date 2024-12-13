using System.Collections;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HardTimeBombAnomaly : HardAnomaly
{
    public override void Apply(GameObject map)
    {
        laptop = storage.laptopObject.GetComponent<Laptop>();
        GameObject digitalClock = storage.digitalClock;
        GameObject timeBomb = storage.timeBomb.gameObject;
        digitalClock.SetActive(false);
        timeBomb.SetActive(true);

        GameManager.GetInstance().um.ShowTimerImage();
        TimeBombAnimationController timerAnimationController = timeBomb.GetComponent<TimeBombAnimationController>();
        timerAnimationController.StartTimeBombAnimation(timeBomb);
        GameManager.GetInstance().sm.PlayTimeBombWarningSound(timeBomb);
    }

    public override HardAnomalyCode GetHardAnomalyCode()
    {
        return HardAnomalyCode.TimeBomb;
    }
}
