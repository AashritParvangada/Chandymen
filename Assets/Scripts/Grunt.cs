using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grunt : MonoBehaviour
{
    PlayerController PlayCont_Player;
    [SerializeField] float RayDistance = 50;
    List<Zone> Zon_Zones = new List<Zone>();
    [SerializeField] float F_MinMovementCheckTime, F_MaxMovementCheckTime;

    NavMeshAgent agent;
    //How this agent works:
    //Ray cast to player.
    //If the player isn't found, set destination to player while raycasting for player every half second.
    //If the player is found, set a destination depending on which zone the player is in.

    private void Start()//Get variables.
    {
        agent = GetComponent<NavMeshAgent>();
        PlayCont_Player = FindObjectOfType<PlayerController>();
        GetZones();
        StartCoroutine(CheckToMove());//Start the movement. Will delay this later during the cutscene.
    }

    void CheckIfCanShootPlayer()//When this is called, sees if the player is in the line of sight.
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (PlayCont_Player.transform.position - transform.position), out hit, RayDistance))
        {
            if (hit.transform == PlayCont_Player.transform)
            {
                Shoot();
            }

            else
            {
                Debug.Log("Where are you hiding, punk?");
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
        Debug.Log("I see you, punk.");
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

    void SetNavDestination(Vector3 _position)//Sets a destination.
    {
        agent.SetDestination(_position);
    }
}
