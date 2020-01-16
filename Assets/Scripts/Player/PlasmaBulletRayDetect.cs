using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlasmaBulletRayDetect : MonoBehaviour
{
    PlasmaBullet plasBull_parentBullet;
    GruntBullet gruntBullet_parentBullet;
    private void Start()
    {
        plasBull_parentBullet = GetComponentInParent<PlasmaBullet>();
        gruntBullet_parentBullet = GetComponentInParent<GruntBullet>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Reflector")
        {
            CalculateReflectionAngle();
        }
    }

    void CalculateReflectionAngle()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            //Debug.Log("Hit Reflector");
            //Calculates the angle for reflecting the bullet from walls.
            Vector3 reflectDirection = Vector3.Reflect(ray.direction, hit.normal);

            float rot = 90 - Mathf.Atan2(reflectDirection.z, reflectDirection.x) * Mathf.Rad2Deg;
            SetPlasBullRot(reflectDirection);
        }
    }

    private void OnDrawGizmos()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 100);

            //Debug.Log("Hit Reflector");
            //Calculates the angle for reflecting the bullet from walls.
            Vector3 reflectDirection = Vector3.Reflect(ray.direction, hit.normal);
            Gizmos.DrawLine(hit.point, hit.point + reflectDirection * 100);
        }
    }


    void SetPlasBullRot(Vector3 _dir)
    {
        if (plasBull_parentBullet) plasBull_parentBullet.V3_Dir = _dir;
        else if (gruntBullet_parentBullet) gruntBullet_parentBullet.V3_Dir = _dir;
    }
}
