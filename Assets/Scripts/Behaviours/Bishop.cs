using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPieceBehaviour
{
    public override void Attack()
    {
        Debug.Log("Bishop Attacks");
    }
    private IEnumerator AttackCoroutine()
    {
        Vector3 playerPos = GameManager.GetInstance().player.transform.position; // world position
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(4f);
        }
    }

    public override void Activate()
    {
        base.Activate();
        maxHealth = 5;
        health = maxHealth;
        damage = 10;
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
}
