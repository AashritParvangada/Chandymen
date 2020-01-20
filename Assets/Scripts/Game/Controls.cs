using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] GameObject GO_controlsPanel;
    HealthBar healthBar; Animator animator;
    bool b_panelIsOn = false, b_cooldownActive = false;

    // Update is called once per frame
    void Update()
    {
        CheckKey();
    }

    public bool IsPanelOn()
    {
        return b_panelIsOn;
    }

    private void Start()
    {
        GetVariables();
    }

    void GetVariables()
    {
        healthBar = FindObjectOfType<HealthBar>();
        animator = GetComponentInChildren<Animator>();
    }

    void SwitchOnControls(bool _onOff)
    {
        animator.SetBool("On", _onOff);
        healthBar.gameObject.SetActive(!_onOff);
        GO_controlsPanel.SetActive(_onOff);
        b_panelIsOn = _onOff;
        StartCoroutine(IEnum_Cooldown());
        SetTimeScale(_onOff);
    }

    void CheckKey()
    {
        if ((Input.GetKeyDown(KeyCode.JoystickButton9) || Input.GetKeyDown(KeyCode.Tab)) && !b_panelIsOn && !b_cooldownActive)
        {
            SwitchOnControls(true);
        }

        if ((Input.GetKeyDown(KeyCode.JoystickButton9) || Input.GetKeyDown(KeyCode.Tab)) && b_panelIsOn && !b_cooldownActive)
        {
            SwitchOnControls(false);
        }
    }

    void SetTimeScale(bool is1)
    {
        if (is1) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    IEnumerator IEnum_Cooldown()
    {
        b_cooldownActive = true;
        yield return new WaitForEndOfFrame();
        b_cooldownActive = false;
    }
}
