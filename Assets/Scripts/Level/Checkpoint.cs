using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] bool B_isPuzzle = false;
    bool b_enteredYet = false;
    [SerializeField] ElectricDoor elecdoor_ToClose;
    //Just sends a command to game manager.
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            FindObjectOfType<GameManager>().SetLastCheckpoint(this);

            if (!b_enteredYet)
            {
                FindObjectOfType<AudioManager>().SetPiecePuzzle(B_isPuzzle);
                b_enteredYet = true;

                if (elecdoor_ToClose) elecdoor_ToClose.SwitchDoor(true);
            }
        }
    }
}
