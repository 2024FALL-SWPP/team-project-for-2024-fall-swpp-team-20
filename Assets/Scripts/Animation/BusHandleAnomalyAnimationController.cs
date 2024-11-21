using System.Collections;
using UnityEngine;

public class BusHandleAnomalyAnimationController : MonoBehaviour
{
    public void playAnimation(GameObject busHandle, float duration)
    {
        StartCoroutine(BusHandleDown(busHandle, duration));
    }

    public IEnumerator BusHandleDown(GameObject busHandle, float duration)
    {
        float elapsedTime = 0;
        Vector3 startPos = busHandle.transform.position;
        Vector3 startScale = busHandle.transform.localScale;
        Vector3 endPos = new Vector3(busHandle.transform.position.x, busHandle.transform.position.y - 0.5f, busHandle.transform.position.z);
        Vector3 endScale = new Vector3(busHandle.transform.localScale.x * 10, busHandle.transform.localScale.y * 10, busHandle.transform.localScale.z * 10);

        while (busHandle && elapsedTime < duration)
        {
            busHandle.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            busHandle.transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}