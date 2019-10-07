using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    bool b_isActive = false;
    int i_numberOfSpawnersWithin = 0;
    int i_enemiesKilled = 0;
    [SerializeField] ElectricDoor ElecDoor_doorToOpen, ElecDoor_doorToClose;
    [SerializeField] GameObject GO_spwnManToActivate;
    [SerializeField] SpawnManager SpwnMan_timboiSpawnMan;
    public SceneChanger ScnChngr;
    [SerializeField] bool B_isLastTrigger;

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
            SwitchDoor(true, ElecDoor_doorToClose);
        }
    }

    void SpawnEnemies()//Spawn an enemy in each spawner.
    {
        foreach (EnemySpawner _EnSpawn in GetComponentsInChildren<EnemySpawner>())
        {
            _EnSpawn.SpawnEnemy();
        }
        if (GetComponentInChildren<Timboi>())
        {
            GetComponentInChildren<Timboi>().SpwnMan_OnTimboiHealth = SpwnMan_timboiSpawnMan;
        }
    }

    void SetActive()
    {
        b_isActive = true;
    }

    void CountNumberOfSpawners()//Used to know when to spawn more enemies and how many more to spawn.
    {
        foreach (EnemySpawner _EnSpawn in GetComponentsInChildren<EnemySpawner>())
        {
            i_numberOfSpawnersWithin++;
        }
    }

    void KilledEnemy()//Called on event.
    {
        if (b_isActive)
        {
            i_enemiesKilled++;//Add another enemy killed. If all enemies have been killed, open the next door.
            if (i_enemiesKilled >= i_numberOfSpawnersWithin)
            {
                EndTriggerAction();

                if (B_isLastTrigger) FindObjectOfType<EventManager>().LastEnemyKilledEvent();
            }
        }
    }

    void EndTriggerAction()
    {
        SwitchDoor(false, ElecDoor_doorToOpen);
        if (GO_spwnManToActivate) GO_spwnManToActivate.SetActive(true);
    }

    void SwitchDoor(bool _toActivate, ElectricDoor _elecDoor)
    {
        if (_elecDoor != null)
            _elecDoor.SwitchDoor(_toActivate);
    }

}
