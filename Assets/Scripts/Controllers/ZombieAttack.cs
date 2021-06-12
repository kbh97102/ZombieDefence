using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] private Animator m_animator = null;
    [SerializeField] private ZombieSoundController zombieSoundController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Core"))
        {
            m_animator.SetBool("Attack", true);
            zombieSoundController.PlaySound(ZombieSoundController.ZombieSounds.Attack);
            PhotonNetwork.Destroy(gameObject.transform.parent.gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Zombie attack");
            m_animator.SetBool("Attack", true);
            zombieSoundController.PlaySound(ZombieSoundController.ZombieSounds.Attack);
        }
    }

     private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Core"))
            {
                m_animator.SetBool("Attack", false);
            }
    
            if (other.gameObject.CompareTag("Player"))
            {
                m_animator.SetBool("Attack", false);
            }
        }
}
