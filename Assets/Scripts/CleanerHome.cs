using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerHome : MonoBehaviour
{
    [SerializeField] bool B_TrueDestroyerFalseSpawner = true;
    [SerializeField] GameObject GO_CleanerBot;
    [SerializeField] float F_SpawnTime = 3;
    [SerializeField] Transform[] Trans_Arr_NavPoints;
    [SerializeField] float F_TimeInterval;

    private void Start()
    {
        StartSpawningBots();
    }
    private void OnTriggerEnter(Collider other)//If this is a destroyer and the cleaner bot enters its radius.
    {
        CheckToDestroyCleanerBot(other);
    }

    void CheckToDestroyCleanerBot(Collider other)
    {
        if (other.GetComponent<CleanerBot>())
        {
            if (B_TrueDestroyerFalseSpawner)
                Destroy(other.gameObject);
        }
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
       // CleBot_Script.CheckArraySizeToPatrol();
    }

    void StartSpawningBots()
    {
        if(!B_TrueDestroyerFalseSpawner)
        StartCoroutine(RepeatSpawnCleanerBots());
    }
}
