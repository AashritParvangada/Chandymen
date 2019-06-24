using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    Transform trans_target;
    public Vector3 v3_distanceFromPlayer, v3_viewingAngle;
    [SerializeField] float F_camSize;
    Vector3 v3_camTarget;
    public bool B_PuzzleMode;
    // Use this for initialization
    void Start()
    {
        trans_target = GameObject.FindObjectOfType<PlayerController>().transform;
        F_camSize = GetComponent<Camera>().orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (!B_PuzzleMode)
        {

            v3_camTarget = trans_target.position + v3_distanceFromPlayer;
            transform.position = Vector3.Lerp(transform.position, v3_camTarget, Time.deltaTime * 8);

            transform.eulerAngles = v3_viewingAngle;
        }
    }

    void FollowTarget()
    {
        v3_camTarget = trans_target.position + v3_distanceFromPlayer;//Set cam target separately from player target.
        transform.position = Vector3.Lerp(transform.position, v3_camTarget, Time.deltaTime * 8);//Lerp smoothly to cam target.

        transform.eulerAngles = v3_viewingAngle;//Look at player from this specific angle.
    }

    public void EnterPuzzleMode(Transform _newCamPos, float _camSize)
    {
        B_PuzzleMode = true;
        transform.SetParent(_newCamPos);
        transform.localPosition = new Vector3(0, 0, 0);
        GetComponent<Camera>().orthographicSize = _camSize;
    }

    public void ExitPuzzleMode()
    {
        B_PuzzleMode = false;
        transform.SetParent(null);
        GetComponent<Camera>().orthographicSize = F_camSize;
    }
}
