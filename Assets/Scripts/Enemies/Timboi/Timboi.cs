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


    // Start is called before the first frame update
    void Start()
    {

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


    void GetZones()//Gets all the zones in the map.
    {
        foreach (Zone _zone in FindObjectsOfType<Zone>())
        {
            zon_List_zones.Add(_zone);
        }
    }
}
