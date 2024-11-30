using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPieceBehaviour
{
    private const int maxHealth = 5;
    public override void Attack(Vector3 playerPos)
    {
        Debug.Log("Bishop Attacks");
    }
    private IEnumerator AttackCoroutine()
    {
        Vector3 playerPos = GameManager.GetInstance().player.transform.position; // world position
        while (true)
        {
            Attack(playerPos);
            yield return new WaitForSeconds(4f);
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
