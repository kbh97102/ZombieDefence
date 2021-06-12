using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CoreController : MonoBehaviour
{
    [SerializeField] private Text coreHP;
    
    private int currentHP;
    private int maxHP;

    private PhotonView photonView;

    private GameManager gameManager;
    private void Awake()
    {
        photonView = PhotonView.Get(this);
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        maxHP = 10;
        currentHP = maxHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            photonView.RPC("CoreAttacked", RpcTarget.All);
            gameManager.ReduceZombieCount();
        }
    }

    [PunRPC]
    private void CoreAttacked()
    {
        currentHP -= 1;
        UpdateSlider();
    }
    
    private void UpdateSlider()
    {
        coreHP.text = currentHP.ToString();
    }

    public void ResetHP()
    {
        currentHP = maxHP;
        UpdateSlider();
    }
    
    public int GetHP()
    {
        return currentHP;
    }
}
