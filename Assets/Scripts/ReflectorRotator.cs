using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorRotator : MonoBehaviour
{
    Transform trans_reflectorTransform;
    bool b_openToRotate = true;
    [SerializeField] bool B_isHorizontal;
    [SerializeField] float F_time = 0.5f;
    [SerializeField] float F_angleOne = 45, F_angleTwo=135;
    private void Start()
    {
        trans_reflectorTransform = transform.parent;
    }


    //If the player is within the sphere trigger.
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.E)) //Joystick 1 is X
            {
            Debug.Log("Player in radius");
                RotateReflector();
            }
        }
    }

    void RotateReflector()
    {
        if (b_openToRotate)
        {
            MakeHorizontalOrVertical();
            StartCoroutine(CloseToRotateForTime(F_time));//This is for lerping. MUST ADD LERPING
        }
    }

    void MakeHorizontalOrVertical() //Depending on which angle the reflector starts with, make it the other one.
    {
        if (B_isHorizontal)
        {
            Vector3 verticalTransform = new Vector3(0, F_angleOne, 0);
            trans_reflectorTransform.eulerAngles = verticalTransform;
            B_isHorizontal = false;
        }

        else if (!B_isHorizontal)
        {
            Vector3 horizontalTransform = new Vector3(0, F_angleTwo, 0);
            trans_reflectorTransform.eulerAngles = horizontalTransform;
            B_isHorizontal = true;
        }
    }

    IEnumerator CloseToRotateForTime(float time) //Lock rotation for a few seconds (OnTriggerStay calls every frame)
    {
        b_openToRotate = false;
        yield return new WaitForSeconds(time);
        b_openToRotate = true;
    }
}
