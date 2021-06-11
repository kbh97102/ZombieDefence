using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CoreController : MonoBehaviour
{
    [SerializeField] private Text coreHP;
    
    private int currentHP;
    private int maxHP;


    private void Start()
    {
        maxHP = 10;
        currentHP = maxHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Attacked();
        }
    }

    private void Attacked()
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
