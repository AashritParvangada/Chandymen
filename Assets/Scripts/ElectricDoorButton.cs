using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElectricDoorButton : MonoBehaviour
{
    [SerializeField] GameObject G_door;
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }

    //Opens the door and rebakes the nav mesh.
    void OpenDoor()
    {
        G_door.SetActive(false);
    }
}
