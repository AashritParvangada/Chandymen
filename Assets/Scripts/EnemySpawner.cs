using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject GO_enemyToSpawn;

    public void SpawnEnemy()//Called from within Spawn Trigger.
    {
        Instantiate(GO_enemyToSpawn, transform);
    }
}
