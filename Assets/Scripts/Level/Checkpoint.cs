using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //Just sends a command to game manager.
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
            FindObjectOfType<GameManager>().SetLastCheckpoint(this);
    }
}
