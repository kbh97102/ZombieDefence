using System;
using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    #region Public Fields

    [Header("SpawnPoints")] [SerializeField]
    private GameObject point1;

    [SerializeField] private GameObject point2;
    [SerializeField] private GameObject point3;
    [SerializeField] private GameObject point4;

    [Header("Zombie prefabs")] [SerializeField]
    private GameObject zombieToPlayer;

    [SerializeField] private GameObject zombieToTower;

    [Header("Player")] [SerializeField] private GameObject target_player;

    [Header("Core")] [SerializeField] private GameObject target_core;

    public GameManager gameManager;

    #endregion

    private IEnumerator spawner;

    private void Start()
    {
        StartGenerateZombie();
    }

    public void StartGenerateZombie()
    {
        if (spawner == null)
        {
            spawner = GenerateZombie();
        }

        StartCoroutine(spawner);
    }
    
    private IEnumerator GenerateZombie()
    {
        var wave = gameManager.GetWave();
        if (wave == 0)
        {
            var zombie = Instantiate(zombieToTower, point1.transform.position, point1.transform.rotation);
            zombie.GetComponent<ZombieCharacterControl>().SetTarget(target_core);
        }
        else if (wave == 1)
        {
        }
        else if (wave == 2)
        {
        }
        else if (wave >= 3)
        {
        }

        yield return new WaitForSeconds(1f);
    }
}