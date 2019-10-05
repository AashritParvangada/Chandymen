using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Timboi : MonoBehaviour
{
    List<Zone> zon_List_zones = new List<Zone>();
    PlayerController playcont_player;
    EventManager evMan_eventManager;
    Animator anim_Controller;
    Rigidbody rb_RB;
    AudioSource audSrc_thisSource;
    NavMeshAgent navMesAg_agent;
    Animator animator;
    [SerializeField] int I_health = 100, I_damage = 30;
    [SerializeField] float F_minAttackTime, F_maxAttackTime, F_minMovementCheckTime, F_maxMovementCheckTime, F_recoverTime;
    bool b_lookAtPlayer = true;
    public bool B_AttackOnDialogue = true;
    [SerializeField] GameObject[] GO_tempArray;
    public SpawnManager SpwnMan_OnTimboiHealth;[SerializeField] float F_thresehold1 = 350, F_thresehold2 = 200;
    private void Start()//Get variables.
    {
        GetVariables();
    }


    private void OnEnable()
    {
        EventManager.OnDialogueComplete += DialogueEventEnded;

    }

    private void OnDisable()
    {
        EventManager.OnDialogueComplete -= DialogueEventEnded;

    }

    private void Update()
    {
        if (b_lookAtPlayer) transform.LookAt(playcont_player.transform);
    }

    public void CheckAttackOnStart()
    {
        if (!B_AttackOnDialogue)
        {
            StartCombat();
        }
    }

    void DialogueEventEnded()
    {
        if (B_AttackOnDialogue)
        {
            StartCombat();
        }
    }

    void StartCombat()
    {
        StartCoroutine(Enum_MoveToZonePoint());//Start the movement. Will delay this later during the cutscene.
    }

    void GetVariables()//Get event manager, nav msh agent, player cont, gun, and make zone list.
    {
        evMan_eventManager = FindObjectOfType<EventManager>();
        navMesAg_agent = GetComponent<NavMeshAgent>();
        playcont_player = FindObjectOfType<PlayerController>();
        anim_Controller = GetComponentInChildren<Animator>();
        rb_RB = GetComponent<Rigidbody>();
        audSrc_thisSource = GetComponent<AudioSource>();
        GetZones();
        animator = GetComponentInChildren<Animator>();
    }

    void SetAnimTrigger(string _trigger)
    {
        animator.SetTrigger(_trigger);
    }

    void MoveToPointInPlayerZone()//Moves to a random point among the zone's points.
    {
        int pointToMoveTo = Random.Range(0, CheckPlayerZone().Trans_List_NavDestinations.Count);
        SetNavDestination(CheckPlayerZone().Trans_List_NavDestinations[pointToMoveTo].transform.position);
        SetAnimTrigger("Walk");
    }


    IEnumerator Enum_MoveToZonePoint() //Waits, then moves to a point within the zone.
    {
        MoveToPointInPlayerZone();
        yield return new WaitForSeconds(CheckMovementTime());

        StartCoroutine(Enum_MoveToPlayer());
    }

    IEnumerator Enum_MoveToPlayer()
    {

        while (CheckDistanceFromPlayer() > 2)
        {
            MoveToPlayer();
            yield return null;
        }

        SlashPlayer(CheckAttackTime());


    }

    float CheckDistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, playcont_player.transform.position);
    }

    void MoveToPlayer()
    {
        navMesAg_agent.speed = 10;
        navMesAg_agent.acceleration = 20;
        navMesAg_agent.SetDestination(playcont_player.transform.position);

    }

    void SlashPlayer(float _attackDelay)
    {
        StopAllCoroutines();
        navMesAg_agent.SetDestination(transform.position);
        StartCoroutine(Enum_Slash(_attackDelay));
    }

    public void SlashBullet()
    {
        Debug.Log("Slashing Bullet");
        StopAllCoroutines();
        navMesAg_agent.SetDestination(transform.position);
        StartCoroutine(Enum_Slash(0));
    }

    IEnumerator Enum_Slash(float _attackDelay)
    {
        b_lookAtPlayer = false;
        navMesAg_agent.isStopped = true;
        SetAnimTrigger("Charge");

        yield return new WaitForSeconds(_attackDelay);

        SetAnimTrigger("Attack");

        foreach (GameObject _go in GO_tempArray)
        {
            _go.SetActive(true);
        }

        yield return new WaitForSeconds(F_recoverTime);

        Recover();
        StartCoroutine(Enum_MoveToZonePoint());
    }

    void Recover()
    {
        b_lookAtPlayer = true;
        navMesAg_agent.isStopped = false;

        foreach (GameObject _go in GO_tempArray)
        {
            _go.SetActive(false);
        }
    }

    //Checks which zone the player is closest to.
    Zone CheckPlayerZone()//Checks which zone the player is in.
    {
        float shortestDistance = 1000;
        Zone closestZone = null;

        foreach (Zone _zone in zon_List_zones)
        {
            if (Vector3.Distance(_zone.transform.position, playcont_player.transform.position) < shortestDistance)
            {
                shortestDistance = Vector3.Distance(_zone.transform.position, playcont_player.transform.position);
                closestZone = _zone;
            }
        }
        return closestZone;
    }

    void GetZones()//Gets all the zones in the map.
    {
        foreach (Zone _zone in FindObjectsOfType<Zone>())
        {
            zon_List_zones.Add(_zone);
        }
    }

    public void DamageHealth(int _Damage)//Called in Plasma Bullet.
    {
        Recover();
        StopAllCoroutines();
        StartCoroutine(Enum_MoveToZonePoint());

        I_health -= _Damage;

        if (I_health <= F_thresehold1)
        {
            SpwnMan_OnTimboiHealth.Activate();
        }

        if (I_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void SetNavDestination(Vector3 _position)//Sets a destination.
    {
        navMesAg_agent.SetDestination(_position);
    }

    float CheckMovementTime()//Randomizes movement time.
    {
        float _movementTime = Random.Range(F_minMovementCheckTime, F_maxMovementCheckTime);
        return _movementTime;
    }

    public float CheckAttackTime()//Randomizes shoot time
    {
        float _attackTime = Random.Range(F_minAttackTime, F_maxAttackTime);
        return _attackTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlasmaBullet>())
        {
            Debug.Log("Bullet Entered");
            DamageHealth(50);
            Debug.Log(I_health);
        }
    }
}
