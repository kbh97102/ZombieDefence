using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    #region Unity

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Object"))
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public void shooting(Vector3 vector3)
    {
        Destroy(gameObject, 30f);
        GetComponent<Rigidbody>().AddForce(vector3 * 1500);
    }
}