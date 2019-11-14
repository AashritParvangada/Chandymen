using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorRotator : MonoBehaviour
{
    Transform trans_reflectorTransform;
    bool b_openToRotate = false;
    [SerializeField] bool B_isHorizontal;
    [SerializeField] float F_time = 0.5f;
    [SerializeField] float F_angleOne = 45, F_angleTwo = 135;

    private void OnEnable()
    {
        EventManager.OnRotateReflectors += CheckRotateReflector;
    }
    private void OnDisable()
    {
        EventManager.OnRotateReflectors -= CheckRotateReflector;
    }

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

    //If the player is within the sphere trigger.
    public void CheckRotateReflector()
    {
        if (b_openToRotate == true)
        {
            RotateReflector();
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
