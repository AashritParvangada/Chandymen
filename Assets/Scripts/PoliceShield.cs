using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceShield : MonoBehaviour
{
    //Destroy bullets that hit the shield, but damage the player who hits it.
    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<PlasmaBullet>())
        {
            Destroy(other.gameObject);
        }

        if(other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().DamageHealth(40);
        }

        if(other.GetComponent<Gun>())
        {
            other.GetComponent<Gun>().DamageParent(40);
        }
    }
}
