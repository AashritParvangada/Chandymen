using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : MonoBehaviour
{
    PlayerController PlayCont_Player;
    [SerializeField] float RayDistance = 50;
    //How this agent works:
    //Ray cast to player.
    //If the player isn't found, set destination to player while raycasting for player every half second.
    //If the player is found, set a destination depending on which zone the player is in.

    private void Start() {
        PlayCont_Player = FindObjectOfType<PlayerController>();
    }

    void CheckForPlayer()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, (PlayCont_Player.transform.position-transform.position), out hit, RayDistance))
        {
            if(hit.transform==PlayCont_Player.transform)
            {
                Debug.Log("I see you, punk");
            }

            else
            {
                Debug.Log("Where are you hiding, punk?");
            }
        }
    }

    private void Update() {
        CheckForPlayer();
    }
}
