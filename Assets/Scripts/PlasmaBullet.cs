using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBullet : MonoBehaviour
{
    public int maxReflectionCount = 10;
    public float maxStepDistance = 200;

    public int bulletCharge = 0;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Reflector")
        {
            Reflect();
        }
    }

    void Reflect()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxStepDistance))
        {
            //Debug.Log("Hit something");
            Vector3 reflectDirection = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDirection.z, reflectDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
            rb.velocity = 8 * transform.forward;
        }
    }
}
