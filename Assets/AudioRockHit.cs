using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class AudioRockHit : MonoBehaviour
{
    public AudioSource impactSound;

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<AsteroidDestroyedEvent>(AsteroidDestroyed);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.AddListener<AsteroidDestroyedEvent>(AsteroidDestroyed);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    void AsteroidDestroyed(AsteroidDestroyedEvent e)
    {
        impactSound.Play();
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("laser"))
        {impactSound.Play();
         

        }
        
    }*/
}
