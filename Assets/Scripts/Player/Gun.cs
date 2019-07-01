using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] float F_bulletSpeed = 10;
    public GameObject GO_Projectile;
    PlayerController PlaCont_Playa;
    // Use this for initialization
    void Start()
    {
        PlaCont_Playa = GetComponentInParent<PlayerController>();
    }

    public void DamageParent(int _Damage)//Currently called from cleaner bot script.
    {
        PlaCont_Playa.DamageHealth(_Damage);
    }

    public void ShootProjectile(Transform _origin)//Instantiate a PlasmaBullet, set its speed and direction.
    {
        GameObject newBullet = Instantiate(GO_Projectile, _origin.position, _origin.rotation, null);
        newBullet.GetComponent<Rigidbody>().velocity = transform.forward * F_bulletSpeed;
        newBullet.transform.forward = transform.forward;

    }
}
