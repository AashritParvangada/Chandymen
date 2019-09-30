using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceBot : MonoBehaviour
{
    [SerializeField] int I_totalHealth = 100;

    int i_currentHealth;
    NavMeshAgent navMeshAg_agent;
    public bool B_AttackOnStart = false, B_AttackOnDialogue = false;
    EventManager evMan_eventManager;
    [SerializeField] float F_chargeCountdownTimer = 2;
    [SerializeField] float F_cooldownTimer = 2, F_stopDistance = 2;
    [SerializeField] GameObject GO_healthBarAnchor, GO_chargeParticle, GO_dashParticle, GO_deathParticle;
    Animator animator;
    PlayerController playCont_Controller;

    private void OnEnable()
    {
        EventManager.OnDialogueComplete += DialogueEventEnded;
    }

    private void OnDisable()
    {
        EventManager.OnDialogueComplete -= DialogueEventEnded;
    }

    private void Awake()
    {
        GetVariables();
    }

    void Start()
    {
        CheckAttackOnStart();
    }

    void GetVariables()
    {
        i_currentHealth = I_totalHealth;
        playCont_Controller = FindObjectOfType<PlayerController>();
        navMeshAg_agent = GetComponent<NavMeshAgent>();
        evMan_eventManager = FindObjectOfType<EventManager>();
        animator = GetComponentInChildren<Animator>();
    }

    public void CheckAttackOnStart()
    {
        if (B_AttackOnStart)
        {
            StartAttacking();
        }
    }

    public void AnimTrigger(string _trigger)
    {
        animator.SetTrigger(_trigger);
    }

    IEnumerator IEnum_ChargeTowardsPlayer(PlayerController _target)//Loops with Set Target
    {
        AnimTrigger("Dash");

        StopAllParticles();
        foreach (ParticleSystem _prtcl in GO_dashParticle.GetComponentsInChildren<ParticleSystem>()) _prtcl.Play();

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
        AnimTrigger("Look");
        StopCoroutine(IEnum_ChargeTowardsPlayer(playCont_Controller));
        StopAllParticles();
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
        i_currentHealth -= _DecreaseBy;
        GO_healthBarAnchor.transform.localScale = new Vector3((float)i_currentHealth / I_totalHealth, 1, 1);

        if (i_currentHealth <= 0)
        {
            AnimTrigger("Die");
            Destroy(gameObject, 0.2f);
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
        AnimTrigger("Charge");

        float _countingDownTime = _timer;
        foreach (ParticleSystem _prtcl in GO_chargeParticle.GetComponentsInChildren<ParticleSystem>()) _prtcl.Play();

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

    void StopAllParticles()
    {
        foreach (ParticleSystem _prtcl in GetComponentsInChildren<ParticleSystem>()) _prtcl.Stop();
    }
}
