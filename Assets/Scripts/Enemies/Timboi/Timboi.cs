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

    [SerializeField] int I_health = 100, I_damage = 30;
    [SerializeField] float F_minAttackTime, F_maxAttackTime, F_minMovementCheckTime, F_maxMovementCheckTime, F_recoverTime;
    bool b_lookAtPlayer = true;

    [SerializeField] GameObject[] GO_tempArray;

    private void Start()//Get variables.
    {
        GetVariables();
        StartCombat();
    }

    private void Update()
    {
        if (b_lookAtPlayer) transform.LookAt(playcont_player.transform);
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

    }

    void MoveToPointInPlayerZone()//Moves to a random point among the zone's points.
    {
        int pointToMoveTo = Random.Range(0, CheckPlayerZone().Trans_List_NavDestinations.Count);

        SetNavDestination(CheckPlayerZone().Trans_List_NavDestinations[pointToMoveTo].transform.position);

    }


    IEnumerator Enum_MoveToZonePoint() //Waits, then moves to a point within the zone.
    {
        MoveToPointInPlayerZone();
        yield return new WaitForSeconds(CheckMovementTime());

        StartCoroutine(Enum_MoveToPlayer());
    }

    IEnumerator Enum_MoveToPlayer()
    {
        MoveToPlayer();
        yield return new WaitForSeconds(CheckAttackTime());
        Recover();

    }

    void MoveToPlayer()
    {
        navMesAg_agent.speed = 10;
        navMesAg_agent.acceleration = 20;
        //transform.LookAt(playcont_player.transform);
        navMesAg_agent.SetDestination(playcont_player.transform.position);
    }

    public void Slash()
    {
        b_lookAtPlayer = false;
        navMesAg_agent.speed = 0;
        navMesAg_agent.enabled = false;

        foreach (GameObject _go in GO_tempArray)
        {
            _go.SetActive(true);
        }

        Recover();
    }

    IEnumerator Enum_RecoverTime()
    {
        yield return new WaitForSeconds(F_recoverTime);
        b_lookAtPlayer = true;
        navMesAg_agent.enabled = true;
        navMesAg_agent.speed = 10;

        foreach (GameObject _go in GO_tempArray)
        {
            _go.SetActive(false);
        }

        StartCoroutine(Enum_MoveToZonePoint());
    }

    void Recover()
    {
        StartCoroutine(Enum_RecoverTime());
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
        I_health -= _Damage;

        if (I_health <= 300)
        {
            evMan_eventManager.CountEnemyKilled();
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

    float CheckAttackTime()//Randomizes shoot time
    {
        float _attackTime = Random.Range(F_minAttackTime, F_maxAttackTime);
        return _attackTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlasmaBullet>())
        {
            DamageHealth(50);
        }
    }
}
