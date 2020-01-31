using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().DamageHealth(100);
        }
    }
}
