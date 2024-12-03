using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPieceBehaviour
{
    public GameObject guideline;
    public GameObject realline;
    public override void Attack()
    {
        if (CalculateDistanceFromPlayer(out Vector3 direction) > 2 * spotSize)
        {
            direction = 2 * spotSize * Vector3.Normalize(direction);
        }
        StartCoroutine(MoveAndAttack(direction));
    }

    private IEnumerator MoveAndAttack(Vector3 direction)
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
        GameObject guideline = Instantiate(this.guideline, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(guideline);
        GameObject realline = Instantiate(this.realline, transform.position, Quaternion.identity);
    }
    private IEnumerator AttackCoroutine()
    {
        Vector3 playerPos = GameManager.GetInstance().player.transform.position; // world position
        yield return new WaitForSeconds(Random.Range(0, 1f));
        Attack();

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            Attack();
        }
    }

    private void OnDestroy()
    {
        if (health == 0)
        {
            GameManager.GetInstance().bedInteractionManager.TryBedInteraction(BedInteractionType.ClearHard);
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
