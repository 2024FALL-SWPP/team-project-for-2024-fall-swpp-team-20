using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPieceBehaviour
{
    private float speed;
    public override void Attack()
    {
        Vector3 direction = GetDirection();
        StartCoroutine(Move(direction));
    }

    private IEnumerator Move(Vector3 direction)
    {
        Vector3 piecePos = transform.position;
        float movedX = 0;
        float movedZ = 0;
        while (movedX < Mathf.Abs(direction.x))
        {
            movedX += speed * Time.deltaTime;
            transform.Translate(Mathf.Sign(direction.x) * Vector3.right * speed * Time.deltaTime, Space.World);
            yield return null;
        }
        while (movedZ < Mathf.Abs(direction.z))
        {
            movedZ += speed * Time.deltaTime;
            transform.Translate(Mathf.Sign(direction.z) * Vector3.forward * speed * Time.deltaTime, Space.World);
            yield return null;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(3, 7));
        Attack();
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 7));
            Attack();
        }
    }

    public override void Activate(bool promoted)
    {
        base.Activate(promoted);
        maxHealth = 10;
        health = maxHealth;
        damage = 10;
        speed = spotSize * 3;
        StartCoroutine(AttackCoroutine());
    }
    private void OnDestroy()
    {
        if (health == 0)
        {

            if (promoted) DeadPawnCount++;
            DeadPieceCount++;
        }
    }

    public override void Update()
    {
        if (DeadPawnCount == 8 && !activated) Activate(false);
        base.Update();
    }
}
