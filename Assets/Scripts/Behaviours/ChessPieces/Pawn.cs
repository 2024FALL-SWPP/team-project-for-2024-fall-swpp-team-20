using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPieceBehaviour
{
    private int movedCount;


    public override void Attack()
    {
        if (CalculateDistanceFromPlayer(out Vector3 direction) < Mathf.Sqrt(2) * spotSize)
        {
            StartCoroutine(MoveToPlayer(direction));
        }
        else
        {
            if (movedCount >= 6) return;
            StartCoroutine(MoveForward());
            movedCount++;
        }
    }
    private IEnumerator MoveToPlayer(Vector3 direction)
    {
        Vector3 initialPos = transform.position;
        float time = 0;
        while (time < 0.5f)
        {
            time += Time.deltaTime;
            transform.position = initialPos + direction * (time / 0.5f);
            yield return null;
        }
        transform.position = initialPos + direction;
    }
    private IEnumerator MoveForward()
    {
        Vector3 initialPos = transform.position;
        Vector3 direction = Vector3.left * spotSize;
        float time = 0;
        while (time < 0.5f)
        {
            time += Time.deltaTime;
            transform.position = initialPos + direction * (time / 0.5f);
            yield return null;
        }
        transform.position = initialPos + direction;
    }
    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(1f, 6f));
        Attack();
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 6f));
            Attack();
        }
    }

    public override void Activate()
    {
        base.Activate();
        maxHealth = 3;
        health = maxHealth;
        damage = 5;
        StartCoroutine(AttackCoroutine());
    }

    private void OnDestroy()
    {
        if (health == 0)
        {
            DeadPawnCount++;
            DeadPieceCount++;
        }
    }



}
