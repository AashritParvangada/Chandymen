using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CleanerHome : MonoBehaviour
{
    [SerializeField] GameObject GO_CleanerBot;
    [SerializeField] float F_SpawnTime = 3;
    [SerializeField] Transform[] Trans_Arr_NavPoints;
    [SerializeField] float F_SetBotMovementTimeInvetvalTo, F_BotSpeed = 5;

    private void Start()
    {
        StartSpawningBots();
    }

    IEnumerator RepeatSpawnCleanerBots()
    {
        yield return new WaitForSeconds(F_SpawnTime);
        SpawnCleanerBot();
        StartCoroutine(RepeatSpawnCleanerBots());
    }

    void SpawnCleanerBot()
    {
        CleanerBot CleBot_Script = Instantiate(GO_CleanerBot, transform).GetComponent<CleanerBot>();
        CleBot_Script.Trans_Arr_PatrolPathPoints = Trans_Arr_NavPoints;
        CleBot_Script.F_ResetTimeInterval=F_SetBotMovementTimeInvetvalTo; CleBot_Script.F_TimeInterval=F_SetBotMovementTimeInvetvalTo;
        CleBot_Script.GetComponent<NavMeshAgent>().speed = F_BotSpeed;
       // CleBot_Script.CheckArraySizeToPatrol();
    }

    void StartSpawningBots()
    {
        StartCoroutine(RepeatSpawnCleanerBots());
    }
}
