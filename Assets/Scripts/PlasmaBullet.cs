﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBullet : MonoBehaviour
{
    public int I_maxReflectionCount = 10;
    public float F_maxStepDistance = 200;

    public int I_BulletCharge = 0;
    [SerializeField] float F_bulletDestroyTime=10;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DestroyBullet());
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
        if (Physics.Raycast(ray, out hit, F_maxStepDistance))
        {
            Debug.Log("Hit Reflector");
            //Calculates the angle for reflecting the bullet from walls.
            Vector3 reflectDirection = Vector3.Reflect(ray.direction, hit.normal);

            float rot = 90 - Mathf.Atan2(reflectDirection.z, reflectDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
            rb.velocity = 10 * transform.forward;
            I_BulletCharge++;
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(F_bulletDestroyTime);
        Destroy(gameObject);
    }
}
