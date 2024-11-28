using System.Collections;
using UnityEngine;

public class FruitDropAnimationController : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    private GameObject randomFruit;
    public GameObject[] floors;
    private GameObject fruits;

    public void StartFruitDropAnimation(GameObject map)
    {
        StartCoroutine(FruitDropAnimation(map));
    }
    public IEnumerator FruitDropAnimation(GameObject map)
    {
        fruits = map.transform.Find("Fruits").gameObject;

        fruitPrefabs = new GameObject[fruits.transform.childCount];

        for (int i = 0; i < fruits.transform.childCount; i++)
        {
            fruitPrefabs[i] = fruits.transform.GetChild(i).gameObject;
        }

        floors = new GameObject[59];

        GameObject floorsParent = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Floors").gameObject;
        for (int i = 0; i < 59; i++)
        {
            floors[i] = floorsParent.transform.Find($"Int_apt_01_Floor_01 ({i + 1})").gameObject;
        }

        while (true)
        {
            StartCoroutine(DropFruit(map));
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator DropFruit(GameObject map)
    {
        randomFruit = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

        GameObject randomFloor = floors[Random.Range(0, floors.Length)];

        Debug.Log(floors.Length);
        Debug.Log(randomFloor==null);

        Vector3 randomPosition = new Vector3(
            randomFloor.transform.position.x,
            fruits.transform.position.y,
            randomFloor.transform.position.z
        );

        GameObject randomFruitinstace =  Instantiate(randomFruit, randomPosition, Quaternion.identity);
        
        yield return new WaitForSeconds(1);

        if(randomFruitinstace) randomFruitinstace.GetComponent<Rigidbody>().useGravity = true;

        yield return new WaitForSeconds(1);
    }
}