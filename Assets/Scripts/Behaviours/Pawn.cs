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
        else {
            if (movedCount >= 6) return;
            StartCoroutine(MoveForward());
            movedCount++;
        }
    }
    private IEnumerator MoveToPlayer(Vector3 direction) {
        float time = 0;
        float deltaTime;
        while (time < 0.5f) {
            deltaTime = Time.fixedDeltaTime;
            time += deltaTime;
            transform.Translate(direction * deltaTime);
            yield return null;
        }
        transform.Translate(direction * (0.5f - time));
    }
    private IEnumerator MoveForward() {
        float time = 0;
        float deltaTime;
        while (time < 0.5f) {
            deltaTime = Time.fixedDeltaTime;
            time += deltaTime;
            transform.Translate(-2 * deltaTime * spotSize * Vector3.right, Space.World);
            yield return null;
        }
        transform.Translate((time - 0.5f) * spotSize * Vector3.right, Space.World);
    }
    private IEnumerator AttackCoroutine() {
        yield return new WaitForSeconds(Random.Range(1f, 6f));
        Attack();
        while (true) {
            yield return new WaitForSeconds(Random.Range(3f, 6f));
            Attack();
        }
    }

    public override void Activate() {
        base.Activate();
        maxHealth = 3;
        health = maxHealth;
        damage = 5;
        StartCoroutine(AttackCoroutine());
    }

    private void OnDestroy() {
        if (health == 0)
        {
            DeadPawnCount++;
            DeadPieceCount++;
        }
    }

    private float CalculateDistanceFromPlayer(out Vector3 direction) {
        direction = GetDirection();
        return Vector3.Magnitude(direction);
    }

    private Vector3 GetDirection() {
        Vector3 playerPos = GetPlayer2DPosition();
        Vector3 piecePos = transform.position;
        piecePos.y = 0f;
        return playerPos - piecePos;
    }
}
