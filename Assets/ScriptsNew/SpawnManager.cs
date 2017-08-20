using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public int maxPlatforms = 20;
    public GameObject platform;
    public GameObject platformVertical;
    public GameObject platformFinal;
    public float horizontalMin = 6.5f;
    public float horizontalMax = 14f;
    public float verticalMin = -6f;
    public float verticalMax = 6f;
    public int lvl = 1;


    private Vector2 originalPosition;
    private Vector2 randomPosition;

    // Use this for initialization
    void Start ()
    {
        GameController.nivel = lvl;
        originalPosition = transform.position;
        Spawn();
	}
	
	void Spawn ()
    {
		for (int i = 0; i < (maxPlatforms + (GameController.nivel^2)); i++)
        {
            float num = Random.Range(1, 7 - GameController.nivel);
            if (num > 1)
            {
                Vector2 randomPosition = originalPosition + new Vector2(Random.Range(horizontalMin, horizontalMax), Random.Range(verticalMin, verticalMax));
                Instantiate(platform, randomPosition, Quaternion.identity);
                originalPosition = randomPosition;
            }
            else
            {
                Vector2 randomPosition = originalPosition + new Vector2(Random.Range(horizontalMin + 2, horizontalMax + 2), Random.Range(verticalMin - 1, verticalMax - 1));
                Instantiate(platformVertical, randomPosition, Quaternion.Euler(0,0,-90));
                originalPosition = randomPosition;
            }
        }
        randomPosition = originalPosition + new Vector2(Random.Range(horizontalMin, horizontalMax), Random.Range(verticalMin, verticalMax));
        Instantiate(platformFinal, randomPosition, Quaternion.identity);
        originalPosition = randomPosition;
    }
}
