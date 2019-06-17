using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSafeSpace : MonoBehaviour
{
    //This script is on cleaner bots, safe platforms etc.
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().B_AboveAcid = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().B_AboveAcid = false;
        }
    }
}
