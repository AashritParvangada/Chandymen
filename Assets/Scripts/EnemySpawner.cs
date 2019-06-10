using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject GO_EnemyToSpawn;

    public void SpawnEnemy()
    {
        Instantiate(GO_EnemyToSpawn, transform);
    }
}
