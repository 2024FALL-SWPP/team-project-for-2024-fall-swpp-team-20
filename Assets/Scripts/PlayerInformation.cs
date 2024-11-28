using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    // player health used by some hard anomalies
    private float health;

    private Coroutine hurt;

    // Reset player data whenever a stage starts
    public void Initialize()
    {
        health = 100f;
        StopCoroutines();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Lava")) HurtPlayer(30 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fruit")) HurtPlayer(10);
        Destroy(other.gameObject);
    }

    // HurtPlayer can be used whenever If needed
    private void HurtPlayer(float damage)
    {
        if (health <= 0) return;
        health -= damage;
        GameManager.GetInstance().um.SetHealthImage(health);
        if (health <= 0)
        {
            if (hurt != null) StopCoroutine(hurt);
            GameManager.GetInstance().stageManager.GameOver();
        }
    }

    // HurtPlayerByHeat is used in only lava anomaly.
    public void HurtPlayerByHeat()
    {
        hurt = StartCoroutine(HurtPlayerByHeat2());
    }

    public IEnumerator HurtPlayerByHeat2()
    {
        while (true)
        {
            HurtPlayer(0.5f);
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void StopCoroutines()
    {
        if (hurt != null) StopCoroutine(hurt);
        // Add more if there is another coroutine
    }
}
