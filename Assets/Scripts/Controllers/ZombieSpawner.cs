using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    #region Public Fields

    [Header("SpawnPoints")] [SerializeField]
    private GameObject point1;

    [SerializeField] private GameObject point2;
    [SerializeField] private GameObject point3;
    [SerializeField] private GameObject point4;

    private GameObject target_player;

    [Header("Core")] [SerializeField] private GameObject target_core;

    [SerializeField] private GameManager gameManager;

    private PhotonView photonView;

    #endregion

    private IEnumerator spawner;
    private Dictionary<int, GameObject> pointMap;
    private ArrayList zombies;

    private Coroutine spawnWorker;

    private void Awake()
    {
        photonView = PhotonView.Get(this);
    }

    private void Start()
    {
        zombies = new ArrayList();
        pointMap = new Dictionary<int, GameObject>();
        pointMap.Add(0, point1);
        pointMap.Add(1, point2);
        pointMap.Add(2, point3);
        pointMap.Add(3, point4);
    }

    public void StartGenerateZombie()
    {
        spawner = GenerateZombie();
        spawnWorker = StartCoroutine(spawner);
    }

    private GameObject GetRandomPosition()
    {
        int randomPosition = (int) Random.Range(0f, 4f);
        return pointMap[randomPosition];
    }

    private void SpawnZombie()
    {
        var list = FindObjectsOfType<PlayerController>();

        int randomPlayerTarget = (int) Random.Range(0, list.Length);
        GameObject position = GetRandomPosition();
        int randomTarget = (int) Random.Range(0, 2f);
        GameObject zombie;
        if (randomTarget <= 0)
        {
            zombie = PhotonNetwork.Instantiate("Zombie", position.transform.position, position.transform.rotation);
            zombie.GetComponent<ZombieCharacterControl>().SetTarget(target_core);
        }
        else
        {
            zombie = PhotonNetwork.Instantiate("Zombie", position.transform.position, position.transform.rotation);
            zombie.GetComponent<ZombieCharacterControl>().SetTarget(list[randomPlayerTarget].gameObject);
        }

        zombie.GetComponent<ZombieCharacterControl>().SetGameManager(gameManager);
        zombies.Add(zombie);
    }

    private IEnumerator GenerateZombie()
    {
        var wave = gameManager.GetWave();
        var current = 0;
        var end = (wave + 1) * 3;
        while (current < end)
        {
            SpawnZombie();
            yield return new WaitForSeconds(3f);
            current++;
        }
    }

    public void ResetZombies()
    {
        // photonView.RPC("ClearZombies", RpcTarget.All);
        ClearZombies();
    }

    // [PunRPC]
    private void ClearZombies()
    {
        if (spawnWorker != null)
        {
            StopCoroutine(spawnWorker);
        }

        foreach (GameObject zombie in zombies)
        {
            if (zombie != null)
            {
                try
                {
                    zombie.GetComponent<ZombieCharacterControl>().StopPlaySound();
                    PhotonNetwork.Destroy(zombie);
                }
                catch (MissingReferenceException e)
                {
                    PhotonNetwork.Destroy(zombie);
                }
            }
        }

        zombies.Clear();
    }
}