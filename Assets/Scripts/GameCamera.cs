using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    Transform target;
    public Vector3 position, angle;
    Vector3 camTarget;
    [SerializeField] bool puzzleMode;
    // Use this for initialization
    void Start()
    {
        if (!puzzleMode)
        {

            target = GameObject.FindGameObjectWithTag("Player").transform;
            transform.eulerAngles = new Vector3(80, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!puzzleMode)
        {

            camTarget = target.position + position;
            transform.position = Vector3.Lerp(transform.position, camTarget, Time.deltaTime * 8);

            transform.eulerAngles = angle;
        }
    }
}
