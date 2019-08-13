using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceBot : MonoBehaviour
{
    [SerializeField] int I_health = 100;
    NavMeshAgent navMeshAg_agent;
    public bool B_AttackOnStart = false, B_AttackOnDialogue = false;
    EventManager evMan_eventManager;
    [SerializeField] float F_chargeCountdownTimer = 2;
    [SerializeField] float F_cooldownTimer = 2, F_stopDistance = 2;

    PlayerController playCont_Controller;

    private void OnEnable()
    {
        EventManager.OnDialogueComplete += DialogueEventEnded;
    }

    private void OnDisable()
    {
        EventManager.OnDialogueComplete -= DialogueEventEnded;
    }
    void Start()
    {
        GetVariables();
        CheckAttackOnStart();
    }

    void GetVariables()
    {
        playCont_Controller = FindObjectOfType<PlayerController>();
        navMeshAg_agent = GetComponent<NavMeshAgent>();
        evMan_eventManager = FindObjectOfType<EventManager>();
    }

    public void CheckAttackOnStart()
    {
        if (B_AttackOnStart)
        {
            StartAttacking();
        }
    }

    IEnumerator IEnum_ChargeTowardsPlayer(PlayerController _target)//Loops with Set Target
    {
        Debug.Log("entered charge state");
        while (Vector3.Distance(transform.position, _target.transform.position) > F_stopDistance)
        {
            Debug.Log(Vector3.Distance(transform.position, _target.transform.position));
            yield return new WaitForSeconds(0.2f);
            navMeshAg_agent.SetDestination(_target.transform.position);
        }

        navMeshAg_agent.SetDestination(SlightForwardMove());
        //navMeshAg_agent.SetDestination(transform.position);

        StartCoroutine(IEnum_CooldownState(F_cooldownTimer));
    }

    Vector3 SlightForwardMove()
    {
        return transform.position + transform.forward * F_stopDistance;
    }

    //Sets the bot to target the player and loops a coroutine that retargets the player's location every .3 seconds.
    IEnumerator IEnum_CooldownState(float _cooldownTime)
    {
        Debug.Log("entered cool state");
        StopCoroutine(IEnum_ChargeTowardsPlayer(playCont_Controller));
        yield return new WaitForSeconds(_cooldownTime);
        StartCoroutine(IEnum_ChargeCharge(F_chargeCountdownTimer));
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
    public void StartAttacking()
    {
        StopAllCoroutines();
        StartCoroutine(IEnum_ChargeCharge(F_chargeCountdownTimer));
    }
    IEnumerator IEnum_ChargeCharge(float _timer)
    {
        Debug.Log("Charging Charge");
        float _countingDownTime = _timer;

        while (_countingDownTime > 0)
        {
            transform.LookAt(playCont_Controller.transform);
            yield return new WaitForSeconds(0.1f);
            _countingDownTime -= 0.1f;
        }

        StartCoroutine(IEnum_ChargeTowardsPlayer(playCont_Controller));
    }

    void DialogueEventEnded()
    {
        if (B_AttackOnDialogue)
        {
            StartAttacking();
        }
    }
}
