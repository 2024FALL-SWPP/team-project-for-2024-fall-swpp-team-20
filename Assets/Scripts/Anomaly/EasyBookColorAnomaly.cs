using UnityEngine;

public class EasyBookColorAnomaly : Anomaly
{
    private Renderer[] bookCoverRenderer = new Renderer[8];
    private Color[] anomalyColor =
    {
        Color.red,
        new Color(1.0f, 0.5f, 0.0f), // Orange
        Color.yellow,
        Color.green,
        Color.blue,
        new Color(0.29f, 0.0f, 0.51f), // Indigo
        new Color(0.56f, 0.0f, 1.0f), // Violet
        Color.magenta
    };

    public override void Apply(GameObject myBedroom)
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject bookCollection = myBedroom.transform.Find("Studyroom").Find("book_collection" + i).gameObject;
            for (int j = 0; j < 8; j++)
            {
                bookCoverRenderer[j] = bookCollection.transform.Find("book" + j).Find("book_cover").GetComponent<Renderer>();
                bookCoverRenderer[j].material.color = anomalyColor[j];
            }
        }
    }
}