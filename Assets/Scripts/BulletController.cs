using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    #region Unity

    private void Start()
    {
        shooting();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            
        }
    }

    #endregion

    private void shooting()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.forward);
    }
    
}
