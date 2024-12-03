using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessPieceBehaviour : MonoBehaviour
{
    public static int DeadPawnCount = 0;
    public static int DeadPieceCount = 0;

    public const float spotSize = 0.0609f;

    public GameObject healthBar;

    private Vector3 healthbarPos;
    private Vector3 cameraPos;

    public int health;
    public int maxHealth;
    public bool activated = false;

    public int pieceCode;

    public int damage;

    public virtual void Attack() { }

    public virtual void Activate()
    {
        healthBar = transform.GetChild(0).gameObject;
        healthBar.SetActive(true);
        activated = true;
    }

    public virtual void Update()
    {
        if (!activated) return;

        healthbarPos = healthBar.transform.position + new Vector3(0, 0.005f, -0.025f);
        cameraPos = Camera.main.transform.position;

        if (healthbarPos.x == cameraPos.x) return;

        float degree = Mathf.Rad2Deg * Mathf.Atan2(cameraPos.z - healthbarPos.z, healthbarPos.x - cameraPos.x);
        transform.rotation = Quaternion.Euler(Vector3.up * degree);
    }

    public void Hurt(int damage)
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
        else
        {
            healthBar.transform.localScale = new Vector3(1, 1, (float)health / maxHealth);
        }
    }

    public Vector3 GetPlayer2DPosition()
    {
        Vector3 playerPos = GameManager.GetInstance().player.transform.position;
        playerPos.y = 0;
        return playerPos;
    }

    public Vector3 GetDirection()
    {
        Vector3 playerPos = GetPlayer2DPosition();
        Vector3 piecePos = transform.position;
        piecePos.y = 0f;
        return playerPos - piecePos;
    }

    public float CalculateDistanceFromPlayer(out Vector3 direction)
    {
        direction = GetDirection();
        return Vector3.Magnitude(direction);
    }
}
