using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTutorial : MonoBehaviour
{
    [SerializeField] GameObject Go_panel;
    bool b_canInteract = false;
    bool b_panelActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            b_canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            b_canInteract = false;
            Go_panel.SetActive(false);
        }
    }

    private void Update()
    {
        CheckPlayerInput();
    }

    void CheckPlayerInput()
    {
        if (b_canInteract && !b_panelActive && (Input.GetKeyDown(KeyCode.JoystickButton1)
 || Input.GetKeyDown(KeyCode.E)))
        {
            SetPanelActive(true);
        }

        if (b_canInteract && b_panelActive && (Input.GetKeyDown(KeyCode.JoystickButton1)
|| Input.GetKeyDown(KeyCode.E)))
        {
            SetPanelActive(false);
        }
    }

    void SetPanelActive(bool _active)
    {
        StartCoroutine(IEnum_InteractCooldown());
        Go_panel.SetActive(_active);
        b_panelActive = _active;
    }

    IEnumerator IEnum_InteractCooldown()
    {
        b_canInteract = false;
        yield return new WaitForSeconds(1);
        b_canInteract = true;
    }
}
