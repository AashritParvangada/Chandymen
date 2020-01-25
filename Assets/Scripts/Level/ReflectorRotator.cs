using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorRotator : MonoBehaviour
{
    Transform trans_reflectorTransform;
    bool b_openToRotate = false;
    bool b_timeLocked = false;
    [SerializeField] bool B_isHorizontal;
    [SerializeField] float F_time;
    [SerializeField] float F_angleOne = 45, F_angleTwo = 135;
    [SerializeField] GameObject Go_xButton;
    AudioSource source;

    float f_rotTime;
    bool b_rotating = false;

    private void Start()
    {
        GetVariables();
        CheckAmHorOrVer();
    }

    void GetVariables()
    {
        source=GetComponent<AudioSource>();
        trans_reflectorTransform = transform.parent;
        b_openToRotate = false;
        Go_xButton.SetActive(false);
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
            Go_xButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            b_openToRotate = false;
            Go_xButton.SetActive(false);
        }
    }

    private void Update()
    {
        if (!b_timeLocked && b_openToRotate == true && (Input.GetKeyDown(KeyCode.JoystickButton1)
         || Input.GetKeyDown(KeyCode.E)))
        {
            b_rotating = true;
            f_rotTime = 0;
            StartCoroutine(CloseToRotateForTime(F_time));
            PlayClip(1f);
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
        b_timeLocked = true;
        yield return new WaitForSeconds(time);
        CheckAmHorOrVer();
        b_timeLocked = false;
    }

    void PlayClip(float startTime)
    {
        source.time=startTime;
        source.Play();
    }
}
