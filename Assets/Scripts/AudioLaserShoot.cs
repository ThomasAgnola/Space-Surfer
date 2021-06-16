using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLaserShoot : MonoBehaviour
{
    public AudioSource impactSound;


    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            impactSound.Play();
        }
    }
}
