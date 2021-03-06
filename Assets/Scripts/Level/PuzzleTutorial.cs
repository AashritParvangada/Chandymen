﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTutorial : MonoBehaviour
{
    [SerializeField] GameObject Go_panel, Go_xButtonActive, Go_xButtonInactive;
    Controls ctrl_ControlPanel;
    bool b_canInteract = false, b_timeLock = false;
    bool b_panelActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            b_canInteract = true;
            SetXButtonActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            b_canInteract = false;
            Go_panel.SetActive(false);
            SetXButtonActive(false);
            ctrl_ControlPanel.gameObject.SetActive(true);
        }
    }
    void Start()
    {
        GetVariables();
        SetXButtonActive(false);
    }

    void GetVariables()
    {
        ctrl_ControlPanel = FindObjectOfType<Controls>();
    }
    private void Update()
    {
        CheckPlayerInput();
    }

    void CheckPlayerInput()
    {
        if (!b_timeLock && b_canInteract && !b_panelActive && (Input.GetKeyDown(KeyCode.JoystickButton1)
 || Input.GetKeyDown(KeyCode.E)))
        {
            SetPanelActive(true);
        }

        if (!b_timeLock && b_canInteract && b_panelActive && (Input.GetKeyDown(KeyCode.JoystickButton1)
|| Input.GetKeyDown(KeyCode.E)))
        {
            SetPanelActive(false);
        }
    }

    void SetPanelActive(bool _active)
    {
        if (!GetControlsActiveState())
        {
            ctrl_ControlPanel.gameObject.SetActive(!_active);
            StartCoroutine(IEnum_InteractCooldown());
            Go_panel.SetActive(_active);
            b_panelActive = _active;
        }
    }

    bool GetControlsActiveState()
    {
        return ctrl_ControlPanel.IsPanelOn();
    }

    IEnumerator IEnum_InteractCooldown()
    {
        b_timeLock = true;
        yield return new WaitForSeconds(0.5f);
        b_timeLock = false;
    }

    void SetXButtonActive(bool _active)
    {
        Go_xButtonActive.SetActive(_active);
        Go_xButtonInactive.SetActive(!_active);
    }
}
