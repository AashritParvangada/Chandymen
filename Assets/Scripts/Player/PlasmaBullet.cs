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
    public Vector3 V3_Dir;
    public AudioClip AudClp_ReflectorBump;
    private void Start()
    {
        rb_rb = GetComponent<Rigidbody>();
        StartCoroutine(DestroyBullet());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Reflector")
        {
            Reflect(V3_Dir);
            GetComponent<AudioSource>().clip = (AudClp_ReflectorBump);
            GetComponent<AudioSource>().Play();
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

    void Reflect(Vector3 _dir)//Ray that reflects the bullet. Must be placed in separate object.
    {
        transform.forward = _dir;
        rb_rb.velocity = 10 * transform.forward;
        I_BulletCharge++;
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(F_bulletDestroyTime);
        Destroy(gameObject);
    }
}
