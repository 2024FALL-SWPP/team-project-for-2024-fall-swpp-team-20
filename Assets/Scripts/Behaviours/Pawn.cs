using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPieceBehaviour
{
    private const int maxHealth = 3;

    public override void Attack(Vector3 playerPos)
    {
        Debug.Log("Pawn Attacks");
    }
    

    private IEnumerator AttackCoroutine() {
        Vector3 playerPos = GameManager.GetInstance().player.transform.position; // world position
        yield return new WaitForSeconds(Random.Range(1f, 10f));
        Attack(playerPos);
        while (true) {
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            Attack(playerPos);
        }
    }

    public override void Activate() {
        base.Activate();
        health = maxHealth;
        StartCoroutine(AttackCoroutine());
    }

    private void OnDestroy() {
        DeadPawnCount++;
        DeadPieceCount++;
    }
}
