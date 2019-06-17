using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    Transform trans_target;
    public Vector3 v3_distanceFromPlayer, v3_viewingAngle;
    Vector3 v3_camTarget;
    public bool B_PuzzleMode;
    // Use this for initialization
    void Start()
    {
        if (!B_PuzzleMode)
        {

            trans_target = GameObject.FindObjectOfType<PlayerController>().transform;
            transform.eulerAngles = new Vector3(80, 0, 0);
        }
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
}
