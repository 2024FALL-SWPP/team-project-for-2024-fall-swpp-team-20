using System.Collections;
using UnityEngine;

public class FruitDropAnimationController : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    public GameObject fruit;
    public void Start()
    {
        fruitPrefabs = Resources.LoadAll<GameObject>("DesignAssets/HardAnomaly/Prefabs");
    }
    public void StartFruitDropAnimation(GameObject map)
    {
        StartCoroutine(DropFruit(map));
    }

    private IEnumerator DropFruit(GameObject map)
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            
            GameObject randomFruit = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

            Vector3 randomPosition = new Vector3(
                Random.Range(map.transform.position.x - map.transform.localScale.x / 2, map.transform.position.x + map.transform.localScale.x / 2),
                map.transform.position.y,
                Random.Range(map.transform.position.z - map.transform.localScale.z / 2, map.transform.position.z + map.transform.localScale.z / 2)
            );

            Instantiate(randomFruit, randomPosition, Quaternion.identity);
        }
    }
}