using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{
    public GameObject Projectil;
    public int force = 30;
    public float durationLaser = 5;
    public AudioSource laserSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsPlaying) return;
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject Laser = Instantiate(Projectil, transform.position, transform.rotation);
            Laser.GetComponent<Rigidbody>().velocity = Vector3.forward * force;
            laserSound.Play();
            Destroy(Laser, durationLaser);
        }
    }
    
}