using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntBullet : MonoBehaviour
{
    [SerializeField] int I_damageCaused = 30;
    [SerializeField] ParticleSpawner ParticleSpawner_hit;
    public Vector3 V3_Dir;
    Rigidbody rb;
    [SerializeField] AudioClip AudClp_ReflectorBump;

    private void Start()
    {
        GetVariables();
        Destroy(gameObject, 7);//Bullet only lasts for x seconds.
    }

    void GetVariables()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnTriggerEnter(Collider other)//If a player enters its radius, damage the player. Else if the bullet hits a wall, destroy this. 
    {
        CheckHitPlayer(other);
        CheckHitReflector(other);
        CheckHitWall(other);
    }

    void CheckHitWall(Collider other)
    {
        if (other.gameObject.layer == 11 || other.GetComponentInParent<ElectricDoor>())//Wall layer.
        {
            InstantiateParticles(ParticleSpawner_hit);
            Destroy(gameObject);
        }
    }

    void CheckHitPlayer(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            InstantiateParticles(ParticleSpawner_hit);
            other.GetComponent<PlayerController>().DamageHealth(I_damageCaused);
            Destroy(gameObject);
        }
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

    void InstantiateParticles(ParticleSpawner _prtclSpawn)
    {
        ParticleSpawner prtclSpawn = Instantiate(_prtclSpawn, transform);
        prtclSpawn.GetVariables();
        prtclSpawn.Activate();
    }

    void Reflect(Vector3 _dir)//Ray that reflects the bullet. Must be placed in separate object.
    {
        transform.forward = _dir;
        rb.velocity = 10 * transform.forward;
    }

}
