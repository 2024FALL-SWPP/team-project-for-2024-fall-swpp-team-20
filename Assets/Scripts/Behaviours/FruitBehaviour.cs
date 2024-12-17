using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
