using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    //Made so that text looks at the camera.
    Camera Cam;
    private void Start()
    {
        Cam = FindObjectOfType<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Cam.transform.position);
    }
}
