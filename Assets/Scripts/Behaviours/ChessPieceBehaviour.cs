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
    public bool activated = false;

    public int pieceCode;

    public virtual void Attack(Vector3 playerPos) { }

    public virtual void Activate() {
        healthBar = transform.GetChild(0).gameObject;
        healthBar.SetActive(true);
        activated = true;      
    }

    public virtual void Update() {
        if (!activated) return;

        healthbarPos = healthBar.transform.position + new Vector3(0, 0.005f, -0.025f);
        cameraPos = Camera.main.transform.position;

        if (healthbarPos.x == cameraPos.x) return;

        float degree = Mathf.Rad2Deg * Mathf.Atan2(cameraPos.z - healthbarPos.z, healthbarPos.x - cameraPos.x);
        healthBar.transform.rotation = Quaternion.Euler(Vector3.up * degree);
    }
}
