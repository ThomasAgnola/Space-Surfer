﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{
    [Tooltip("\"Fractured\" is the object that this will break into")]
    public GameObject fractured;

    private void OnCollisionEnter(Collision collision)
    {   if(collision.gameObject.CompareTag("laser"))
        {   
            Instantiate(fractured, transform.position, transform.rotation); //Spawn in the broken version
            Destroy(gameObject); //Destroy the object to stop it getting in the way
            Destroy(collision.gameObject);
        }
        

    }
}