using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreHPSlider : MonoBehaviour
{
    public Slider slider;

    private float hp = 100;
    private float hpFull = 100;

    private void Start()
    {
        hp = 100;
        hpFull = 100;
    }

    private void Update()
    {
        test();
        slider.value = hp / hpFull;
    }

    private void test()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hp -= 1;
        }
    }
}
