using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;


public class BulletController : MonoBehaviour
{
    #region Unity

    private void OnCollisionEnter(Collision other)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            PhotonNetwork.Destroy(gameObject);
        }

        if (other.CompareTag("Object") || other.CompareTag("Core"))
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    #endregion

    public void shooting(Vector3 vector3)
    {
        StartCoroutine("DestroyBullet");
        GetComponent<Rigidbody>().AddForce(vector3 * 3000);
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(10f);
        PhotonNetwork.Destroy(gameObject);
    }
}