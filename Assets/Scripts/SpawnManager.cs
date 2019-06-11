using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    int i_EnemiesKilled = 0;
    [SerializeField] bool b_isActive = false;

    [SerializeField] int I_MaxSimultanEnemies, I_KilledThresehold_1, I_KilledThresehold_2, I_KilledThresehold_3, I_TotalEnemiesToKill;
    [SerializeField] Transform[] Trans_SpawnPoints;
    [SerializeField] GameObject GO_Grunt, GO_PoliceBot, GO_DoorToCloseOnStart;
    [SerializeField] GameObject[] GO_DoorsToOpenWhenDone;
    private void OnEnable()
    {
        EventManager.OnEnemyKilled += CountEnemies;
        EventManager.OnEnemyKilled += EnemyKilled;

    }

    private void OnDisable()
    {
        EventManager.OnEnemyKilled -= CountEnemies;
        EventManager.OnEnemyKilled -= EnemyKilled;

    }

    void SetActive()
    {
        b_isActive = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() && !b_isActive)
        {
            SetActive();
            RandomlyInstantiate();
            GetComponent<BoxCollider>().enabled = false;
            CloseDoor();
        }
    }

    void CountEnemies()
    {
        if (b_isActive)
        {
            int _totalEnemies = 0;
            foreach (Grunt _grunt in FindObjectsOfType<Grunt>())
            {
                _totalEnemies++;
            }

            foreach (PoliceBot _PB in FindObjectsOfType<PoliceBot>())
            {
                _totalEnemies++;
            }

            SpawnEnemy(_totalEnemies);
        }
    }

    void EnemyKilled()
    {
        if (b_isActive)
        {
            i_EnemiesKilled++;
            CheckTotal();
            CheckMax();
        }
    }

    void CheckTotal()
    {
        if (i_EnemiesKilled >= I_TotalEnemiesToKill)
        {
            OpenDoors();
            gameObject.SetActive(false);
        }
    }

    void OpenDoors()
    {
        foreach (GameObject _Door in GO_DoorsToOpenWhenDone)
        {
            _Door.SetActive(false);
        }
    }

    void CloseDoor()
    {
        GO_DoorToCloseOnStart.SetActive(true);
    }

    void SpawnEnemy(int _totalEnemies)
    {
        while (_totalEnemies < I_MaxSimultanEnemies)
        {
            Debug.Log("Instantiating");
            RandomlyInstantiate();
            _totalEnemies++;
        }

    }

    void RandomlyInstantiate()
    {
        int _rand = Random.Range(0, 2);
        if (_rand == 0) Instantiate(GO_PoliceBot, Trans_SpawnPoints[Random.Range(0, Trans_SpawnPoints.Length)]).GetComponent<PoliceBot>();
        else
        {
            Instantiate(GO_Grunt, Trans_SpawnPoints[Random.Range(0, Trans_SpawnPoints.Length)]).GetComponent<PoliceBot>();
        }
    }

    void CheckMax()
    {
        if (i_EnemiesKilled >= I_KilledThresehold_1) I_MaxSimultanEnemies = 2;
        if (i_EnemiesKilled >= I_KilledThresehold_2) I_MaxSimultanEnemies++;
        if (i_EnemiesKilled >= I_KilledThresehold_3) I_MaxSimultanEnemies++;
    }

}
