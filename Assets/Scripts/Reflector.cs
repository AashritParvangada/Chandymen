using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    Transform reflectorTransform;
    bool openToRotate = true;
    [SerializeField] bool isHorizontal;
    [SerializeField] float f_Time = 0.5f;
    private void Start()
    {
        reflectorTransform = transform;
    }


    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Got Player");
        if (other.tag=="Player")
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton1)) //Was 1
            {
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

    void MakeHorizontalOrVertical()
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

    IEnumerator CloseToRotateForTime(float time)
    {
        openToRotate = false;
        yield return new WaitForSeconds(time);
        openToRotate = true;
    }
}
