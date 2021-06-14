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

    private float maxSpawnTime = 8f;
    private float minSpawnTime = 3f;
    private float timer = 0f;
    private float timer1 = 0f;
    private float timer2 = 0f;
    private float sauvSpawnTime;
    private float sauvSpawnTime1;
    private float sauvSpawnTime2;

    [HideInInspector]
    public float minX = -2.6f, maxX = 2.6f, minY = 0f, maxY = 2.6f;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        timer1 = 0f;
        timer2 = 0f;
        sauvSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        sauvSpawnTime1 = Random.Range(minSpawnTime, maxSpawnTime);
        sauvSpawnTime2 = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsPlaying) return;
        timer += Time.deltaTime;
        timer1 += Time.deltaTime;
        timer2 += Time.deltaTime;
        if (timer >= sauvSpawnTime)
        {
            //spawn asteroid
            SpawnNewAsteroid();
            timer = 0f;
            sauvSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }
        if (timer1 >= sauvSpawnTime1)
        {
            //spawn asteroid
            SpawnNewAsteroid1();
            timer1 = 0f;
            sauvSpawnTime1 = Random.Range(minSpawnTime, maxSpawnTime);
        }
        if (timer2 >= sauvSpawnTime2)
        {
            //spawn asteroid
            
            SpawnNewAsteroid2();
            timer2 = 0f;
            sauvSpawnTime2 = Random.Range(minSpawnTime, maxSpawnTime);
        }

    }

    private void SpawnNewAsteroid()
    {
        

        Vector3 spawPos = new Vector3(transform.position.x, transform.position.y, asteroidSpawnDistance);
        Instantiate(asteroidPrefab[Random.Range(0, asteroidPrefab.Length)], spawPos, Quaternion.identity);
        
    }

    private void SpawnNewAsteroid1()
    {


        
        Vector3 spawPos1 = new Vector3(transform.position.x - 1.5f, transform.position.y, asteroidSpawnDistance);
        Instantiate(asteroidPrefab[Random.Range(0, asteroidPrefab.Length)], spawPos1, Quaternion.identity);
        
    }

    private void SpawnNewAsteroid2()
    {


        
        Vector3 spawPos2 = new Vector3(transform.position.x + 1.5f, transform.position.y, asteroidSpawnDistance);
        Instantiate(asteroidPrefab[Random.Range(0, asteroidPrefab.Length)], spawPos2, Quaternion.identity);
    }
}
