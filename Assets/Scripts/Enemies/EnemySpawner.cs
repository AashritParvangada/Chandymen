using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject GO_enemyToSpawn;
    public bool B_activateOnStart = true, B_activateOnDialogue = false;

    public void SpawnEnemy()//Called from within Spawn Trigger.
    {
        GameObject _spawnedEnemy = Instantiate(GO_enemyToSpawn, transform);
        EnemyTypeActions(_spawnedEnemy);
    }

    void EnemyTypeActions(GameObject _go)
    {
        if (_go.GetComponent<PoliceBot>())
        {
            PoliceBot _pb = _go.GetComponent<PoliceBot>();
            _pb.B_AttackOnDialogue = B_activateOnDialogue; _pb.B_AttackOnStart = B_activateOnStart;
            Debug.Log("Setting Attack on Dialogue to " + B_activateOnDialogue);
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
    }
}
