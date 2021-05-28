using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")] public WaveUI waveUI;
    public Button startButton;
    public GameObject resultPanel;

    public ZombieSpawner spawner;

    public GameObject player;
    public CoreController core;

    private int wave;
    private bool isPlaying;
    private int zombieCount;
    private PlayerController playerController;


    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
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
        waveUI.NextWaveUI(wave);
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

        if (core.GetHP() <= 0 || playerController.GetHP() <= 0)
        {
            PlayerLose();
        }
    }

    public void ReduceZombieCount()
    {
        zombieCount -= 1;
    }

    public void ReSetGame()
    {
        playerController.ResetHP();
        core.ResetHP();
        player.GetComponent<PlayerShootController>().ResetAmmo();
        zombieCount = 0;
        wave = 0;
        waveUI.ResetWaveUI();
        isPlaying = false;
        startButton.gameObject.SetActive(true);
        player.GetComponent<PlayerShootController>().ResetAmmo();
        spawner.ResetZombies();
    }

    private void PlayerLose()
    {
        //TODO 졌을 때 화면

        if (wave <= 0)
        {
            wave = 0;
        }
        else
        {
            wave -= 1;
        }

            resultPanel.GetComponent<ResultUI>().SetResult(
            player.GetComponent<PlayerShootController>().GetAmmo().ToString(),
            core.GetHP().ToString(),
            playerController.GetHP().ToString(),
            wave.ToString(),
            zombieCount.ToString()
        );
        ReSetGame();
        resultPanel.gameObject.SetActive(true);
    }
}