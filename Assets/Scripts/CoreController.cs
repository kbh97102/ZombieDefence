using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CoreController : MonoBehaviour
{
    [SerializeField]private Slider slider;
    
    private int currentHP;
    private int maxHP;


    private void Start()
    {
        maxHP = 10;
        currentHP = maxHP;
    }

    private void Update()
    {
        UpdateSlider();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Attacked();
        }
    }

    private void Attacked()
    {
        currentHP -= 1;
        if (currentHP <= 0)
        {
            //TODO GameOver
        }
    }
    
    private void UpdateSlider()
    {
        slider.value = (float) currentHP / maxHP;
    }
}
