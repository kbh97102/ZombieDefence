using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("UI")] public WaveUI waveUI;
    public Button startButton;
    public GameObject resultPanel;

    public ZombieSpawner spawner;
    public HPUI hpUI;

    public Camera camera;
    public AmmoController ammo;

    public GameObject position1;
    public GameObject position2;


    private GameObject player;
    public CoreController core;

    private int wave;
    private bool isPlaying;
    private int zombieCount;
    private PlayerController playerController;

    private Dictionary<int, GameObject> positionMap;

    private WaveSound waveSound;

    private PhotonView photonView;

    private void Awake()
    {
        positionMap = new Dictionary<int, GameObject>();
        positionMap.Add(0, position1);
        positionMap.Add(1, position2);
        
        photonView = PhotonView.Get(this);
    }

    private void Start()
    {
        waveSound = new WaveSound(GetComponent<AudioSource>());

        isPlaying = false;
        wave = 0;
        UpdateWave();

        if (player == null)
        {
            object playerPosition;
            PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("position", out playerPosition);
            int index = (int) playerPosition;

            player = PhotonNetwork.Instantiate("unitychan", positionMap[index].transform.position,
                Quaternion.identity, 0);
            PhotonNetwork.LocalPlayer.TagObject = player;


            playerController = player.GetComponent<PlayerController>();
            playerController.mainCamera = camera;

            player.GetComponent<PlayerShootController>().mainCamera = camera;
            player.GetComponent<PlayerShootController>().ammoController = ammo;

            playerController.hpUI = hpUI;

            // photonView = player.GetPhotonView();

            camera.GetComponent<FollowCamera>().target = player.transform;
        }
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
            photonView.RPC("WaveEnd", RpcTarget.All);
        }
    }

    [PunRPC]
    private void WaveEnd()
    {
        waveSound.PlaySound("win");
        isPlaying = false;
        zombieCount = 0;
        startButton.gameObject.SetActive(true);
        wave++;
        UpdateWave();
    }

    private void CheckLose()
    {
        if (!isPlaying)
        {
            return;
        }

        if (core.GetHP() <= 0 || playerController.GetHP() <= 0)
        {
            Debug.Log("CheckLose you lose");
            photonView.RPC("PlayerLose", RpcTarget.All);
        }
        else
        {
            Debug.Log("Check lose");
        }
    }

    public void ReduceZombieCount()
    {
        zombieCount -= 1;
    }

    [PunRPC]
    public void ReSetGame()
    {
        playerController.ResetHP();
        core.ResetHP();
        zombieCount = 0;
        wave = 0;
        waveUI.ResetWaveUI();
        isPlaying = false;
        startButton.gameObject.SetActive(true);
        player.GetComponent<PlayerShootController>().ResetAmmo();
        spawner.ResetZombies();
        hpUI.UpdateHP(playerController.GetHP());
    }

    [PunRPC]
    private void PlayerLose()
    {
        waveSound.PlaySound("lose");
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