using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;

public class TimeBombAnimationController : MonoBehaviour
{
    public int initialTime = 300;
    public void StartTimeBombAnimation(GameObject timeBomb)
    {
        StartCoroutine(StartTimer(timeBomb));
        StartCoroutine(StartToggleColor(timeBomb));
    }

    public IEnumerator StartTimer(GameObject timeBomb)
    {
        while (initialTime > 0)
        {
            yield return new WaitForSeconds(1);
            initialTime--;
            int min = initialTime / 60;
            int sec = initialTime % 60;
            GameManager.GetInstance().um.SetTimerText(min, sec);
            timeBomb.transform.Find("ClockText").GetComponent<TextMeshPro>().text = $"{min:D2}:{sec:D2}";
            if (initialTime == 10) GameManager.GetInstance().sm.PlayBombBeepSound(timeBomb);
        }
        GameManager.GetInstance().sm.PlayBombExplosionSound(timeBomb);
        GameManager.GetInstance().bedInteractionManager.TryBedInteraction(BedInteractionType.Sleep);
    }

    public IEnumerator StartToggleColor(GameObject timeBomb)
    {
        while (initialTime > 0)
        {
            yield return new WaitForSeconds(0.5f);
            timeBomb.GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            timeBomb.GetComponent<Renderer>().material.color = Color.black;
        }
    }
}