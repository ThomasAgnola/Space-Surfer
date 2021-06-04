using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLaser : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {

        //foreach (ContactPoint contact in collision.contacts)
        //{
            Destroy(collision.gameObject, 0);
        //}

    }
}
