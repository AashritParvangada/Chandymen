using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerHole : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)//If this is a destroyer and the cleaner bot enters its radius.
    {
        CheckToDestroyCleanerBot(other);
    }

    void CheckToDestroyCleanerBot(Collider other)
    {
        if (other.GetComponent<CleanerBot>())
        {
                Destroy(other.gameObject);
        }
    }
}
