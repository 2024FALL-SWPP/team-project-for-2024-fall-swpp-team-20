using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private List<string> chessPieces =new List<string> { "Pawn", "Knight", "Bishop", "Rook", "Queen", "King" };

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        if (chessPieces.Contains(tag)){ 
            // damage to piece if it is activated
        }
    }
}
