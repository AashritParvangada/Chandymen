﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorRotator : MonoBehaviour
{
    Transform trans_reflectorTransform;
    bool b_openToRotate = false;
    [SerializeField] bool B_isHorizontal;
    [SerializeField] float F_time = 0.5f;
    [SerializeField] float F_angleOne = 45, F_angleTwo = 135;


    float f_rotTime;
    bool b_rotating = false;

    private void Start()
    {
        GetVariables();
        CheckAmHorOrVer();
    }

    void GetVariables()
    {
        trans_reflectorTransform = transform.parent;
        b_openToRotate = false;
    }

    void CheckAmHorOrVer()
    {
        float _angleOne, _angleTwo;
        _angleOne = trans_reflectorTransform.eulerAngles.y - F_angleOne;
        _angleTwo = trans_reflectorTransform.eulerAngles.y - F_angleTwo;

        B_isHorizontal = Mathf.Abs(_angleOne) < Mathf.Abs(_angleTwo) ? false : true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            b_openToRotate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            b_openToRotate = false;
        }
    }

    private void Update()
    {
        if (b_openToRotate == true && (Input.GetKeyDown(KeyCode.JoystickButton1)
         || Input.GetKeyDown(KeyCode.E)))
        {
            b_rotating = true;
            f_rotTime = 0;
            StartCoroutine(CloseToRotateForTime(F_time));
            Debug.Log(B_isHorizontal);
        }

        if (b_rotating)
        {
            RotateReflectorUpdate();
        }

        if (f_rotTime > F_time)
        {
            b_rotating = false;
        }
    }

    void RotateReflectorUpdate()
    {
        f_rotTime += Time.deltaTime;
        trans_reflectorTransform.rotation = Quaternion.Lerp(trans_reflectorTransform.rotation,
         GetTargetRotation(), f_rotTime);
    }

    Quaternion GetTargetRotation() //Depending on which angle the reflector starts with, make it the other one.
    {
        if (B_isHorizontal)
        {
            Vector3 verticalTransform = new Vector3(0, F_angleOne, 0);
            Quaternion quat_verticalTransform = Quaternion.Euler(verticalTransform);
            return quat_verticalTransform;
        }

        else
        {
            Vector3 horizontalTransform = new Vector3(0, F_angleTwo, 0);
            Quaternion quat_horTransform = Quaternion.Euler(horizontalTransform);
            return quat_horTransform;
        }

    }

    IEnumerator CloseToRotateForTime(float time) //Lock rotation for a few seconds (OnTriggerStay calls every frame)
    {
        b_openToRotate = false;
        yield return new WaitForSeconds(time);
        CheckAmHorOrVer();
        b_openToRotate = true;
    }
}
