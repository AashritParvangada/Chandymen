using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceBotAttackArea : MonoBehaviour
{
    PlayerController PC_Playa;

    //Assign a bunch of bots whom are activated by the player entering this trigger volume.
    [SerializeField] PoliceBot[] PB_Arr_BotsTriggeredByThis;

    //When the player enters this volume, activate the bots.
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            PC_Playa = other.GetComponent<PlayerController>();
            foreach (PoliceBot _pb in PB_Arr_BotsTriggeredByThis)
            {
                _pb.SetTarget(PC_Playa);
            }
        }
    }

}

