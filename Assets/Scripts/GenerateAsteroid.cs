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

    private float spawnTime = 4f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        timer = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsPlaying) return;
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
    Vector3 spawPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    Vector3 spawPos1 = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z);
    Vector3 spawPos2 = new Vector3(transform.position.x  + 1.5f, transform.position.y, transform.position.z);
    Instantiate(asteroidPrefab[Random.Range(0, asteroidPrefab.Length)], spawPos, Quaternion.identity);
    Instantiate(asteroidPrefab[Random.Range(0, asteroidPrefab.Length)], spawPos1, Quaternion.identity);
    Instantiate(asteroidPrefab[Random.Range(0, asteroidPrefab.Length)], spawPos2, Quaternion.identity);
    }
}
