using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyDetector : MonoBehaviour
{
    Camera cam_Camera;
    [SerializeField] float F_transparency = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        cam_Camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam_Camera.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            Debug.Log("Make transparent");

            SetAlpha(other.GetComponent<Renderer>(), F_transparency);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
        {
            SetAlpha(other.GetComponent<Renderer>(), 1);
        }
    }

    private void SetAlpha(Renderer _rend, float _alpha)
    {
        Color _color = _rend.material.color;
        _color.a = _alpha;
        _rend.material.color = _color;
    }
}
