using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float moveSpeed = 20f;
    private Rigidbody rb;
    private Vector3 randomRotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        randomRotation = new Vector3(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsPlaying)
        {
            Vector3 movementVector = new Vector3(0f, 0f, 0f);
            rb.velocity = movementVector;
            return;
        }
        else
        {
            Vector3 movementVector = new Vector3(0f, 0f, -moveSpeed * Time.deltaTime);
            rb.velocity = movementVector;
            transform.Rotate(randomRotation * Time.deltaTime);
        }
    }

    public void DestroyAsteroid()
    {

    }
}
