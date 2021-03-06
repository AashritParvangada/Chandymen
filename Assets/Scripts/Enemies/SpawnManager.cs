﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    int i_enemiesKilled = 0, i_enemiesSpawned = 0;
    [SerializeField] bool B_isActive = false, B_activateEnemy = true;

    [SerializeField] int I_maxSimultanEnemies, I_killedThresehold_1, I_killedThresehold_2, I_killedThresehold_3, I_totalEnemiesToKill;
    [SerializeField] Transform[] Trans_Arr_spawnPoints;
    [SerializeField] GameObject GO_grunt, GO_policeBot, GO_spawnFX;
    [SerializeField] ElectricDoor[] ElecDoor_Arr_doorsToOpenWhenDone, ElecDoor_Arr_doorsToCloseOnStart;
    [SerializeField] int I_chanceOfGrunt = 50, I_chanceOfBot = 50;
    [SerializeField] bool B_triggerLastEnemyKilledEvent;
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
        B_isActive = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() && !B_isActive)
        {
            SetActive();
            RandomlyInstantiate();
            GetComponent<BoxCollider>().enabled = false;
            CloseDoor();
        }
    }

    public void Activate()
    {
        if (!B_isActive)
        {
            SetActive();
            RandomlyInstantiate();
            GetComponent<BoxCollider>().enabled = false;
            CloseDoor();
        }
    }

    void CountEnemies()//Called on enemy killed event.
    {
        if (B_isActive)//If this has been activated by OnTriggerEnter.
        {
            int _totalEnemies = 0;
            foreach (Grunt _grunt in GetComponentsInChildren<Grunt>())
            {
                _totalEnemies++;
            }

            foreach (PoliceBot _PB in GetComponentsInChildren<PoliceBot>())
            {
                _totalEnemies++;
            }

            SpawnEnemy(_totalEnemies);//Compares total enemis around with the amount that should be around now.
        }
    }

    void EnemyKilled()
    {
        if (B_isActive)
        {
            i_enemiesKilled++;
            CheckTotal();//Sets this to false if player has killed all required enemies.
            CheckIncreaseDifficulty();//Checks to increase maximum simultaneous enemies around at once. Happens at the various threseholds.
        }
    }

    void CheckTotal()//Is the player objective to reach total.
    {
        if (i_enemiesKilled >= I_totalEnemiesToKill)
        {
            if (B_triggerLastEnemyKilledEvent) FindObjectOfType<EventManager>().LastEnemyKilledEvent();
            OpenDoors();
            gameObject.SetActive(false);
        }
    }

    void OpenDoors()//Open all doors within this array
    {
        foreach (ElectricDoor _Door in ElecDoor_Arr_doorsToOpenWhenDone)
        {
            _Door.SwitchDoor(false);
        }
    }

    void CloseDoor()//Close exits when player enters volume.
    {
        foreach (ElectricDoor _Door in ElecDoor_Arr_doorsToCloseOnStart)
        {
            _Door.SwitchDoor(true);
        }
    }

    void SpawnEnemy(int _totalEnemies)//Spawn enemies until total enemies = max simultaneous, but not if there's only a few enemies left until goal.
    {

        while (_totalEnemies < I_maxSimultanEnemies && i_enemiesSpawned < I_totalEnemiesToKill)
        {
            RandomlyInstantiate();
            _totalEnemies++;
        }



    }

    void RandomlyInstantiate()//Randomly instantiate a grunt or police bot depending on two numbers. 
    {
        int _rand = Random.Range(1, I_chanceOfBot + I_chanceOfGrunt);
        if (_rand <= I_chanceOfBot)
        {
            Transform _trans = Trans_Arr_spawnPoints[Random.Range(0, Trans_Arr_spawnPoints.Length)];
            Instantiate(GO_spawnFX, _trans);
            PoliceBot _pb = Instantiate(GO_policeBot, _trans).GetComponent<PoliceBot>();
            if (B_activateEnemy) _pb.StartAttacking();
        }
        else
        {
            Transform _trans = Trans_Arr_spawnPoints[Random.Range(0, Trans_Arr_spawnPoints.Length)];
            Instantiate(GO_spawnFX, _trans);
            Grunt _grnt = Instantiate(GO_grunt, _trans).GetComponent<Grunt>();
            if (B_activateEnemy) _grnt.StartCombat();
        }

        i_enemiesSpawned++;
    }

    void CheckIncreaseDifficulty()//See if player has crossed the thresehold of enemies to kill before increasing the difficulty.
    {
        if (i_enemiesKilled >= I_killedThresehold_1) I_maxSimultanEnemies = 2;
        if (i_enemiesKilled >= I_killedThresehold_2) I_maxSimultanEnemies++;
        if (i_enemiesKilled >= I_killedThresehold_3) I_maxSimultanEnemies++;
    }

    public void IncreaseMaxSimultaneousEnemies(int _increaseBy)
    {
        I_maxSimultanEnemies++;
    }

}
