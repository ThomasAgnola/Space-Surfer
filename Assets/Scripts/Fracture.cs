﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class Fracture : MonoBehaviour
{
    [Tooltip("\"Fractured\" is the object that this will break into")]
    public GameObject fractured;
    public AudioSource impactSound;

    private void OnCollisionEnter(Collision collision)
    {   if(collision.gameObject.CompareTag("laser"))
        {   
            GameObject asteroid=Instantiate(fractured, transform.position, transform.rotation); //Spawn in the broken version
            Destroy(gameObject); //Destroy the object to stop it getting in the way
            EventManager.Instance.Raise(new AsteroidExplosionEvent()); //Increment Score of the party
            Destroy(collision.gameObject); //Destroy the laser
            asteroid.AddComponent<SphereCollider>(); //add a spherecollider to trigger an explosion
            Destroy(asteroid.GetComponent<SphereCollider>(), 0.5f); //destroy the spherecollider just after
            impactSound.Play();
            Destroy(asteroid, 6); //destroy the asteroid after 6 sec
            

        }
        else if(collision.gameObject.CompareTag("spaceship"))
        {
            GameObject asteroid = Instantiate(fractured, transform.position, transform.rotation);
            Destroy(gameObject);
            asteroid.AddComponent<SphereCollider>();
            Destroy(asteroid.GetComponent<SphereCollider>(), 0.5f);
            impactSound.Play();
            Destroy(asteroid, 6);
            EventManager.Instance.Raise(new GameOverEvent());

        }
        

    }
}
