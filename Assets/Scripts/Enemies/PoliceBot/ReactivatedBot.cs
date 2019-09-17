using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReactivatedBot : MonoBehaviour
{
    [SerializeField] int I_totalHealth = 100;
    [SerializeField] GameObject GO_healthBarAnchor;
    int i_currentHealth;
    NavMeshAgent navMeshAg_agent;
    public bool B_AttackOnStart, B_ActivateOnDialogue;
    EventManager evMan_eventManager;
    PlayerController plCont_playa;

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

    private void Update()
    {
        transform.LookAt(plCont_playa.transform);
    }

    void GetVariables()
    {
        i_currentHealth = I_totalHealth;

        navMeshAg_agent = GetComponent<NavMeshAgent>();
        evMan_eventManager = FindObjectOfType<EventManager>();
        plCont_playa = FindObjectOfType<PlayerController>();
    }

    public void CheckAttackOnStart()
    {
        if (B_AttackOnStart)
        {
            SetTarget(plCont_playa);
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
        i_currentHealth -= _DecreaseBy;

        GO_healthBarAnchor.transform.localScale = new Vector3((float)i_currentHealth / I_totalHealth, 1, 1);

        if (i_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()//Event manager destroy event.
    {
        if (evMan_eventManager)
            evMan_eventManager.CountEnemyKilled();
    }
    void DialogueEventEnded()
    {
        if (B_ActivateOnDialogue)
        {
            SetTarget(plCont_playa);
        }
    }
}
