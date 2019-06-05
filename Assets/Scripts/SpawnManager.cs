using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    int i_EnemiesKilled = 0;
    [SerializeField] int I_MaxNumberOfEnemies, I_KilledThresehold_1, I_KilledThresehold_2, I_KilledThresehold_3;
    [SerializeField] Transform[] Trans_SpawnPoints;
    [SerializeField] GameObject GO_Grunt, GO_PoliceBot;
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

    void CountEnemies()
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

    void EnemyKilled()
    {
        i_EnemiesKilled++;
        Debug.Log(i_EnemiesKilled);
        CheckMax();
    }

    void SpawnEnemy(int _totalEnemies)
    {
        while (_totalEnemies < I_MaxNumberOfEnemies)
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
        if (i_EnemiesKilled >= I_KilledThresehold_1) I_MaxNumberOfEnemies = 2;
        if (i_EnemiesKilled >= I_KilledThresehold_2) I_MaxNumberOfEnemies++;
        if (i_EnemiesKilled >= I_KilledThresehold_3) I_MaxNumberOfEnemies++;
    }

}
