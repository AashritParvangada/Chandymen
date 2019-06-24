using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCamLock : MonoBehaviour
{
    [SerializeField] Transform Trans_camPos;
    [SerializeField] float F_camSize;

    Camera GO_PlayerCamera;
    // Start is called before the first frame update
    void Start()
    {
        GO_PlayerCamera = FindObjectOfType<Camera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            GO_PlayerCamera.GetComponent<GameCamera>().EnterPuzzleMode(Trans_camPos, F_camSize);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            GO_PlayerCamera.GetComponent<GameCamera>().B_PuzzleMode = false;
            GO_PlayerCamera.transform.SetParent(null);
        }
    }
}
