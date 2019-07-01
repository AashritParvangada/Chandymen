﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grunt : MonoBehaviour
{
    [SerializeField] int I_health = 100;

    PlayerController playcont_player; Gun gun_playaGun;
    [SerializeField] float F_rayDistance = 50;
    List<Zone> zon_List_zones = new List<Zone>();
    [SerializeField] float F_minMovementCheckTime, F_maxMovementCheckTime;
    [SerializeField] float F_minShootTime, F_maxShootTime;

    NavMeshAgent navMesAg_agent;

    [SerializeField] Transform Trans_gruntGun;
    [SerializeField] GameObject GO_bullet;
    [SerializeField] float F_bulletSpeed;
    [SerializeField] bool B_shootOnStart = true;
    EventManager evMan_eventManager;
    Animator anim_Controller;
    Rigidbody rb_RB;
    float f_currentSpeed;
    //How this agent works:
    //Ray cast to player.
    //If the player isn't found, set destination to player while raycasting for player every half second.
    //If the player is found, set a destination depending on which zone the player is in.
    private void OnEnable()
    {
        EventManager.OnDialogueComplete += StartCombat;
    }
    private void OnDisable()
    {
        EventManager.OnDialogueComplete -= StartCombat;
    }
    private void Start()//Get variables.
    {
        GetVariables();
        if (B_shootOnStart == true)
        {
            StartCombat();
        }
    }

    void StartCombat()
    {
        StartCoroutine(CheckToMove());//Start the movement. Will delay this later during the cutscene.
        StartCoroutine(CheckToShoot());//Start shooting. Will delay this later.
    }

    void GetVariables()//Get event manager, nav msh agent, player cont, gun, and make zone list.
    {
        evMan_eventManager = FindObjectOfType<EventManager>();
        navMesAg_agent = GetComponent<NavMeshAgent>();
        playcont_player = FindObjectOfType<PlayerController>();
        gun_playaGun = FindObjectOfType<Gun>();
        anim_Controller = GetComponentInChildren<Animator>();
        rb_RB = GetComponent<Rigidbody>();
        GetZones();

    }

    private void Update()
    {
        transform.LookAt(playcont_player.transform);//Just stare at the player.
        anim_Controller.SetFloat("Speed", navMesAg_agent.velocity.magnitude);
        //CheckToShoot(); //Use later for shotgun.
    }

    void CheckIfCanShootPlayer()//When this is called, sees if the player is in the line of sight. If so, shoot.
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (playcont_player.transform.position - transform.position), out hit, F_rayDistance))
        {
            if (hit.transform == playcont_player.transform || hit.transform == gun_playaGun.transform)
            {
                Shoot();
            }

        }
    }

    void GetZones()//Gets all the zones in the map.
    {
        foreach (Zone _zone in FindObjectsOfType<Zone>())
        {
            zon_List_zones.Add(_zone);
        }
    }

    void Shoot()//Shoots a spray of bullets.
    {
        anim_Controller.SetTrigger("Shot");

        GameObject _bullet = Instantiate(GO_bullet, null);
        _bullet.transform.position = Trans_gruntGun.position;
        _bullet.transform.forward = transform.forward;
        _bullet.GetComponent<Rigidbody>().velocity = transform.forward * F_bulletSpeed;

        GameObject _bullet2 = Instantiate(GO_bullet, null);
        _bullet2.transform.position = Trans_gruntGun.position;
        _bullet2.transform.forward = transform.forward;
        _bullet2.transform.eulerAngles = new Vector3(_bullet2.transform.eulerAngles.x, _bullet2.transform.eulerAngles.y + 10, 0);
        _bullet2.GetComponent<Rigidbody>().velocity = _bullet2.transform.forward * F_bulletSpeed;


        GameObject _bullet3 = Instantiate(GO_bullet, null);
        _bullet3.transform.position = Trans_gruntGun.position;
        _bullet3.transform.forward = transform.forward;
        _bullet3.transform.eulerAngles = new Vector3(_bullet3.transform.eulerAngles.x, _bullet3.transform.eulerAngles.y - 10, 0);
        _bullet3.GetComponent<Rigidbody>().velocity = _bullet3.transform.forward * F_bulletSpeed;

    }

    IEnumerator CheckToShoot()//Repeats on itself to keep shooting at the player.
    {
        yield return new WaitForSeconds(CheckShootTime());
        CheckIfCanShootPlayer();
        StartCoroutine(CheckToShoot());
    }

    IEnumerator CheckToMove() //Waits, then moves to a point within the zone.
    {
        yield return new WaitForSeconds(CheckMovementTime());
        MoveToPointInPlayerZone();

        StartCoroutine(CheckToMove());
    }

    void MoveToPointInPlayerZone()//Moves to a random point among the zone's points.
    {
        int pointToMoveTo = Random.Range(0, CheckPlayerZone().Trans_List_NavDestinations.Count);

        SetNavDestination(CheckPlayerZone().Trans_List_NavDestinations[pointToMoveTo].transform.position);

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

    float CheckMovementTime()//Randomizes movement time.
    {
        float _movementTime = Random.Range(F_minMovementCheckTime, F_maxMovementCheckTime);
        return _movementTime;
    }

    float CheckShootTime()//Randomizes shoot time
    {
        float _shoottime = Random.Range(F_minShootTime, F_maxShootTime);
        return _shoottime;
    }

    void SetNavDestination(Vector3 _position)//Sets a destination.
    {
        navMesAg_agent.SetDestination(_position);
    }

    public void DamageHealth(int _Damage)//Called in Plasma Bullet.
    {
        I_health -= _Damage;
        if (I_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()//When this enemy is killed, trigger event count enemies killed.   
    {
        evMan_eventManager.CountEnemyKilled();
    }
}
