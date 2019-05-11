using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricDoorButton : MonoBehaviour
{

    [SerializeField] GameObject G_Door;
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        G_Door.SetActive(false);
    }
}
