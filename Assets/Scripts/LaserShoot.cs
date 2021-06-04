using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{
    public GameObject Projectil;
    public int force = 10;
    public float durationLaser = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            //GameObject Laser = Instantiate(Projectil, transform.position, Quaternion.identity);
            GameObject Laser = Instantiate(Projectil, transform.position, transform.rotation);
            Laser.GetComponent<Rigidbody>().velocity = Vector3.forward * force;

            Destroy(Laser, durationLaser);
        }
    }
}