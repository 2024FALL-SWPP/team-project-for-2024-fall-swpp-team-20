using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPieceBehaviour
{

    public (int, int) chessPosition;
    public int pos1;
    public int pos2;
    private float speed;

    public GameObject guidelinePrefab;
    public GameObject reallinePrefab;

    public override void Attack()
    {
        (int, int) targetPos = GetPossiblePos(chessPosition);

        StartCoroutine(MoveAndAttack(targetPos));
    }

    private IEnumerator MoveAndAttack((int, int) targetPos)
    {
        Vector3 direction = ChessPosToWorldPos(targetPos) - ChessPosToWorldPos(chessPosition);
        float movingTime = Vector3.Magnitude(direction) / speed;
        float time = 0f;
        while (time < movingTime)
        {
            float deltaTime = Time.deltaTime;
            time += deltaTime;
            transform.position = ChessPosToWorldPos(chessPosition) + Vector3.up * transform.position.y + direction * (time / movingTime);
            yield return null;
        }
        transform.position = ChessPosToWorldPos(targetPos) + Vector3.up * transform.position.y;
        chessPosition = targetPos;

        //Attack
        List<GameObject> guideline = InitializeGuideline(chessPosition);
        yield return new WaitForSeconds(0.6f);
        HideGuidelines(guideline);
        yield return new WaitForSeconds(0.1f);
        ShowGuidelines(guideline);
        yield return new WaitForSeconds(0.45f);
        HideGuidelines(guideline);
        yield return new WaitForSeconds(0.1f);
        ShowGuidelines(guideline);
        yield return new WaitForSeconds(0.3f);
        HideGuidelines(guideline);
        yield return new WaitForSeconds(0.1f);
        ShowGuidelines(guideline);
        yield return new WaitForSeconds(0.1f);
        HideGuidelines(guideline);
        yield return new WaitForSeconds(0.1f);
        ShowGuidelines(guideline);
        yield return new WaitForSeconds(0.1f);
        HideGuidelines(guideline);
        yield return new WaitForSeconds(0.1f);
        StartAttack(guideline);
    }
    private void HideGuidelines(List<GameObject> guidelines)
    {
        foreach (GameObject guideline in guidelines)
        {
            guideline.SetActive(false);
        }
    }

    private void StartAttack(List<GameObject> guidelines)
    {
        foreach (GameObject guideline in guidelines)
        {
            LineBehaviour realline = Instantiate(reallinePrefab, transform.position, guideline.transform.rotation).GetComponent<LineBehaviour>();
            realline.ShowSlowly(guideline.transform.localScale.z);
            Destroy(guideline);
        }
    }

    private void ShowGuidelines(List<GameObject> guidelines)
    {
        foreach (GameObject guideline in guidelines)
        {
            guideline.SetActive(true);
        }
    }
    private List<GameObject> InitializeGuideline((int, int) chessPos)
    {
        List<GameObject> guidelines = new List<GameObject>();

        int length45 = Mathf.Min(7 - chessPos.Item1, 7 - chessPos.Item2);
        int length135 = Mathf.Min(chessPos.Item1, 7 - chessPos.Item2);
        int length225 = Mathf.Min(chessPos.Item1, chessPos.Item2);
        int length315 = Mathf.Min(7 - chessPos.Item1, chessPos.Item2);

        if (length45 > 0)
        {
            GameObject line45 = Instantiate(guidelinePrefab, transform.position, Quaternion.Euler(0, -45, 0));
            line45.transform.localScale = new Vector3(1, 1, Mathf.Sqrt(2) * (length45 + 0.5f * spotSize));
            guidelines.Add(line45);
        }
        if (length135 > 0)
        {
            GameObject line135 = Instantiate(guidelinePrefab, transform.position, Quaternion.Euler(0, -135, 0));
            line135.transform.localScale = new Vector3(1, 1, Mathf.Sqrt(2) * (length135 + 0.5f * spotSize));
            guidelines.Add(line135);
        }
        if (length225 > 0)
        {
            GameObject line225 = Instantiate(guidelinePrefab, transform.position, Quaternion.Euler(0, -225, 0));
            line225.transform.localScale = new Vector3(1, 1, Mathf.Sqrt(2) * (length225 + 0.5f * spotSize));
            guidelines.Add(line225);
        }
        if (length315 > 0)
        {
            GameObject line315 = Instantiate(guidelinePrefab, transform.position, Quaternion.Euler(0, -315, 0));
            line315.transform.localScale = new Vector3(1, 1, Mathf.Sqrt(2) * (length315 + 0.5f * spotSize));
            guidelines.Add(line315);
        }

        return guidelines;

    }
    private IEnumerator AttackCoroutine()
    {
        Vector3 playerPos = GameManager.GetInstance().player.transform.position; // world position

        yield return new WaitForSeconds(Random.Range(1, 5));
        Attack();
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10, 15));
            Attack();
        }
    }

    public override void Activate()
    {
        base.Activate();
        maxHealth = 5;
        health = maxHealth;
        damage = 10;
        speed = spotSize * 5;
        chessPosition = (pos1, pos2);
        StartCoroutine(AttackCoroutine());
    }
    private void OnDestroy()
    {
        if (health == 0) DeadPieceCount++;
    }

    public override void Update()
    {
        if (DeadPawnCount == 8 && !activated) Activate();
        base.Update();
    }

    private (int, int) GetPossiblePos((int, int) currentPos)
    {
        List<(int, int)> validPositions = new List<(int, int)>();
        (int, int)[] positions = new (int, int)[4] { currentPos, currentPos, currentPos, currentPos };

        for (int i = 0; i < 4; i++)
        {
            while (RearrangeVector(positions[i], i, out positions[i]))
            {
                validPositions.Add(positions[i]);
            }
        }
        int randIndex = Random.Range(0, validPositions.Count);
        return validPositions[randIndex];
    }
    private bool RearrangeVector((int, int) position, int code, out (int, int) newPosition)
    {
        (int, int) tempPosition = position;
        if ((1 & code) == 1) tempPosition.Item1++;
        else tempPosition.Item1--;
        if (code < 2) tempPosition.Item2++;
        else tempPosition.Item2--;

        newPosition = tempPosition;

        if (isValid(tempPosition)) return true;
        else return false;
    }
    private bool isValid((int, int) position)
    {
        if (position.Item1 >= 0 && position.Item1 <= 7 && position.Item2 >= 0 && position.Item2 <= 7) return true;
        else return false;
    }
    private Vector3 ChessPosToWorldPos((int, int) chessPos)
    {
        return new Vector3(4.8543f + spotSize * chessPos.Item2, 0f, -0.5428f - spotSize * chessPos.Item1);
    }
}
