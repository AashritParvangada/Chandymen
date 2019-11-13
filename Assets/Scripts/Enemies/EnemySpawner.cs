using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject GO_enemyToSpawn, GO_spawnFX;
    public bool B_activateOnStart = true, B_activateOnDialogue = false;

    public void SpawnEnemy()//Called from within Spawn Trigger.
    {
        SpawnParticles();
        GameObject _spawnedEnemy = Instantiate(GO_enemyToSpawn, transform);
        EnemyTypeActions(_spawnedEnemy);
    }

    void SpawnParticles()
    {
        GameObject _spawnFX = Instantiate(GO_spawnFX, transform);
    }

    void EnemyTypeActions(GameObject _go)
    {
        if (_go.GetComponent<PoliceBot>())
        {
            PoliceBot _pb = _go.GetComponent<PoliceBot>();
            _pb.B_AttackOnDialogue = B_activateOnDialogue; _pb.B_AttackOnStart = B_activateOnStart;
            _pb.CheckAttackOnStart();
        }

        if (_go.GetComponent<ReactivatedBot>())
        {
            ReactivatedBot _rab = _go.GetComponent<ReactivatedBot>();
            _rab.B_ActivateOnDialogue = B_activateOnDialogue; _rab.B_AttackOnStart = B_activateOnStart;
            _rab.CheckAttackOnStart();
        }

        if (_go.GetComponent<Grunt>())
        {
            Grunt _grnt = _go.GetComponent<Grunt>();
            _grnt.B_ShootOnDialogue = B_activateOnDialogue; _grnt.B_ShootOnStart = B_activateOnStart;
            _grnt.CheckAttackOnStart();
        }

        if (_go.GetComponent<Timboi>())
        {
            Timboi _timboi = _go.GetComponent<Timboi>();
            _timboi.SpwnMan_OnTimboiHealth = GetComponentInParent<SpawnTrigger>().SpwnMan_timboiSpawnMan;
            _timboi.transform.SetParent(null);
            _timboi.B_AttackOnDialogue = B_activateOnDialogue;
            _timboi.CheckAttackOnStart();
        }
    }
}
