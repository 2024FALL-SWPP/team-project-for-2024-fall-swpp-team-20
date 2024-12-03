using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPieceBehaviour
{
    public (int, int) chessPosition;
    public int pos1;
    public int pos2;

    public GameObject guidelinePrefab;
    public GameObject reallinePrefab;

    private float speed;
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

    private List<GameObject> InitializeGuideline((int, int) chessPos)
    {
        List<GameObject> guidelines = new List<GameObject>();

        int length45 = Mathf.Min(7 - chessPos.Item1, 7 - chessPos.Item2);
        int length135 = Mathf.Min(chessPos.Item1, 7 - chessPos.Item2);
        int length225 = Mathf.Min(chessPos.Item1, chessPos.Item2);
        int length315 = Mathf.Min(7 - chessPos.Item1, chessPos.Item2);

        int length0 = 7 - chessPos.Item1;
        int length90 = 7 - chessPos.Item2;
        int length180 = chessPos.Item1;
        int length270 = chessPos.Item2;


        GameObject line45 = Instantiate(guidelinePrefab, transform.position, Quaternion.Euler(0, -45, 0));
        line45.transform.localScale = new Vector3(1, 1, Mathf.Sqrt(2) * (length45 + 0.5f * spotSize));
        guidelines.Add(line45);

        GameObject line135 = Instantiate(guidelinePrefab, transform.position, Quaternion.Euler(0, -135, 0));
        line135.transform.localScale = new Vector3(1, 1, Mathf.Sqrt(2) * (length135 + 0.5f * spotSize));
        guidelines.Add(line135);

        GameObject line225 = Instantiate(guidelinePrefab, transform.position, Quaternion.Euler(0, -225, 0));
        line225.transform.localScale = new Vector3(1, 1, Mathf.Sqrt(2) * (length225 + 0.5f * spotSize));
        guidelines.Add(line225);

        GameObject line315 = Instantiate(guidelinePrefab, transform.position, Quaternion.Euler(0, -315, 0));
        line315.transform.localScale = new Vector3(1, 1, Mathf.Sqrt(2) * (length315 + 0.5f * spotSize));
        guidelines.Add(line315);

        GameObject line0 = Instantiate(guidelinePrefab, transform.position, Quaternion.Euler(0, 0, 0));
        line0.transform.localScale = new Vector3(1, 1, length0 + 0.5f * spotSize);
        guidelines.Add(line0);

        GameObject line90 = Instantiate(guidelinePrefab, transform.position, Quaternion.Euler(0, -90, 0));
        line90.transform.localScale = new Vector3(1, 1, length90 + 0.5f * spotSize);
        guidelines.Add(line90);

        GameObject line180 = Instantiate(guidelinePrefab, transform.position, Quaternion.Euler(0, -180, 0));
        line180.transform.localScale = new Vector3(1, 1, length180 + 0.5f * spotSize);
        guidelines.Add(line180);

        GameObject line270 = Instantiate(guidelinePrefab, transform.position, Quaternion.Euler(0, -270, 0));
        line270.transform.localScale = new Vector3(1, 1, length270 + 0.5f * spotSize);
        guidelines.Add(line270);

        return guidelines;

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

    private (int, int) GetPossiblePos((int, int) currentPos)
    {
        List<(int, int)> validPositions = new List<(int, int)>();
        (int, int)[] positions = new (int, int)[8] { currentPos, currentPos, currentPos, currentPos, currentPos, currentPos, currentPos, currentPos };

        for (int i = 0; i < 8; i++)
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
        if (code < 4)
        {
            if ((1 & code) == 1) tempPosition.Item1++;
            else tempPosition.Item1--;
            if (code < 2) tempPosition.Item2++;
            else tempPosition.Item2--;
        }
        else
        {
            switch (code)
            {
                case 4:
                    tempPosition.Item1--;
                    break;
                case 5:
                    tempPosition.Item1++;
                    break;
                case 6:
                    tempPosition.Item2++;
                    break;
                case 7:
                    tempPosition.Item2++;
                    break;
            }
        }

        newPosition = tempPosition;

        if (isValid(tempPosition)) return true;
        else return false;
    }

    private bool isValid((int, int) position)
    {
        if (position.Item1 >= 0 && position.Item1 <= 7 && position.Item2 >= 0 && position.Item2 <= 7) return true;
        else return false;
    }

    public override void Activate()
    {
        base.Activate();
        maxHealth = 10;
        health = maxHealth;
        damage = 10;
        chessPosition = (pos1, pos2);
        speed = 3 * spotSize;
        StartCoroutine(AttackCoroutine());
    }
    private void OnDestroy()
    {
        if (health == 0)
        {
            DeadPieceCount++;
        }
    }

    public override void Update()
    {
        if (DeadPawnCount == 8 && !activated) Activate();
        base.Update();
    }

    private Vector3 ChessPosToWorldPos((int, int) chessPos)
    {
        return new Vector3(4.8543f + spotSize * chessPos.Item2, 0f, -0.5428f - spotSize * chessPos.Item1);
    }
}
