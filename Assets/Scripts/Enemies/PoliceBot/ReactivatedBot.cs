using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReactivatedBot : MonoBehaviour
{
    [SerializeField] int I_health = 100;
    NavMeshAgent navMeshAg_agent;
    [SerializeField] bool B_attackOnStart = false;
    EventManager evMan_eventManager;

    private void Awake() {
        GetVariables();
    }
    void Start()
    {
        CheckAttackOnStart();
    }

    void GetVariables()
    {
        navMeshAg_agent = GetComponent<NavMeshAgent>();
        evMan_eventManager = FindObjectOfType<EventManager>();
    }

    void CheckAttackOnStart()
    {
        if (B_attackOnStart)
        {
            PlayerController _playa = FindObjectOfType<PlayerController>();
            SetTarget(_playa);
        }
    }

    IEnumerator RetargetPlayer(PlayerController _target)//Loops with Set Target
    {
        yield return new WaitForSeconds(.3f);
        SetTarget(_target);
    }

    //Sets the bot to target the player and loops a coroutine that retargets the player's location every .3 seconds.
    public void SetTarget(PlayerController _target)
    {
        //Debug.Log(_target.name);
        navMeshAg_agent.SetDestination(_target.transform.position);
        StartCoroutine(RetargetPlayer(_target));
    }

    //Decrease the bot's health when shot. Currently is a 1HKO.
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlasmaBullet>())
        {
            DecreaseHealth(100);
        }
    }

    void DecreaseHealth(int _DecreaseBy)//To damage this Police Bot's health.
    {
        I_health -= _DecreaseBy;
        if (I_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()//Event manager destroy event.
    {
        if (evMan_eventManager)
            evMan_eventManager.CountEnemyKilled();
    }
}
