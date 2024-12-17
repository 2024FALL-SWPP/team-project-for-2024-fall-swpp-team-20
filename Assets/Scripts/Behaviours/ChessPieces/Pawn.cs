using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPieceBehaviour
{
    private int movedCount;

    public List<GameObject> promotePieces;

    public int row;
    public override void Attack()
    {
        StartCoroutine(MoveForward());
        movedCount++;
    }
    /*private IEnumerator MoveToPlayer(Vector3 direction)
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
        
    }*/
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
        if (movedCount >= 6)
        {
            activated = false;
            Promote();
        }
    }
    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        Attack();
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            Attack();
        }
    }

    public override void Activate(bool promoted, int row)
    {
        base.Activate(promoted, row);
        maxHealth = 5;
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

    private void Promote()
    {
        int spawnIndex = Random.Range(0, 4);
        ChessPieceBehaviour newPiece = Instantiate(promotePieces[spawnIndex], transform.position, Quaternion.identity, transform.parent).GetComponent<ChessPieceBehaviour>();
        newPiece.Activate(true, row);
        Destroy(gameObject);
    }

}
