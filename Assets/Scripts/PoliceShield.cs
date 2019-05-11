using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceShield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<PlasmaBullet>())
        {
            Destroy(other.gameObject);
        }

        if(other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().DamageHealth(40);
        }
    }
}
