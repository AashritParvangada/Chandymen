using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerHome : MonoBehaviour
{
    [SerializeField] bool B_TrueDestroyerFalseSpawner = true;

    private void OnTriggerEnter(Collider other)//If this is a destroyer and the cleaner bot enters its radius.
    {
        CheckToDestroyCleanerBot(other);
    }

    void CheckToDestroyCleanerBot(Collider other)
    {
        if (other.GetComponent<CleanerBot>())
        {
            if (B_TrueDestroyerFalseSpawner)
                Destroy(other.gameObject);
        }
    }
}
