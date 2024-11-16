using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnomalySceneController : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI messageText;
    public string message = "Oh no, something's gone wrong...";
    public string targetObjectName = "Barrier";
    public float delay = 0.4f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == targetObjectName)
        {
            StartCoroutine(DisplayMessage());
        }
    }

    private IEnumerator DisplayMessage()
    {
        yield return new WaitForSeconds(delay);
        messageText.text = message;
    }

    private IEnumerator Stun()
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("stunTrigger");
    }
}
