using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPieceBehaviour
{
    private const int maxHealth = 20;
    public override void Attack(Vector3 playerPos)
    {
        throw new System.NotImplementedException();
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

    public override void Update()
    {
        if (DeadPieceCount == 15 && !activated) Activate();
        base.Update();
    }
}
