using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElectricDoorButton : MonoBehaviour
{
    [SerializeField] NavMeshSurface NMS_NavToRebake;
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

        if (NMS_NavToRebake)
            NMS_NavToRebake.BuildNavMesh();
    }
}
