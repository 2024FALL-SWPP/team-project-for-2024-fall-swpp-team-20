using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPieceBehaviour
{
    public override void Attack()
    {
        Debug.Log("King attacks");
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

    private void OnDestroy()
    {
        if (health == 0) { 
            // Do Something
        }
    }
    public override void Activate()
    {
        base.Activate();
        maxHealth = 20;
        health = maxHealth;
        damage = 15;
        StartCoroutine(AttackCoroutine());
    }

    public override void Update()
    {
        if (DeadPieceCount == 15 && !activated) Activate();
        base.Update();
    }
}
