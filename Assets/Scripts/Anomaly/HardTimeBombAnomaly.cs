using System.Collections;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HardTimeBombAnomaly : HardAnomaly
{
    public override void Apply(GameObject map)
    {
        laptop = map.transform.Find("Interior").Find("2nd Floor").
            Find("Apartment_01").Find("Props").Find("laptop").GetComponent<Laptop>();
        GameObject digitalClock = map.transform.Find("Interior").Find("2nd Floor").
            Find("Apartment_01").Find("Props").Find("digital_clock").gameObject;
        GameObject timeBomb = map.transform.Find("Interior").Find("2nd Floor").
            Find("Apartment_01").Find("Props").Find("timeBomb").gameObject;
        digitalClock.SetActive(false);
        timeBomb.SetActive(true);

        GameManager.GetInstance().um.ShowTimerImage();
        TimeBombAnimationController timerAnimationController = timeBomb.GetComponent<TimeBombAnimationController>();
        timerAnimationController.StartTimeBombAnimation(timeBomb);
        GameManager.GetInstance().sm.PlayTimeBombWarningSound(timeBomb);
    }

    public override void SetHardAnomalyCode()
    {
        GameManager.GetInstance().um.ShowCharacterScript(HardAnomalyCode.TimeBomb);
    }

    public void Explosion()
    {
        GameManager.GetInstance().um.HideTimerImage();
        GameManager.GetInstance().bedInteractionManager.TryBedInteraction(BedInteractionType.Sleep);
    }
    public override HardAnomalyCode GetHardAnomalyCode()
    {
        return HardAnomalyCode.TimeBomb;
    }
}