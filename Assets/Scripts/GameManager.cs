using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  

    private int wave;

    private void Start()
    {
        wave = 0;
    }

    public int GetWave()
    {
        return wave;
    }
}