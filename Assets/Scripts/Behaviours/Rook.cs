using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPieceBehaviour
{
    private const int maxHealth = 5;
    public override void Attack(Vector3 playerPos)
    {
        Debug.Log("Rook attacks");
    }

    private IEnumerator AttackCoroutine()
    {
        Vector3 playerPos = GameManager.GetInstance().player.transform.position; // world position
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4, 6));
            Attack(playerPos);
        }
    }

    public override void Activate()
    {
        base.Activate();
        health = maxHealth;
        StartCoroutine(AttackCoroutine());
    }
    private void OnDestroy()
    {
        DeadPieceCount++;
    }

    public override void Update()
    {
        if (DeadPawnCount == 8 && !activated) Activate();
        base.Update();
    }
}
