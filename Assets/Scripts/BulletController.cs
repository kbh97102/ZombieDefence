using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BulletController : MonoBehaviour
{

    #region Unity
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            
        }
    }

    #endregion

    public void shooting(Vector3 vector3)
    {
        GetComponent<Rigidbody>().AddForce(vector3*1000);
    }
    
}
