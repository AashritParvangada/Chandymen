using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElectricDoorButton : MonoBehaviour
{
    [SerializeField] GameObject G_door;
    [SerializeField] float F_holdTime;
    float f_startTime; bool b_newKey = false;
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.E))
        {
            f_startTime = Time.time;
            b_newKey = true;
        }
        if ((Input.GetKey(KeyCode.Joystick1Button1) || Input.GetKey(KeyCode.E)) && b_newKey)
        {
            Debug.Log(Time.time - f_startTime);
            if (Time.time - f_startTime >= F_holdTime) OpenDoor();
        }
        if (Input.GetKeyUp(KeyCode.Joystick1Button1) || Input.GetKeyUp(KeyCode.E)) b_newKey = false;
    }

    //Opens the door and rebakes the nav mesh.
    void OpenDoor()
    {
        G_door.SetActive(false);
    }
}
