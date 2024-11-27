using System.Collections;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HardTimeBombAnomaly : HardAnomaly
{
    private string password = "1234";
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
        TimerAnimationController timerAnimationController = timeBomb.GetComponent<TimerAnimationController>();
        timerAnimationController.StartTimerAnimation();
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
}