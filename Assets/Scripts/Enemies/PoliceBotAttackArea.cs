using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceBotAttackArea : MonoBehaviour
{
    PlayerController playCont_playa;

    //Assign a bunch of bots whom are activated by the player entering this trigger volume.
    [SerializeField] PoliceBot[] PB_Arr_botsTriggeredByThis;

    //When the player enters this volume, activate the bots.
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            playCont_playa = other.GetComponent<PlayerController>();
            foreach (PoliceBot _pb in PB_Arr_botsTriggeredByThis)
            {
                _pb.SetTarget(playCont_playa);
            }
        }
    }

}

