using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    #region Fields

    [Header("Number")] 
    [SerializeField] private Sprite zero;
    [SerializeField] private Sprite one;
    [SerializeField] private Sprite two;
    [SerializeField] private Sprite three;
    [SerializeField] private Sprite four;
    [SerializeField] private Sprite five;
    [SerializeField] private Sprite six;
    [SerializeField] private Sprite seven;
    [SerializeField] private Sprite eight;
    [SerializeField] private Sprite nine;


    [SerializeField] private Image number1;
    [SerializeField] private Image number2;

    #endregion

    private Dictionary<int, Sprite> numberMap;
    
    private void Awake()
    {
        numberMap = new Dictionary<int, Sprite>();
        numberMap.Add(0, zero);
        numberMap.Add(1, one);
        numberMap.Add(2, two);
        numberMap.Add(3, three);
        numberMap.Add(4, four);
        numberMap.Add(5, five);
        numberMap.Add(6, six);
        numberMap.Add(7, seven);
        numberMap.Add(8, eight);
        numberMap.Add(9, nine);
    }

    private void Start()
    {
       ResetWaveUI();
    }

    public void NextWaveUI(int wave)
    {
        if (wave < 10)
        {
            number1.sprite = zero;
            number2.sprite = numberMap[wave];
        }
        else
        {
            var numberOne = wave / 10;
            var numberTwo = wave % 10;
            number1.sprite = numberMap[numberOne];
            number2.sprite = numberMap[numberTwo];
        }
        
    }
    

    public void ResetWaveUI()
    {
        number1.sprite = zero;
        number2.sprite = zero;
    }
}
