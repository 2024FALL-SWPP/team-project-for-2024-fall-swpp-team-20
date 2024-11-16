using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnomalySceneController : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public string message = "Oh no, something's gone wrong...";
    public string targetObjectName = "Barrier";
    public float displayDelay = 0.4f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == targetObjectName)
        {
            StartCoroutine(DisplayMessage());
        }
    }

    private IEnumerator DisplayMessage()
    {
        yield return new WaitForSeconds(displayDelay);
        messageText.text = message;
    }
}
