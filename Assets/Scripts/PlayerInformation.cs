using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    // player health used by some hard anomalies
    private float health;

    private Coroutine hurt;

    private bool invince = false;

    // Reset player data whenever a stage starts
    public void Initialize()
    {
        health = 100f;
        StopCoroutines();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!invince)
        {
            if (other.gameObject.CompareTag("Lava")) HurtPlayer(10 * Time.deltaTime, false);
            if (other.gameObject.CompareTag("ChessPiece"))
            {
                ChessPieceBehaviour cpb = other.GetComponent<ChessPieceBehaviour>();
                if (!cpb.activated) return;
                HurtPlayer(cpb.damage, false);
                invince = true;
                Invoke(nameof(RemoveInvincibility), 2);
            }
            if (other.gameObject.CompareTag("RealLine"))
            { // Bishop and Queen's attack
                HurtPlayer(10, false);
                invince = true;
                Invoke(nameof(RemoveInvincibility), 2);
            }
        }
    }

    private void RemoveInvincibility() => invince = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fruit"))
        {
            HurtPlayer(10, false);
            Destroy(other.gameObject);
        }
    }
    // HurtPlayer can be used whenever If needed
    private void HurtPlayer(float damage, bool isDOT)
    {
        if (health <= 0) return;
        if (!isDOT) GameManager.GetInstance().am.SetDamageFlag();
        health -= damage;
        GameManager.GetInstance().um.SetHealthImage(health);
        if (health <= 0)
        {
            if (hurt != null) StopCoroutine(hurt);
            GameManager.GetInstance().bedInteractionManager.TryBedInteraction(BedInteractionType.FailHard);
        }
    }

    // DOT Damage
    public void HurtPlayerByDOT(float damage)
    {
        hurt = StartCoroutine(IHurtPlayerByDOT(damage));
    }

    public IEnumerator IHurtPlayerByDOT(float damage)
    {
        while (true)
        {
            HurtPlayer(damage, true);
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void StopCoroutines()
    {
        if (hurt != null) StopCoroutine(hurt);
        // Add more if there is another coroutine
    }
}
