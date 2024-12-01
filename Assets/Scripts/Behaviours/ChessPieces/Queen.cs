using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPieceBehaviour
{
    public (int, int) chessPosition;
    public int pos1;
    public int pos2;
    public override void Attack()
    {
        Debug.Log("Queen Attacks");
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
        maxHealth = 10;
        health = maxHealth;
        damage = 10;
        chessPosition = (pos1, pos2);
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
        Debug.Log(DeadPawnCount);
        if (DeadPawnCount == 8 && !activated) Activate();
        base.Update();
    }
}
