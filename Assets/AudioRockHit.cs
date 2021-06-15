using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRockHit : MonoBehaviour
{
    public AudioSource impactSound;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("laser"))
        {impactSound.Play();
         

        }
        
    }
}
