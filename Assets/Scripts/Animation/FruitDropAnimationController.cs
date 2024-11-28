using System.Collections;
using UnityEngine;

public class FruitDropAnimationController : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    private GameObject randomFruit;
    public GameObject[] floors;
    public void StartFruitDropAnimation(GameObject map)
    {
        StartCoroutine(DropFruit(map));
    }

    private IEnumerator DropFruit(GameObject map)
    {
        GameObject fruits = map.transform.Find("Fruits").gameObject;

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
}