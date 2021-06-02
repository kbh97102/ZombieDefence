using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    
    public Text hpText;
    
    
    public void UpdateHP(int hp)
    {
        hpText.text = hp.ToString();
    }
}
