﻿using System.Collections;
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
    [SerializeField] ParticleSpawner ParticleSpawner_wallHit;
    private void Start()
    {
        rb_rb = GetComponent<Rigidbody>();
        StartCoroutine(IEnum_DestroyBullet());
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckHitReflector(other);

        CheckHitWall(other);


        if (other.GetComponent<Grunt>())
        {
            other.GetComponent<Grunt>().DamageHealth(F_bulletDamage);
            Destroy(gameObject);
        }


    }

    void Reflect(Vector3 _dir)//Ray that reflects the bullet. Must be placed in separate object.
    {
        transform.forward = _dir;
        rb_rb.velocity = 10 * transform.forward;
        I_BulletCharge++;
    }

    IEnumerator IEnum_DestroyBullet()
    {
        yield return new WaitForSeconds(F_bulletDestroyTime);
        Destroy(gameObject);
    }

    void CheckHitReflector(Collider other)
    {
        if (other.tag == "Reflector")
        {
            Reflect(V3_Dir);
            other.GetComponent<Animator>().SetTrigger("Wobble");
            GetComponent<AudioSource>().clip = (AudClp_ReflectorBump);
            GetComponent<AudioSource>().Play();
        }
    }

    void CheckHitWall(Collider other)
    {
        if (other.gameObject.layer == 11 || other.GetComponentInParent<ElectricDoor>())//Wall layer.
        {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        InstantiateParticles(ParticleSpawner_wallHit);
        Destroy(gameObject);
    }

    public void WallParticles()
    {
        InstantiateParticles(ParticleSpawner_wallHit);
    }

    void InstantiateParticles(ParticleSpawner _prtclSpawn)
    {
        ParticleSpawner prtclSpawn = Instantiate(_prtclSpawn, transform);
        prtclSpawn.GetVariables();
        prtclSpawn.Activate();
    }

}
