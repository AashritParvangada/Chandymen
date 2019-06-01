using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grunt : MonoBehaviour
{
    int I_Health = 100;

    PlayerController PlayCont_Player; Gun Gun_PlayaGun;
    [SerializeField] float RayDistance = 50;
    List<Zone> Zon_Zones = new List<Zone>();
    [SerializeField] float F_MinMovementCheckTime, F_MaxMovementCheckTime;
    [SerializeField] float F_MinShootTime, F_MaxShootTime;

    NavMeshAgent agent;

    [SerializeField] Transform Trans_GruntGun;
    [SerializeField] GameObject GO_Bullet;
    [SerializeField] float F_BulletSpeed;
    //How this agent works:
    //Ray cast to player.
    //If the player isn't found, set destination to player while raycasting for player every half second.
    //If the player is found, set a destination depending on which zone the player is in.

    private void Start()//Get variables.
    {
        agent = GetComponent<NavMeshAgent>();
        PlayCont_Player = FindObjectOfType<PlayerController>();
        Gun_PlayaGun=FindObjectOfType<Gun>();
        GetZones();
        StartCoroutine(CheckToMove());//Start the movement. Will delay this later during the cutscene.
        StartCoroutine(CheckToShoot());//Start shooting. Will delay this later.
    }

    private void Update()
    {
        transform.LookAt(PlayCont_Player.transform);
        //CheckToShoot(); //Use later for shotgun.
    }

    void CheckIfCanShootPlayer()//When this is called, sees if the player is in the line of sight.
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (PlayCont_Player.transform.position - transform.position), out hit, RayDistance))
        {
            if (hit.transform == PlayCont_Player.transform || hit.transform == Gun_PlayaGun.transform)
            {
                Shoot();
            }

        }
    }

    void GetZones()//Gets all the zones in the map.
    {
        foreach (Zone _zone in FindObjectsOfType<Zone>())
        {
            Zon_Zones.Add(_zone);
        }
    }

    void Shoot()//Shoots a spray of bullets.
    {
        GameObject _bullet = Instantiate(GO_Bullet, null);
        _bullet.transform.position = Trans_GruntGun.position;
        _bullet.transform.forward = transform.forward;
        _bullet.GetComponent<Rigidbody>().velocity = transform.forward * F_BulletSpeed;

        GameObject _bullet2 = Instantiate(GO_Bullet, null);
        _bullet2.transform.position = Trans_GruntGun.position;
        _bullet2.transform.forward = transform.forward;
        _bullet2.transform.eulerAngles = new Vector3(_bullet2.transform.eulerAngles.x, _bullet2.transform.eulerAngles.y + 10, 0);
        _bullet2.GetComponent<Rigidbody>().velocity = _bullet2.transform.forward * F_BulletSpeed;


        GameObject _bullet3 = Instantiate(GO_Bullet, null);
        _bullet3.transform.position = Trans_GruntGun.position;
        _bullet3.transform.forward = transform.forward;
        _bullet3.transform.eulerAngles = new Vector3(_bullet3.transform.eulerAngles.x, _bullet3.transform.eulerAngles.y - 10, 0);
        _bullet3.GetComponent<Rigidbody>().velocity = _bullet3.transform.forward * F_BulletSpeed;

    }

IEnumerator CheckToShoot()
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

        foreach (Zone _zone in Zon_Zones)
        {
            if (Vector3.Distance(_zone.transform.position, PlayCont_Player.transform.position) < shortestDistance)
            {
                shortestDistance = Vector3.Distance(_zone.transform.position, PlayCont_Player.transform.position);
                closestZone = _zone;
            }
        }
        return closestZone;
    }

    float CheckMovementTime()//Randomizes movement time.
    {
        float _movementTime = Random.Range(F_MinMovementCheckTime, F_MaxMovementCheckTime);
        return _movementTime;
    }

    float CheckShootTime()//Randomizes shoot time
    {
        float _shoottime=Random.Range(F_MinShootTime, F_MaxShootTime);
        return _shoottime;
    }

    void SetNavDestination(Vector3 _position)//Sets a destination.
    {
        agent.SetDestination(_position);
    }

    public void DamageHealth(int _Damage)
    {
        I_Health-=_Damage;
        if(I_Health<=0)
        {
            Destroy(gameObject);
        }
    }
}
