using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public Text waveUI;
    public Button startButton;
    
    public ZombieSpawner spawner;

    public PlayerController player;
    public CoreController core;    
    
    private int wave;
    private bool isPlaying;
    private int zombieCount;
    
    private void Start()
    {
        isPlaying = false;
        wave = 0;
        UpdateWave();
    }

    private void Update()
    {
        CheckWaveEnd(); 
        CheckLose();
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
        zombieCount = (wave + 1) * 3;
        spawner.StartGenerateZombie();
        isPlaying = true;
        startButton.gameObject.SetActive(false);
    }

    private void CheckWaveEnd()
    {
        if (!isPlaying)
        {
            return;
        }
        if (zombieCount <= 0)
        {
            isPlaying = false;
            zombieCount = 0;
            startButton.gameObject.SetActive(true);
            wave++;
            UpdateWave();
        }
    }

    private void CheckLose()
    {
        if (!isPlaying)
        {
            return;
        }

        if (core.GetHP() <= 0 || player.GetHP() <= 0)
        {
            PlayerLose();
        }
    }

    public void ReduceZombieCount()
    {
        zombieCount -= 1;
    }
    
    private void PlayerLose()
    {
        //TODO 졌을 때 화면   
    }
}