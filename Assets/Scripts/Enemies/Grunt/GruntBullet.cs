using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntBullet : MonoBehaviour
{
    [SerializeField] int I_damageCaused = 30;
    [SerializeField] ParticleSpawner ParticleSpawner_hit;
    private void Start()
    {
        Destroy(gameObject, 3);//Bullet only lasts for 3 seconds.
    }


    private void OnTriggerEnter(Collider other)//If a player enters its radius, damage the player. Else if the bullet hits a wall, destroy this. 
    {
        CheckHitPlayer(other);
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

    void InstantiateParticles(ParticleSpawner _prtclSpawn)
    {
        ParticleSpawner prtclSpawn = Instantiate(_prtclSpawn, transform);
        prtclSpawn.GetVariables();
        prtclSpawn.Activate();
    }

}
