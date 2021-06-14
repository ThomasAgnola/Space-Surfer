using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAsteroid : MonoBehaviour
{
    #region Singleton
    public static GenerateAsteroid Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    public GameObject[] asteroidPrefab;
    public float asteroidSpawnDistance = 50f;

    private float spawnTime = 1f;
    private float timer = 0f;

    [HideInInspector]
    public float minX = -2.6f, maxX = 2.6f, minY = 0f, maxY = 2.6f;

    // Start is called before the first frame update
    void Start()
    {
        timer = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnTime)
        {
            //spawn asteroid
            SpawnNewAsteroid();
            timer = 0f;
        }
    }

    private void SpawnNewAsteroid()
    {
        float newX = Random.Range(minX, maxX);
        float newY = Random.Range(minY, maxY);

        Vector3 spawPos = new Vector3(newX, newY, asteroidSpawnDistance);

        Instantiate(asteroidPrefab[Random.Range(0, asteroidPrefab.Length)], spawPos, Quaternion.identity);
    }
}