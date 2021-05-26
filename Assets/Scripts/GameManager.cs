using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public Text waveUI;

    public ZombieSpawner spawner;
    
    private int wave;

    private void Start()
    {
        wave = 0;
        UpdateWave();
    }

    public int GetWave()
    {
        return wave;
    }
    
    public void UpdateWave()
    {
        waveUI.text = wave.ToString();
    }

    public void StartGame()
    {
        spawner.StartGenerateZombie();
    }
}