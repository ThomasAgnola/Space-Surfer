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

    private float spawnTime = 4f;
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
        

        Vector3 spawPos = new Vector3(transform.position.x, transform.position.y, asteroidSpawnDistance);
        Vector3 spawPos1 = new Vector3(transform.position.x - 1.5f, transform.position.y, asteroidSpawnDistance);
        Vector3 spawPos2 = new Vector3(transform.position.x  + 1.5f, transform.position.y, asteroidSpawnDistance);
        Instantiate(asteroidPrefab[Random.Range(0, asteroidPrefab.Length)], spawPos, Quaternion.identity);
        Instantiate(asteroidPrefab[Random.Range(0, asteroidPrefab.Length)], spawPos1, Quaternion.identity);
        Instantiate(asteroidPrefab[Random.Range(0, asteroidPrefab.Length)], spawPos2, Quaternion.identity);
    }
}
