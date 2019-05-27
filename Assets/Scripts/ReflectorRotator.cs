using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorRotator : MonoBehaviour
{
    Transform reflectorTransform;
    bool openToRotate = true;
    [SerializeField] bool isHorizontal;
    [SerializeField] float f_Time = 0.5f;
    private void Start()
    {
        reflectorTransform = transform.parent;
    }


    //If the player is within the sphere trigger.
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton1)) //Joystick 1 is X
            {
            Debug.Log("Player in radius");
                RotateReflector();
            }
        }
    }

    void RotateReflector()
    {
        if (openToRotate)
        {
            MakeHorizontalOrVertical();
            StartCoroutine(CloseToRotateForTime(f_Time));
        }
    }

    void MakeHorizontalOrVertical() //Depending on which angle the reflector starts with, make it the other one.
    {
        if (isHorizontal)
        {
            Vector3 verticalTransform = new Vector3(0, 45, 0);
            reflectorTransform.eulerAngles = verticalTransform;
            isHorizontal = false;
        }

        else if (!isHorizontal)
        {
            Vector3 horizontalTransform = new Vector3(0, 135, 0);
            reflectorTransform.eulerAngles = horizontalTransform;
            isHorizontal = true;
        }
    }

    IEnumerator CloseToRotateForTime(float time) //Lock rotation for a few seconds (OnTriggerStay calls every frame)
    {
        openToRotate = false;
        yield return new WaitForSeconds(time);
        openToRotate = true;
    }
}
