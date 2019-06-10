using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    bool b_isActive = false;
    int i_numberOfSpawnersWithin = 0;
    int i_enemiesKilled = 0;
    [SerializeField] GameObject GO_DoorToOpen;

    private void Start()
    {
        CountNumberOfSpawners();
    }
    private void OnEnable()
    {
        EventManager.OnEnemyKilled += KilledEnemy;

    }

    private void OnDisable()
    {
        EventManager.OnEnemyKilled -= KilledEnemy;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() && !b_isActive)
        {
            SpawnEnemies();
            SetActive();
            GetComponent<BoxCollider>().enabled = false;

        }
    }

    void SpawnEnemies()
    {
        foreach (EnemySpawner _EnSpawn in GetComponentsInChildren<EnemySpawner>())
        {
            _EnSpawn.SpawnEnemy();
        }
    }

    void SetActive()
    {
        b_isActive = true;
    }

    void CountNumberOfSpawners()
    {
        foreach (EnemySpawner _EnSpawn in GetComponentsInChildren<EnemySpawner>())
        {
            i_numberOfSpawnersWithin++;
        }
    }

    void KilledEnemy()
    {
        if (b_isActive)
        {
            i_enemiesKilled++;
            if (i_enemiesKilled >= i_numberOfSpawnersWithin)
            {
                OpenDoor();
            }
        }
    }

    void OpenDoor()
    {
        GO_DoorToOpen.SetActive(false);
    }


}
