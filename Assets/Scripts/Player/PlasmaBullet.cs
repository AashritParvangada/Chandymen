using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBullet : MonoBehaviour
{
    public int I_MaxReflectionCount = 10;
    public float F_MaxStepDistance = 200;
    [SerializeField] int F_bulletDamage = 50;
    public int I_BulletCharge = 0;
    [SerializeField] float F_bulletDestroyTime = 10;
    Rigidbody rb_rb;
    private void Start()
    {
        rb_rb = GetComponent<Rigidbody>();
        StartCoroutine(DestroyBullet());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Reflector")
        {
            Reflect();
        }

        else if (other.GetComponent<Grunt>())
        {
            other.GetComponent<Grunt>().DamageHealth(F_bulletDamage);
            Destroy(gameObject);
        }

        else if (other.gameObject.layer == 9)//Wall layer.
        {
            Destroy(gameObject);
        }
    }

    void Reflect()//Ray that reflects the bullet. Must be placed in separate object.
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, F_MaxStepDistance))
        {
            //Debug.Log("Hit Reflector");
            //Calculates the angle for reflecting the bullet from walls.
            Vector3 reflectDirection = Vector3.Reflect(ray.direction, hit.normal);

            float rot = 90 - Mathf.Atan2(reflectDirection.z, reflectDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
            rb_rb.velocity = 10 * transform.forward;
            I_BulletCharge++;
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(F_bulletDestroyTime);
        Destroy(gameObject);
    }
}
