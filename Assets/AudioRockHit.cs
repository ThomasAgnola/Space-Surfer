using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRockHit : MonoBehaviour
{
    public AudioSource impactSound;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.name=="SingleLine-LightSaber")
        {impactSound.Play();
         Destroy(GetComponent<SphereCollider>(), 0);

        }
        
    }
}
