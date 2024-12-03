using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        if (other.CompareTag("ChessPiece"))
        {
            ChessPieceBehaviour cpb = other.GetComponent<ChessPieceBehaviour>();
            if (!cpb.activated) return;
            cpb.Hurt(1);
            Destroy(gameObject);
        }
        else if (tag == "ChessWalls") Destroy(gameObject);
    }

    public void Shoot(Vector3 direction)
    {
        rb.velocity = speed * direction;
    }
}
