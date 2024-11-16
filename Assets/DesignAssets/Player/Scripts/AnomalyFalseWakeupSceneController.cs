using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnomalyFalseWakeupSceneController : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI messageText;
    public string message = "Oh no, something's gone wrong...";
    public string targetObjectName = "Barrier";
    public float delay = 0.4f;

    // 눈 감기 효과를 위한 UI Image
    public RectTransform eyesUp; // eyes_up Image의 RectTransform
    public RectTransform eyesDown; // eyes_down Image의 RectTransform
    public float eyeCloseDuration = 3f; // 눈 감기 애니메이션 지속 시간
    public Vector2 eyesUpTargetPosition = new Vector2(0, 100); // eyes_up의 목표 위치
    public Vector2 eyesDownTargetPosition = new Vector2(0, -100); // eyes_down의 목표 위치

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool isWalking = horizontal != 0 || vertical != 0;
        animator.SetBool("IsWalking", isWalking);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == targetObjectName)
        {
            StartCoroutine(DisplayMessage());
            StartCoroutine(Stun());
            StartCoroutine(CloseEyes());
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
        animator.SetTrigger("Stunned");
    }

    private IEnumerator CloseEyes()
    {
        yield return new WaitForSeconds(delay + 0.6f);

        Vector2 eyesUpStart = eyesUp.anchoredPosition;
        Vector2 eyesDownStart = eyesDown.anchoredPosition;

        float elapsed = 0f;

        while (elapsed < eyeCloseDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / eyeCloseDuration;

            // Lerp로 부드럽게 위치 이동
            eyesUp.anchoredPosition = Vector2.Lerp(eyesUpStart, eyesUpTargetPosition, t);
            eyesDown.anchoredPosition = Vector2.Lerp(eyesDownStart, eyesDownTargetPosition, t);

            yield return null; // 다음 프레임까지 대기
        }

        // 최종 위치 설정 (정확도를 위해)
        eyesUp.anchoredPosition = eyesUpTargetPosition;
        eyesDown.anchoredPosition = eyesDownTargetPosition;
    }
}
