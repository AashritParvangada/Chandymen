using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceBotAttackArea : MonoBehaviour
{
    PlayerController PC_Playa;
    [SerializeField] PoliceBot[] PB_Arr_BotsTriggeredByThis;
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

