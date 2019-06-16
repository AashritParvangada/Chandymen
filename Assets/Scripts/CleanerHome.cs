using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CleanerHome : MonoBehaviour
{
    [SerializeField] GameObject GO_cleanerBot;
    [SerializeField] float F_spawnTime = 3;
    [SerializeField] Transform[] Trans_Arr_navPoints;
    [SerializeField] float F_setBotMovementTimeInvetvalTo, F_botSpeed = 5;

    private void Start()
    {
        StartSpawningBots();
    }

    IEnumerator RepeatSpawnCleanerBots()
    {
        yield return new WaitForSeconds(F_spawnTime);
        SpawnCleanerBot();
        StartCoroutine(RepeatSpawnCleanerBots());
    }

    void SpawnCleanerBot()
    {
        CleanerBot CleBot_Script = Instantiate(GO_cleanerBot, transform).GetComponent<CleanerBot>();
        CleBot_Script.Trans_Arr_PatrolPathPoints = Trans_Arr_navPoints;
        CleBot_Script.F_ResetTimeInterval=F_setBotMovementTimeInvetvalTo; CleBot_Script.F_TimeInterval=F_setBotMovementTimeInvetvalTo;
        CleBot_Script.GetComponent<NavMeshAgent>().speed = F_botSpeed;
       // CleBot_Script.CheckArraySizeToPatrol();
    }

    void StartSpawningBots()
    {
        StartCoroutine(RepeatSpawnCleanerBots());
    }
}
