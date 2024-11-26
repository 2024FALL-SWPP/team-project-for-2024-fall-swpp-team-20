using UnityEngine;
using System.Collections;

public class TimerAnimationController : MonoBehaviour
{
    public int initialTime = 300;
    public void StartTimerAnimation()
    {
        StartCoroutine(StartTimer());
    }

    public IEnumerator StartTimer()
    {
        while(initialTime > 0)
        {
            yield return new WaitForSeconds(1);
            initialTime--;
            GameManager.GetInstance().um.SetTimerText(initialTime/60, initialTime%60);
        }
        GameManager.GetInstance().bedInteractionManager.TryBedInteraction(BedInteractionType.Sleep);
    }
}