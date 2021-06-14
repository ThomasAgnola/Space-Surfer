using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{
    public GameObject Projectil;
    public int force = 30;
    public float durationLaser = 5;
    public AudioSource laserSound;
    private float m_NextShootTime;
    [SerializeField] float m_LaserCoolDownDuration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsPlaying) return;
        else if (Input.GetKeyDown(KeyCode.Space) || (Input.GetAxisRaw("Fire1")>0) )
        {
            if (Time.time > m_NextShootTime)
            {
                ShootLaser();
                m_NextShootTime = Time.time + m_LaserCoolDownDuration;
            }
            
        }
    }
    void ShootLaser()
    {
        GameObject Laser = Instantiate(Projectil, transform.position, transform.rotation);
        Laser.GetComponent<Rigidbody>().velocity = Vector3.forward * force;
        laserSound.Play();
        Destroy(Laser, durationLaser);
    }
}